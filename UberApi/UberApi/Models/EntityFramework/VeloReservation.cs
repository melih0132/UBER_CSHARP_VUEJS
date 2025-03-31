using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[PrimaryKey("IdReservationVelo", "IdVelo")]
[Table("t_e_veloreservation_velr")]
public partial class VeloReservation
{
    [Key]
    [Column("velr_id")]
    public int IdReservationVelo { get; set; }

    [Key]
    [Column("vel_id")]
    public int IdVelo { get; set; }

    [Column("velr_dureereservation")]
    public int DureeReservation { get; set; }

    [Column("velr_prixreservation")]
    [Precision(6, 2)]
    public decimal PrixReservation { get; set; }

    [ForeignKey("IdReservationVelo")]
    [InverseProperty("VeloReservations")]
    public virtual Reservation IdReservationVeloNavigation { get; set; } = null!;

    [ForeignKey("IdVelo")]
    [InverseProperty("VeloReservations")]
    public virtual Velo IdVeloNavigation { get; set; } = null!;
}
