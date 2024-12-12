namespace doughdash_api.Models;

public class Rider
{
    public int Id { get; init; }
    public string Password { get; init; }
    public string Nome { get; set; }
    public string Mezzo { get; set; }
    public int Pizzeria { get; set; }
}