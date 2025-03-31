using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_categorieproduit_cpt")]
public partial class CategorieProduit
{
    [Key]
    [Column("cpt_id")]
    public int IdCategorie { get; set; }

    [Column("cpt_nom")]
    [StringLength(100)]
    public string? NomCategorie { get; set; }

    [ForeignKey("IdCategorie")]
    [InverseProperty("IdCategories")]
    public virtual ICollection<Produit>? IdProduits { get; set; } = new List<Produit>();
}
