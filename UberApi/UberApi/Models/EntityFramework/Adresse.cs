using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_adresse_adr")]
public partial class Adresse
{
    [Key]
    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("vil_id")]
    public int? IdVille { get; set; }

    [Column("adr_libelle")]
    [StringLength(100)]
    public string LibelleAdresse { get; set; } = null!;

    [Column("adr_latitude")]
    [StringLength(100)]
    public string? Latitude { get; set; }

    [Column("adr_longitude")]
    [StringLength(100)]
    public string? Longitude { get; set; }

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Client>? Clients { get; set; } = new List<Client>();

    [InverseProperty("AdrIdAdresseNavigation")]
    public virtual ICollection<Course>? CourseAdrIdAdresseNavigations { get; set; } = new List<Course>();

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Course>? CourseIdAdresseNavigations { get; set; } = new List<Course>();

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Coursier>? Coursiers { get; set; } = new List<Coursier>();

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Entreprise>? Entreprises { get; set; } = new List<Entreprise>();

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Etablissement>? Etablissements { get; set; } = new List<Etablissement>();

    [ForeignKey("IdVille")]
    [InverseProperty("Adresses")]
    public virtual Ville? IdVilleNavigation { get; set; }

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<LieuFavori>? LieuFavoris { get; set; } = new List<LieuFavori>();

    [InverseProperty("IdAdresseNavigation")]
    public virtual ICollection<Velo>? Velos { get; set; } = new List<Velo>();
}
