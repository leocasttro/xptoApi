using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace xptoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculoChamadaController : ControllerBase
    {
        // Definindo uma lista de tarifas
        private static readonly List<Tarifa> Tarifas = new List<Tarifa>
        {
            new Tarifa {Origem = "011", Destino = "016", PrecoPorMinuto = 1.9m},
            new Tarifa {Origem = "016", Destino = "011", PrecoPorMinuto = 2.9m},
            new Tarifa {Origem = "011", Destino = "017", PrecoPorMinuto = 1.7m},
            new Tarifa {Origem = "017", Destino = "011", PrecoPorMinuto = 2.7m},
            new Tarifa {Origem = "011", Destino = "018", PrecoPorMinuto = 0.9m},
            new Tarifa {Origem = "018", Destino = "011", PrecoPorMinuto = 1.9m}
        };

        [HttpPost]
        public ActionResult<CalculoChamadaResultado> Post(CalculoChamadaRequest request)
        {
            // Buscando a tarifa correspondente
            var tarifa = Tarifas.Find(t => t.Origem == request.Origem && t.Destino == request.Destino);

            // Validando a tarifa
            if (tarifa == null)
            {
                return BadRequest("Não foi possível encontrar uma tarifa para a origem e destino informados");
            }

            // Calculando o valor da chamada sem o plano FaleMais
            var valorSemPlano = tarifa.PrecoPorMinuto * request.Tempo;

            // Calculando o valor da chamada com o plano FaleMais
            var valorComPlano = 0m;
            var minutosExcedentes = request.Tempo - request.Plano.Minutos;
            if (minutosExcedentes > 0)
            {
                valorComPlano = tarifa.PrecoPorMinuto * request.Plano.FatorAcrescimo * minutosExcedentes;
            }

            // Retornando o resultado
            return new CalculoChamadaResultado
            {
                ValorSemPlano = valorSemPlano,
                ValorComPlano = valorComPlano
            };
        }
    }

    // Definindo as classes de request e resultado
    public class CalculoChamadaRequest
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Tempo { get; set; }
        public PlanoFaleMais Plano { get; set; }
    }

    public class CalculoChamadaResultado
    {
        public decimal ValorSemPlano { get; set; }
        public decimal ValorComPlano { get; set; }
    }

    public class Tarifa
    {
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal PrecoPorMinuto { get; set; }
    }

    public class PlanoFaleMais
    {
        public int Minutos { get; set; }
        public decimal FatorAcrescimo { get; set; }
    }
}
