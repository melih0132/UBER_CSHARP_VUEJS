using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_pays_pys")]
[Index("NomPays", Name = "uq_nompays", IsUnique = true)]
public partial class Pays
{
    [Key]
    [Column("pys_id")]
    public int IdPays { get; set; }

    [Column("pys_nom")]
    [StringLength(50)]
    public string NomPays { get; set; } = null!;

    [Column("pys_pourcentagetva")]
    [Precision(4, 2)]
    public decimal? PourcentageTva { get; set; }

    [InverseProperty("IdPaysNavigation")]
    public virtual ICollection<CodePostal>? CodePostals { get; set; } = new List<CodePostal>();

    [InverseProperty("IdPaysNavigation")]
    public virtual ICollection<Facture>? Factures { get; set; } = new List<Facture>();

    [InverseProperty("IdPaysNavigation")]
    public virtual ICollection<Ville>? Villes { get; set; } = new List<Ville>();
}
