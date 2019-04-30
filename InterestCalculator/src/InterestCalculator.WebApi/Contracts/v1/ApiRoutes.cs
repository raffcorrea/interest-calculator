
namespace InterestCalculator.WebApi.Contracts.v1
{
    public static class ApiRoutes
    {
        private const string ROOT = "api";
        private const string VERSION = "v1";
        private const string BASE = ROOT + "/" + VERSION;

        public const string CALCULA_JUROS = BASE + "/CalculaJuros";
        public const string SHOW_ME_THE_CODE = BASE + "/ShowMeTheCode";
    }
}
