using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_coursier_crr")]
[Index("Iban", Name = "uq_coursier_iban", IsUnique = true)]
[Index("EmailUser", Name = "uq_coursier_mail", IsUnique = true)]
[Index("NumeroCarteVtc", Name = "uq_coursier_numcarte", IsUnique = true)]
public partial class Coursier
{
    [Key]
    [Column("crr_id")]
    public int IdCoursier { get; set; }

    [Column("ent_id")]
    public int IdEntreprise { get; set; }

    [Column("adr_id")]
    public int? IdAdresse { get; set; }

    [Column("crr_genreuser")]
    [StringLength(20)]
    public string GenreUser { get; set; } = null!;

    [Column("crr_nomuser")]
    [StringLength(50)]
    public string NomUser { get; set; } = null!;

    [Column("crr_prenomuser")]
    [StringLength(50)]
    public string PrenomUser { get; set; } = null!;

    [Column("crr_datenaissance")]
    public DateOnly DateNaissance { get; set; }

    [Column("crr_telephone")]
    [StringLength(20)]
    public string Telephone { get; set; } = null!;

    [Column("crr_emailuser")]
    [StringLength(200)]
    public string EmailUser { get; set; } = null!;

    [Column("crr_motdepasseuser")]
    [StringLength(200)]
    public string MotDePasseUser { get; set; } = null!;

    [Column("crr_numerocartevtc")]
    [StringLength(12)]
    public string NumeroCarteVtc { get; set; } = null!;

    [Column("crr_iban")]
    [StringLength(30)]
    public string? Iban { get; set; }

    [Column("crr_datedebutactivite")]
    public DateOnly? DateDebutActivite { get; set; }

    [Column("crr_notemoyenne")]
    [Precision(2, 1)]
    public decimal? NoteMoyenne { get; set; }

    [InverseProperty("IdCoursierNavigation")]
    public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();

    [InverseProperty("IdCoursierNavigation")]
    public virtual ICollection<Entretien>? Entretiens { get; set; } = new List<Entretien>();

    [InverseProperty("IdCoursierNavigation")]
    public virtual ICollection<Horaires>? Horaires { get; set; } = new List<Horaires>();

    [ForeignKey("IdAdresse")]
    [InverseProperty("Coursiers")]
    public virtual Adresse? IdAdresseNavigation { get; set; }

    [ForeignKey("IdEntreprise")]
    [InverseProperty("Coursiers")]
    public virtual Entreprise? IdEntrepriseNavigation { get; set; } = null!;

    [InverseProperty("IdCoursierNavigation")]
    public virtual ICollection<ReglementSalaire>? ReglementSalaires { get; set; } = new List<ReglementSalaire>();

    [InverseProperty("IdCoursierNavigation")]
    public virtual ICollection<Vehicule>? Vehicules { get; set; } = new List<Vehicule>();



   
}