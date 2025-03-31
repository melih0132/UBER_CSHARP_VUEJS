using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_horaires_hor")]
public partial class Horaires
{
    [Key]
    [Column("hor_id")]
    public int IdHoraires { get; set; }

    [Column("etb_id")]
    public int? IdEtablissement { get; set; }

    [Column("crr_id")]
    public int? IdCoursier { get; set; }

    [Column("livr_id")]
    public int? IdLivreur { get; set; }

    [Column("hor_joursemaine")]
    [StringLength(9)]
    public string JourSemaine { get; set; } = null!;

    [Column("hor_heuredebut", TypeName = "time with time zone")]
    public DateTimeOffset? HeureDebut { get; set; }

    [Column("hor_heurefin", TypeName = "time with time zone")]
    public DateTimeOffset? HeureFin { get; set; }

    [ForeignKey("IdCoursier")]
    [InverseProperty("Horaires")]
    public virtual Coursier? IdCoursierNavigation { get; set; }

    [ForeignKey("IdEtablissement")]
    [InverseProperty("Horaires")]
    public virtual Etablissement? IdEtablissementNavigation { get; set; }

    [ForeignKey("IdLivreur")]
    [InverseProperty("Horaires")]
    public virtual Livreur? IdLivreurNavigation { get; set; }
}