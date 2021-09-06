using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Task1.DTO;

namespace Task1
{
    class TextService
    {
        private string user = "TMG-Api-Key";
        private string _appKey = "0J/RgNC40LLQtdGC0LjQutC4IQ==";

        public async Task<string> GetResults(string query)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //get-request
                    client.DefaultRequestHeaders.Add(user, _appKey);
                    var result = await client.GetStringAsync($"http://tmgwebtest.azurewebsites.net/api/textstrings/{query}");

                    //convert it
                    var data = JsonConvert.DeserializeObject<ResultItem>(result);
                    string respond = data.Content.ToString();

                    return respond;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
