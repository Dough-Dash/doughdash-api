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


    public string? Descrizione { get; set; } // Nullable if the column can be null
    public TimeSpan? Orario { get; set; }
    public byte[]? Base64Image { get; set; }
    public int? Pizzeria { get; set; } // Nullable integer
    public int? Cliente { get; set; }
    public int? Rider { get; set; }
}