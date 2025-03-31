using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_facture_fac")]
public partial class Facture
{
    [Key]
    [Column("fac_id")]
    public int IdFacture { get; set; }

    [Column("res_id")]
    public int? IdReservation { get; set; }

    [Column("cmd_id")]
    public int? IdCommande { get; set; }

    [Column("pys_id")]
    public int IdPays { get; set; }

    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("fac_montantreglement")]
    [Precision(5, 2)]
    public decimal? MontantReglement { get; set; }

    [Column("fac_datefacture")]
    public DateOnly? DateFacture { get; set; }

    [Column("fac_quantite")]
    public int? Quantite { get; set; }

    [ForeignKey("IdClient")]
    [InverseProperty("Factures")]
    public virtual Client? IdClientNavigation { get; set; } = null!;

    [ForeignKey("IdCommande")]
    [InverseProperty("Factures")]
    public virtual Commande? IdCommandeNavigation { get; set; }

    [ForeignKey("IdPays")]
    [InverseProperty("Factures")]
    public virtual Pays? IdPaysNavigation { get; set; } = null!;

    [ForeignKey("IdReservation")]
    [InverseProperty("Factures")]
    public virtual Reservation? IdReservationNavigation { get; set; }
}
