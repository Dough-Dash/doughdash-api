using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doughdash_api.Models;

[Table("Menu")]
public class Menu
{
    [Key] [Column(Order = 0)] public int IDProdotto { get; init; }

    public string Nome { get; set; }
    public decimal Prezzo { get; set; }
    public string Descrizione { get; set; }
}