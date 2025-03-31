using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_client_clt")]
[Index("EmailUser", Name = "uq_client_mail", IsUnique = true)]
public partial class Client
{
    [Key]
    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("ent_id")]
    public int? IdEntreprise { get; set; }

    [Column("adr_id")]
    public int? IdAdresse { get; set; }

    [Column("clt_genre")]
    [StringLength(20)]
    public string GenreUser { get; set; } = null!;

    [Column("clt_nom")]
    [StringLength(50)]
    public string NomUser { get; set; } = null!;

    [Column("clt_prenom")]
    [StringLength(50)]
    public string PrenomUser { get; set; } = null!;

    [Column("clt_datenaissance")]
    public DateOnly DateNaissance { get; set; }

    [Column("clt_telephone")]
    [StringLength(20)]
    [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le numéro de téléphone doit contenir 10 chiffres")]
    public string Telephone { get; set; } = null!;

    [Column("clt_email")]
    [StringLength(200)]
    public string EmailUser { get; set; } = null!;

    [Column("clt_motdepasse")]
    [StringLength(200)]
    public string MotDePasseUser { get; set; } = null!;

    [Column("clt_photoprofile")]
    [StringLength(300)]
    public string? PhotoProfile { get; set; }

    [Column("clt_souhaiterecevoirbonplan")]
    public bool? SouhaiteRecevoirBonPlan { get; set; }

    [Column("clt_mfaactivee")]
    public bool? MfaActivee { get; set; }

    [Column("clt_type")]
    [StringLength(20)]
    public string TypeClient { get; set; } = null!;

    [Column("clt_lastconnexion", TypeName = "date")]
    public DateTime? LastConnexion { get; set; }

    [Column("clt_demandesuppression")]
    public bool? DemandeSuppression { get; set; }

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<Facture>? Factures { get; set; } = new List<Facture>();

    [ForeignKey("IdAdresse")]
    [InverseProperty("Clients")]
    public virtual Adresse? IdAdresseNavigation { get; set; }

    [ForeignKey("IdEntreprise")]
    [InverseProperty("Clients")]
    public virtual Entreprise? IdEntrepriseNavigation { get; set; }

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<LieuFavori>? LieuFavoris { get; set; } = new List<LieuFavori>();

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<Otp>? Otps { get; set; } = new List<Otp>();

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<Panier>? Paniers { get; set; } = new List<Panier>();

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<Reservation>? Reservations { get; set; } = new List<Reservation>();

    [ForeignKey("IdClient")]
    [InverseProperty("IdClients")]
    public virtual ICollection<CarteBancaire>? IdCbs { get; set; } = new List<CarteBancaire>();
}
