using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using simple_console_RPG;
using System.Text.Json;
using System.Text.Json.Serialization;


public class AdventureGenerator
{

    public enum Location
    {
        Castle = 1,
        Forrest = 2,
        Desert = 3,
        Swamp = 4,
        Dungeon = 5,
        Village = 6,
        Mountain = 7,
        Volcano = 8,
        IceKingdom = 9,
        RockyVillage = 10,
    }

    public async Task<string>  GenerateAdventure(int diceValue)
    {
        string prompt = "Write a set in a Forrest";
        string apiKey = "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";
        string apiUrl = "https://api.openai.com/v1/engines/davinci-codex/completions";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        //if(diceValue == 2)
        //{
        //    prompt = prompt + Location.Forrest;
        //    //Console.WriteLine(prompt);
        //}

        var requestData = new
        {
            prompt,
            max_tokens = 100,
            n = 1,
            stop = "\n"
        };

        Console.WriteLine(prompt);
        var json = JsonSerializer.Serialize(requestData);

        var content = new StringContent(json);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


        var response = await client.PostAsync("https://api.openai.com/v1/completions", content);
        var resjson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("status success");
            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
            string generatedText = responseObject.choices[0].text;
            return generatedText;
        }
        else
        {
            Console.WriteLine("Failed to generate text");
            return "";
        }

    }
}
