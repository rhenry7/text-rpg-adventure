using System.Text;
using Anthropic;
using Anthropic.Exceptions;
using Anthropic.Models.Beta.Messages;

namespace simple_console_RPG
{
    // Dungeon master backed by Claude. Keeps one conversation for the whole
    // adventure so every chapter remembers what came before.
    public class Narrator
    {
        private const string ModelId = "claude-opus-5-5";

        private readonly AnthropicClient client;
        private readonly List<BetaMessageParam> history = new();
        private string systemPrompt = "";

        public Narrator()
        {
            // ANTHROPIC_API_KEY env var wins; otherwise fall back to apikey.txt next to the exe
            string? apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey) && File.Exists("apikey.txt"))
            {
                apiKey = File.ReadAllText("apikey.txt").Trim();
            }

            client = new AnthropicClient { ApiKey = apiKey };
        }

        // set once at the start - the world facts stay fixed for the rest of the story
        public void StartAdventure(PlayerStats player, string setting, string enemy, string enemyLeader, string playerGoal, string enemyGoal, string outline)
        {
            systemPrompt =
                "You are the dungeon master of a retro text-based Dungeons & Dragons adventure. "
                + "Tell a dramatic, poetic, heartfelt story with vivid action. "
                + "Write in second person, addressing the hero directly. "
                + "Write plain prose only: no markdown, no headings, no lists. "
                + "Keep each reply to the length the request asks for, and never decide the player's choices for them. "
                + "The game engine tracks health and stats; narrate the outcomes it reports and never invent different numbers.\n\n"
                + "World facts (keep these consistent for the whole story):\n"
                + $"- Hero: {player.Name}, a {player.PlayerClass}\n"
                + $"- Setting: {setting}\n"
                + $"- Enemy: {enemy}, led by {enemyLeader}\n"
                + $"- The hero's goal: {playerGoal}\n"
                + $"- The enemy's motivation: {enemyGoal}\n"
                + $"- Story outline: {outline}";
            history.Clear();
        }

        // sends the next story beat, streams the reply to the console, and returns it
        public async Task<string> NarrateAsync(string prompt)
        {
            history.Add(new BetaUserMessageParam { Content = prompt });

            var parameters = new MessageCreateParams
            {
                Model = ModelId,
                MaxTokens = 16000,
                System = systemPrompt,
                OutputConfig = new BetaOutputConfig { Effort = Effort.Low },
                // if Claude declines a request, the server reruns it on a fallback model
                Betas = ["server-side-fallback-2026-07-01"],
                Fallbacks = new Default(),
                Messages = history,
            };

            var story = new StringBuilder();
            string? stopReason = null;

            try
            {
                Console.WriteLine();
                await foreach (var streamEvent in client.Beta.Messages.CreateStreaming(parameters))
                {
                    if (streamEvent.TryPickContentBlockDelta(out var blockDelta)
                        && blockDelta.Delta.TryPickText(out var text))
                    {
                        Console.Write(text.Text);
                        story.Append(text.Text);
                    }
                    else if (streamEvent.TryPickDelta(out var messageDelta))
                    {
                        stopReason = messageDelta.Delta.StopReason?.ToString();
                    }
                }
                Console.WriteLine("\n");
            }
            catch (Exception ex) when (ex is AnthropicApiException or AnthropicIOException or AnthropicSseException)
            {
                Console.WriteLine($"\n[the narrator falls silent... ({ex.Message})]\n");
                history.RemoveAt(history.Count - 1);
                return "";
            }

            if (stopReason == "refusal")
            {
                // a declined turn is discarded so the conversation stays clean
                Console.WriteLine("\n[the narrator declines to tell this part of the tale]\n");
                history.RemoveAt(history.Count - 1);
                return "";
            }

            history.Add(new BetaAssistantMessageParam { Content = story.ToString() });
            return story.ToString();
        }
    }
}
