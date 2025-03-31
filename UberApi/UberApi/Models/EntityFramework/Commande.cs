using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_commande_cmd")]
public partial class Commande
{
    [Key]
    [Column("cmd_id")]
    public int IdCommande { get; set; }

    [Column("pnr_id")]
    public int IdPanier { get; set; }

    [Column("livr_id")]
    public int? IdLivreur { get; set; }

    [Column("cb_id")]
    public int? IdCb { get; set; }

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("cmd_prix")]
    [Precision(5, 2)]
    public decimal PrixCommande { get; set; }

    [Column("cmd_temps")]
    public int TempsCommande { get; set; }

    [Column("cmd_heurecreation", TypeName = "date")]
    public DateTime HeureCreation { get; set; }

    [Column("cmd_heure", TypeName = "date")]
    public DateTime HeureCommande { get; set; }

    [Column("cmd_estlivraison")]
    public bool EstLivraison { get; set; }

    [Column("cmd_statut")]
    [StringLength(40)]
    public string StatutCommande { get; set; } = null!;

    [Column("cmd_refusdemandee")]
    public bool RefusDemandee { get; set; }

    [Column("cmd_remboursementeffectue")]
    public bool RemboursementEffectue { get; set; }

    [InverseProperty("IdCommandeNavigation")]
    public virtual ICollection<Facture>? Factures { get; set; } = new List<Facture>();

    [ForeignKey("IdLivreur")]
    [InverseProperty("Commandes")]
    public virtual Livreur? IdLivreurNavigation { get; set; }

    [ForeignKey("IdPanier")]
    [InverseProperty("Commandes")]
    public virtual Panier? IdPanierNavigation { get; set; } = null!;
}
