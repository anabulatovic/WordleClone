using Newtonsoft.Json;
using static Android.Print.PrintAttributes;

namespace WordleClone.Services
{
    public class SolutionService
    {
        public async Task<string> GetSolution()
        {
            string solution = "";
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://random-word-api.herokuapp.com/word?length=5");       
            HttpResponseMessage response = client.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                solution = JsonConvert.DeserializeObject<List<String>>(content)[0];
            }

            return solution;
        }

        public async Task<List<string>> GetDefinition(string word)
        {
            List<string> definition = new List<string>{ "", "" };
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://api.dictionaryapi.dev/api/v2/entries/en/" + "hello");
            HttpResponseMessage response = client.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                definition = JsonConvert.DeserializeObject<List<String>>(content);
            }

            return definition;
        }

        public async Task<bool> Exists(string input)
        {
            bool isValid = false;
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri("https://api.dictionaryapi.dev/api/v2/entries/en/" + input);
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
    }
}
