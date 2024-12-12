using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doughdash_api.Models;

[Table("Ordini")]
public class Ordine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column(Order = 0)]
    public int IDOrdine { get; set; } // Changed from init to set

    public string Descrizione { get; set; }
    public bool Stato { get; set; }
    public TimeSpan? Orario { get; set; }
    public string Base64Image { get; set; }
    public int Pizzeria { get; set; }
    public int Cliente { get; set; }
    public int Rider { get; set; }
}