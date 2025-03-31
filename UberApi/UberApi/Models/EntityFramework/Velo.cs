using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_velo_vel")]
[Index("NumeroVelo", Name = "uq_velo_numero", IsUnique = true)]
public partial class Velo
{
    [Key]
    [Column("vel_id")]
    public int IdVelo { get; set; }

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("vel_numero")]
    public int NumeroVelo { get; set; }

    [Column("vel_estdisponible")]
    public bool EstDisponible { get; set; }

    [ForeignKey("IdAdresse")]
    [InverseProperty("Velos")]
    public virtual Adresse IdAdresseNavigation { get; set; } = null!;

    [InverseProperty("IdVeloNavigation")]
    public virtual ICollection<VeloReservation> VeloReservations { get; set; } = new List<VeloReservation>();
}
