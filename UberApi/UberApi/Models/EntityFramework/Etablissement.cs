using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_etablissement_etb")]
public partial class Etablissement
{
    [Key]
    [Column("etb_id")]
    public int IdEtablissement { get; set; }

    [Column("rst_id")]
    public int IdRestaurateur { get; set; }

    [Column("etb_type")]
    [StringLength(50)]
    public string TypeEtablissement { get; set; } = null!;

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("etb_nom")]
    [StringLength(50)]
    public string? NomEtablissement { get; set; }

    [Column("etb_description")]
    [StringLength(1500)]
    public string? Description { get; set; }

    [Column("etb_image")]
    [StringLength(200)]
    public string? ImageEtablissement { get; set; }

    [Column("etb_livraison")]
    public bool? Livraison { get; set; }

    [Column("etb_aemporter")]
    public bool? AEmporter { get; set; }

    [InverseProperty("IdEtablissementNavigation")]
    public virtual ICollection<Contient2>? Contient2s { get; set; } = new List<Contient2>();

    [InverseProperty("IdEtablissementNavigation")]
    public virtual ICollection<GestionEtablissement>? GestionEtablissements { get; set; } = new List<GestionEtablissement>();

    [InverseProperty("IdEtablissementNavigation")]
    public virtual ICollection<Horaires>? Horaires { get; set; } = new List<Horaires>();

    [ForeignKey("IdAdresse")]
    [InverseProperty("Etablissements")]
    public virtual Adresse? IdAdresseNavigation { get; set; } = null!;

    [ForeignKey("IdRestaurateur")]
    [InverseProperty("Etablissements")]
    public virtual Restaurateur? IdRestaurateurNavigation { get; set; } = null!;

    [ForeignKey("IdEtablissement")]
    [InverseProperty("IdEtablissements")]
    public virtual ICollection<CategoriePrestation>? IdCategoriePrestations { get; set; } = new List<CategoriePrestation>();

    [ForeignKey("IdEtablissement")]
    [InverseProperty("IdEtablissements")]
    public virtual ICollection<Produit>? IdProduits { get; set; } = new List<Produit>();
}
