using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_categorieprestation_cpr")]
public partial class CategoriePrestation
{
    [Key]
    [Column("cpr_id")]
    public int IdCategoriePrestation { get; set; }

    [Column("cpr_libelle")]
    [StringLength(50)]
    public string? LibelleCategoriePrestation { get; set; }

    [Column("cpr_description")]
    [StringLength(500)]
    public string? DescriptionCategoriePrestation { get; set; }

    [Column("cpr_image")]
    [StringLength(300)]
    public string? ImageCategoriePrestation { get; set; }

    [ForeignKey("IdCategoriePrestation")]
    [InverseProperty("IdCategoriePrestations")]
    public virtual ICollection<Etablissement>? IdEtablissements { get; set; } = new List<Etablissement>();
}
