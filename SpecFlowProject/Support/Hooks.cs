using TechTalk.SpecFlow;

namespace SpecFlowProject.Support
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public static async Task BeforeScenario()
        {
            // Gave this a shot, but with my lack of C# knowledge 
            // This would take too long to implement properly
            //using var client = new HttpClient();

            //var data = new Dictionary<string, string>
            //{
            //    {"username", "admin"},
            //    {"password", "admin"}
            //};

            //var response = await client.PostAsync("https://demo.1crmcloud.com/json.php?action=login", new FormUrlEncodedContent(data));
            //var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);
        }
    }
}