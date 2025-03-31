using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_restaurateur_rst")]
[Index("EmailUser", Name = "uq_restaurateur_mail", IsUnique = true)]
public partial class Restaurateur
{
    [Key]
    [Column("rst_id")]
    public int IdRestaurateur { get; set; }

    [Column("rst_nom")]
    [StringLength(50)]
    public string NomUser { get; set; } = null!;

    [Column("rst_prenom")]
    [StringLength(50)]
    public string PrenomUser { get; set; } = null!;

    [Column("rst_telephone")]
    [StringLength(20)]
    public string Telephone { get; set; } = null!;

    [Column("rst_email")]
    [StringLength(200)]
    public string EmailUser { get; set; } = null!;

    [Column("rst_motdepasse")]
    [StringLength(200)]
    public string MotDePasseUser { get; set; } = null!;

    [InverseProperty("IdRestaurateurNavigation")]
    public virtual ICollection<Etablissement>? Etablissements { get; set; } = new List<Etablissement>();
}
