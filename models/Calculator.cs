namespace xptoApi;
public class Tarifa
{
    public int Id { get; set; }
    public string? Origem { get; set; }
    public string? Destino { get; set; }
    public decimal PrecoPorMinuto { get; set; }
}

public class Ligacao
{
    public int Id { get; set; }
    public string? Origem { get; set; }
    public string? Destino { get; set; }
    public int DuracaoMinutos { get; set; }
    public string? PlanoFaleMais { get; set; }
    public decimal ValorComPlano { get; set; }
    public decimal ValorSemPlano { get; set; }
}
