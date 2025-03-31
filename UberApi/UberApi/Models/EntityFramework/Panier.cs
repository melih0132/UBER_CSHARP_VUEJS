using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_panier_pnr")]
public partial class Panier
{
    [Key]
    [Column("pnr_id")]
    public int IdPanier { get; set; }

    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("pnr_prix")]
    [Precision(5, 2)]
    public decimal? Prix { get; set; }

    [InverseProperty("IdPanierNavigation")]
    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    [InverseProperty("IdPanierNavigation")]
    public virtual ICollection<Contient2> Contient2s { get; set; } = new List<Contient2>();

    [ForeignKey("IdClient")]
    [InverseProperty("Paniers")]
    public virtual Client IdClientNavigation { get; set; } = null!;
}
