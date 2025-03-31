using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_entretien_ett")]
public partial class Entretien
{
    [Key]
    [Column("ett_id")]
    public int IdEntretien { get; set; }

    [Column("crr_id")]
    public int IdCoursier { get; set; }

    [Column("ett_date", TypeName = "timestamp without time zone")]
    public DateTime? DateEntretien { get; set; }

    [Column("ett_status")]
    [StringLength(20)]
    public string Status { get; set; } = null!;

    [Column("ett_resultat")]
    [StringLength(20)]
    public string? Resultat { get; set; }

    [Column("ett_rdvlogistiquedate", TypeName = "timestamp without time zone")]
    public DateTime? RdvLogistiqueDate { get; set; }

    [Column("ett_rdvlogistiquelieu")]
    [StringLength(255)]
    public string? RdvLogistiqueLieu { get; set; }

    [ForeignKey("IdCoursier")]
    [InverseProperty("Entretiens")]
    public virtual Coursier? IdCoursierNavigation { get; set; } = null!;
}
