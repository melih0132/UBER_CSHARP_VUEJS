using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_reservation_res")]
public partial class Reservation
{
    [Key]
    [Column("res_id")]
    public int IdReservation { get; set; }

    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("res_date")]
    public DateOnly? DateReservation { get; set; }

    [Column("res_heure")]
    public TimeOnly? HeureReservation { get; set; }

    [Column("res_pourqui")]
    [StringLength(100)]
    public string? PourQui { get; set; }

    [InverseProperty("IdReservationNavigation")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [InverseProperty("IdReservationNavigation")]
    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    [ForeignKey("IdClient")]
    [InverseProperty("Reservations")]
    public virtual Client IdClientNavigation { get; set; } = null!;

    [InverseProperty("IdReservationVeloNavigation")]
    public virtual ICollection<VeloReservation> VeloReservations { get; set; } = new List<VeloReservation>();
}
