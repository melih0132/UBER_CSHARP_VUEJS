using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[PrimaryKey(nameof(IdPanier), nameof(IdProduit))]
[Table("t_j_contient2_c2")]
public partial class Contient2
{
    [Key]
    [Column("pnr_id")]
    public int IdPanier { get; set; }

    [Key]
    [Column("pdt_id")]
    public int IdProduit { get; set; }

    [Column("etb_id")]
    public int IdEtablissement { get; set; }

    [Column("c2_quantite")]
    public int Quantite { get; set; }

    [ForeignKey("IdEtablissement")]
    [InverseProperty("Contient2s")]
    public virtual Etablissement IdEtablissementNavigation { get; set; } = null!;

    [ForeignKey("IdPanier")]
    [InverseProperty("Contient2s")]
    public virtual Panier IdPanierNavigation { get; set; } = null!;

    [ForeignKey("IdProduit")]
    [InverseProperty("Contient2s")]
    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
