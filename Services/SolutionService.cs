using Android.OS;
using Newtonsoft.Json;
using WordleClone.Model;
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

        public async Task<WordEntry> GetDefinition(string word)
        {
            dynamic entries = "";
            HttpClient client = new HttpClient();
            Uri uri = new Uri("https://api.dictionaryapi.dev/api/v2/entries/en/" + word);
            HttpResponseMessage response = client.GetAsync(uri).ConfigureAwait(false).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                entries = JsonConvert.DeserializeObject(content) ?? "";
            }

            WordEntry entry = new WordEntry(Convert.ToString(entries[0].meanings[0].partOfSpeech), Convert.ToString(entries[0].meanings[0].definitions[0].definition));

            return entry;
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
