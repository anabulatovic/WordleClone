using System.Text.Json;

namespace WordleClone.Services
{
    public class SolutionGeneratorService //: ISolutionGeneratorService
    {
        HttpClient _client = new HttpClient();
        public string Solution { get; set; }

        public async Task<string> GenerateSolution()
        {
            Uri uri = new Uri("https://random-word-api.herokuapp.com/word?length=5");

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Solution = JsonSerializer.Deserialize<string>(content);
                }
            }

            catch
            {

            }

            return Solution;
        }
    }
}
