using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_ville_vil")]
public partial class Ville
{
    [Key]
    [Column("vil_id")]
    public int IdVille { get; set; }

    [Column("pys_id")]
    public int? IdPays { get; set; }

    [Column("cp_id")]
    public int? IdCodePostal { get; set; }

    [Column("vil_nom")]
    [StringLength(50)]
    public string NomVille { get; set; } = null!;

    [InverseProperty("IdVilleNavigation")]
    public virtual ICollection<Adresse>? Adresses { get; set; } = new List<Adresse>();

    [ForeignKey("IdCodePostal")]
    [InverseProperty("Villes")]
    public virtual CodePostal? IdCodePostalNavigation { get; set; }

    [ForeignKey("IdPays")]
    [InverseProperty("Villes")]
    public virtual Pays? IdPaysNavigation { get; set; }
}
