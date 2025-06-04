using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ASimmo.Tests
{
    public class ControllersFunctionalTests : IClassFixture<CustomWebApplicationFactory<ASimmo.Startup>>
    {
        private readonly CustomWebApplicationFactory<ASimmo.Startup> _factory;
        public ControllersFunctionalTests(CustomWebApplicationFactory<ASimmo.Startup> factory)
        {
            _factory = factory;
        }

        private async Task<HttpClient> GetAuthenticatedClientAsync(string email, string password)
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var loginData = new[]
            {
                new KeyValuePair<string, string>("Input.Email", email),
                new KeyValuePair<string, string>("Input.Password", password),
                new KeyValuePair<string, string>("Input.RememberMe", "false")
            };
            var content = new FormUrlEncodedContent(loginData);
            // Get antiforgery token
            var getLogin = await client.GetAsync("/Identity/Account/Login");
            var html = await getLogin.Content.ReadAsStringAsync();
            var token = System.Text.RegularExpressions.Regex.Match(html, "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"([^\"]+)\"").Groups[1].Value;
            var cookie = getLogin.Headers.GetValues("Set-Cookie").FirstOrDefault(x => x.Contains(".AspNetCore.Antiforgery"));
            if (!string.IsNullOrEmpty(token) && cookie != null)
            {
                client.DefaultRequestHeaders.Add("Cookie", cookie.Split(';')[0]);
                loginData = loginData.Append(new KeyValuePair<string, string>("__RequestVerificationToken", token)).ToArray();
                content = new FormUrlEncodedContent(loginData);
            }
            var response = await client.PostAsync("/Identity/Account/Login", content);
            // Accept 302 as successful login
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Found)
            {
                throw new System.Exception($"Login failed: {response.StatusCode}");
            }
            return client;
        }

        [Fact]
        public async Task Can_Create_Adresse()
        {
            var client = _factory.CreateClient();
            var getResponse = await client.GetAsync("/Adresses/Create");
            getResponse.EnsureSuccessStatusCode();

            var formData = new MultipartFormDataContent
            {
                { new StringContent("Test Rue"), "Rue" },
                { new StringContent("12345"), "CodePostal" },
                { new StringContent("Test Ville"), "Ville" }
            };
            var postResponse = await client.PostAsync("/Adresses/Create", formData);
            Assert.True(postResponse.StatusCode == System.Net.HttpStatusCode.Redirect || postResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Can_List_BienImmos()
        {
            // Use admin credentials from appsettings.json
            var client = await GetAuthenticatedClientAsync("admin@email.com", "P@ssw0rd");
            var response = await client.GetAsync("/BienImmos");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            Assert.Contains("Bien", html, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
