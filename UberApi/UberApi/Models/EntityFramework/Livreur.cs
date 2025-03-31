using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_livreur_livr")]
[Index("Iban", Name = "uq_livreur_iban", IsUnique = true)]
[Index("EmailUser", Name = "uq_livreur_mail", IsUnique = true)]
public partial class Livreur
{
    [Key]
    [Column("livr_id")]
    public int IdLivreur { get; set; }

    [Column("ent_id")]
    public int IdEntreprise { get; set; }

    [Column("adr_id")]
    public int? IdAdresse { get; set; }

    [Column("livr_genreuser")]
    [StringLength(20)]
    public string GenreUser { get; set; } = null!;

    [Column("livr_nomuser")]
    [StringLength(50)]
    public string NomUser { get; set; } = null!;

    [Column("livr_prenomuser")]
    [StringLength(50)]
    public string PrenomUser { get; set; } = null!;

    [Column("livr_datenaissance")]
    public DateOnly DateNaissance { get; set; }

    [Column("livr_telephone")]
    [StringLength(20)]
    public string Telephone { get; set; } = null!;

    [Column("livr_emailuser")]
    [StringLength(200)]
    public string EmailUser { get; set; } = null!;

    [Column("livr_motdepasseuser")]
    [StringLength(200)]
    public string MotDePasseUser { get; set; } = null!;

    [Column("livr_iban")]
    [StringLength(30)]
    public string? Iban { get; set; }

    [Column("livr_datedebutactivite")]
    public DateOnly? DateDebutActivite { get; set; }

    [Column("livr_notemoyenne")]
    [Precision(2, 1)]
    public decimal? NoteMoyenne { get; set; }

    [InverseProperty("IdLivreurNavigation")]
    public virtual ICollection<Commande>? Commandes { get; set; } = new List<Commande>();

    [InverseProperty("IdLivreurNavigation")]
    public virtual ICollection<Horaires>? Horaires { get; set; } = new List<Horaires>();
}