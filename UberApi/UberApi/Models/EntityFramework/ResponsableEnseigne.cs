using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_responsableenseigne_rse")]
[Index("EmailUser", Name = "uq_responsable_email", IsUnique = true)]
public partial class ResponsableEnseigne
{
    [Key]
    [Column("rse_id")]
    public int IdResponsable { get; set; }

    [Column("rse_nom")]
    [StringLength(50)]
    public string NomUser { get; set; } = null!;

    [Column("rse_prenom")]
    [StringLength(50)]
    public string PrenomUser { get; set; } = null!;

    [Column("rse_telephone")]
    [StringLength(20)]
    public string Telephone { get; set; } = null!;

    [Column("rse_email")]
    [StringLength(200)]
    public string EmailUser { get; set; } = null!;

    [Column("rse_motdepasse")]
    [StringLength(200)]
    public string MotDePasseUser { get; set; } = null!;

    [InverseProperty("IdResponsableNavigation")]
    public virtual ICollection<GestionEtablissement> GestionEtablissements { get; set; } = new List<GestionEtablissement>();
}
