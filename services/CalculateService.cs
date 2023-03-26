public class CalculateService 
{
  private readonly List<Tariff> tariffs;
  private readonly List<Plan> plans;

  public CalculateService()
  {
    tariffs = new List<Tariff>
    {
      new Tariff {Origin = "011", Destination = "016", PriceMinute = 1.9m},
      new Tariff {Origin = "016", Destination = "011", PriceMinute = 2.9m},
      new Tariff {Origin = "011", Destination = "017", PriceMinute = 1.7m},
      new Tariff {Origin = "017", Destination = "011", PriceMinute = 2.7m},
      new Tariff {Origin = "011", Destination = "018", PriceMinute = 0.9m},
      new Tariff {Origin = "018", Destination = "011", PriceMinute = 1.9m}
    };

    plans = new List<Plan> 
    {
      new Plan {Name = "FaleMais30", Minute = 30},
      new Plan {Name = "FaleMais60", Minute = 60},
      new Plan {Name = "FaleMais120", Minute = 120}
    };
  }

  public Result CalculateValue(Call request)
  {
                // Buscando a tarifa correspondente
            var tariff = tariffs.Find(t => t.Origin == request.Origin && t.Destination == request.Destination);

            // Validando a tarifa
            if (tariff == null)
            {
              throw new ArgumentException("Não foi possível encontrar uma tarifa para a origem e destino informados");
            }

            if (request.Plan == null) {
              throw new ArgumentException("Plano inválido");
            }

            var selectPlan = plans.Find(p => p.Name == request.Plan.Name);

            if (selectPlan == null) 
            {
              throw new ArgumentException("Não foi possível encontrar um plano");
            }

            // Calculando o valor da chamada sem o plano FaleMais
            var valorSemPlano = tariff.PriceMinute * request.Temp;

            // Calculando o valor da chamada com o plano FaleMais
            var valorComPlano = 0m;
            var minutosExcedentes = request.Temp - selectPlan.Minute;
            if (minutosExcedentes > 0)
            {
                valorComPlano = (tariff.PriceMinute * minutosExcedentes) * (1 + request.Plan.Addition);
            }

            // Retornando o resultado
            return new Result
            {
                ValorSemPlano = valorSemPlano,
                ValorComPlano = valorComPlano
            };
  }
}