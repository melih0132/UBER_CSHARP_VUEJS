using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_produit_pdt")]
public partial class Produit
{
    [Key]
    [Column("pdt_id")]
    public int IdProduit { get; set; }

    [Column("pdt_nom")]
    [StringLength(200)]
    public string? NomProduit { get; set; }

    [Column("pdt_prix")]
    [Precision(5, 2)]
    public decimal? PrixProduit { get; set; }

    [Column("pdt_image")]
    [StringLength(300)]
    public string? ImageProduit { get; set; }

    [Column("pdt_description")]
    [StringLength(1500)]
    public string? Description { get; set; }

    [InverseProperty("IdProduitNavigation")]
    public virtual ICollection<Contient2> Contient2s { get; set; } = new List<Contient2>();

    [ForeignKey("IdProduit")]
    [InverseProperty("IdProduits")]
    public virtual ICollection<CategorieProduit> IdCategories { get; set; } = new List<CategorieProduit>();

    [ForeignKey("IdProduit")]
    [InverseProperty("IdProduits")]
    public virtual ICollection<Etablissement> IdEtablissements { get; set; } = new List<Etablissement>();
}
