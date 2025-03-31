using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_reglementsalaire_rsa")]
public partial class ReglementSalaire
{
    [Key]
    [Column("rsa_id")]
    public int IdReglement { get; set; }

    [Column("crr_id")]
    public int? IdCoursier { get; set; }

    [Column("livr_id")]
    public int? IdLivreur { get; set; }

    [Column("rsa_montantreglement")]
    [Precision(6, 2)]
    public decimal? MontantReglement { get; set; }

    [ForeignKey("IdCoursier")]
    [InverseProperty("ReglementSalaires")]
    public virtual Coursier? IdCoursierNavigation { get; set; }
}
