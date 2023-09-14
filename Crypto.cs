using System.ComponentModel.DataAnnotations;

namespace tradebot;

public class Crypto
{
    [Key]
    public int id { get; set; }
    public string? name { get; set; }
    public decimal? price { get; set; }
    public DateTime? date { get; set; }
}