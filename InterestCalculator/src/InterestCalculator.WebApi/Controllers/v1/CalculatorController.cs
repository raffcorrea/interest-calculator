using System;
using System.Globalization;
using InterestCalculator.Domain;
using InterestCalculator.WebApi.Contracts.v1;
using Microsoft.AspNetCore.Mvc;

namespace InterestCalculator.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const string GITHUB_URL = "https://github.com/raffcorrea/interest-calculator";

        /// <summary>
        /// Calcula juros composto considerando valor inicial e a quantidade de meses informado 
        /// </summary>
        /// <param name="valorInicial">Valor inicial a ser utilizado no cálculo</param>
        /// <param name="meses">Quantidade de meses a ser utilizada no cálculo</param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.CALCULA_JUROS)]
        public IActionResult CalculaJuros(decimal valorInicial, int meses)
        {
            Calculator calculator = new Calculator(valorInicial, meses);

            try
            {
                calculator.Calculate();
            }
            catch (Exception)
            {
                //TODO: implement a log strategy to log the exception details
                return BadRequest($"Ocorreu um erro ao tentar efetuar o calculo de juros composto utilizando os seguintes parâmetros: Valor inicial: {valorInicial} e quantidade de meses: {meses}");
            }

            return Ok(calculator.Result.ToString("0.00", new CultureInfo("pt-BR")));
        }

        /// <summary>
        /// Retorna a url do projeto no GitHub 
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.SHOW_ME_THE_CODE)]
        public IActionResult ShowMeTheCode()
        {
            return Ok(GITHUB_URL);
        }
    }
}