using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace doughdash_api.Models;

[Table("DettagliOrdine")]
public class DettagliOrdine
{
    [Key] [Column(Order = 0)] public required int IdOrdine { get; init; }

    [Key] [Column(Order = 1)] public required int Prodotto { get; init; }

    public int Qta { get; set; }
}