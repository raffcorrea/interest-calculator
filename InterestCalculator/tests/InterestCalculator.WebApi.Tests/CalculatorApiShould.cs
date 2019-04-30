using InterestCalculator.WebApi.Contracts.v1;
using InterestCalculator.WebApi.Tests.Provider;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InterestCalculator.WebApi.Tests
{
    public class CalculatorApiShould
    {

        [Fact]
        public async Task GetGitHubProjectUrl()
        {
            string actualUrlJson = string.Empty;
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync(ApiRoutes.SHOW_ME_THE_CODE);

                response.EnsureSuccessStatusCode();

                actualUrlJson = response.Content.ReadAsStringAsync().Result;

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("https://github.com/raffcorrea/interest-calculator", JsonConvert.DeserializeObject<string>(actualUrlJson));
            }
        }

        [Fact]
        public async Task ReturnTheExpectedPositiveInterestCalculationResult()
        {
            string actualValueString = string.Empty;
            decimal actualValueDecimal = 0;
            string parameters = "?valorinicial=100&meses=5";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{ApiRoutes.CALCULA_JUROS}/{parameters}");

                response.EnsureSuccessStatusCode();

                actualValueString = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                actualValueDecimal = Convert.ToDecimal(actualValueString, new CultureInfo("pt-BR"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(105.10m, actualValueDecimal);
            }
        }

        [Fact]
        public async Task ReturnTheExpectedNegativeInterestCalculationResult()
        {
            string actualValueString = string.Empty;
            decimal actualValueDecimal = 0;
            string parameters = "?valorinicial=-54&meses=2";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{ApiRoutes.CALCULA_JUROS}/{parameters}");

                response.EnsureSuccessStatusCode();

                actualValueString = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                actualValueDecimal = Convert.ToDecimal(actualValueString, new CultureInfo("pt-BR"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(-55.08m, actualValueDecimal);
            }
        }

        [Fact]
        public async Task ReturnZeroInterestCalculationResult()
        {
            string actualValueString = string.Empty;
            decimal actualValueDecimal = 0;
            string parameters = "?valorinicial=0&meses=0";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{ApiRoutes.CALCULA_JUROS}/{parameters}");

                response.EnsureSuccessStatusCode();

                actualValueString = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);
                actualValueDecimal = Convert.ToDecimal(actualValueString, new CultureInfo("pt-BR"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(0, actualValueDecimal);
            }
        }

        [Fact]
        public async Task ReturnBadRequestDuringRequestForInterestCalculation()
        {
            string parameters = "?valorinicial=9999999999999999999&meses=0999999999999999999999999999999999";
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"{ApiRoutes.CALCULA_JUROS}/{parameters}");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
