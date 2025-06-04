using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ASimmo.Tests
{
    public class RazorPagesSmokeTests : IClassFixture<CustomWebApplicationFactory<ASimmo.Startup>>
    {
        private readonly CustomWebApplicationFactory<ASimmo.Startup> _factory;
        public RazorPagesSmokeTests(CustomWebApplicationFactory<ASimmo.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Index")]
        [InlineData("/ListePromoteurs")]
        [InlineData("/Projets")]
        public async Task RazorPages_AreReachable(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
