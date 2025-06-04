using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ASimmo.Tests
{
    public class ControllersSmokeTests : IClassFixture<CustomWebApplicationFactory<ASimmo.Startup>>
    {
        private readonly CustomWebApplicationFactory<ASimmo.Startup> _factory;
        public ControllersSmokeTests(CustomWebApplicationFactory<ASimmo.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Adresses")]
        [InlineData("/BienImmos")]
        [InlineData("/Classifications")]
        [InlineData("/Locaux")]
        [InlineData("/Promoteurs")]
        [InlineData("/TypeBienImmos")]
        [InlineData("/TypeClassifications")]
        [InlineData("/TypePromoteurs")]
        public async Task Controllers_AreReachable(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
