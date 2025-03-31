using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_entreprise_ent")]
public partial class Entreprise
{
    [Key]
    [Column("ent_id")]
    public int IdEntreprise { get; set; }

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("ent_siret")]
    [StringLength(20)]
    public string SiretEntreprise { get; set; } = null!;

    [Column("ent_nom")]
    [StringLength(50)]
    public string NomEntreprise { get; set; } = null!;

    [Column("ent_taille")]
    [StringLength(30)]
    public string Taille { get; set; } = null!;

    [InverseProperty("IdEntrepriseNavigation")]
    public virtual ICollection<Client>? Clients { get; set; } = new List<Client>();

    [InverseProperty("IdEntrepriseNavigation")]
    public virtual ICollection<Coursier>? Coursiers { get; set; } = new List<Coursier>();

    [ForeignKey("IdAdresse")]
    [InverseProperty("Entreprises")]
    public virtual Adresse? IdAdresseNavigation { get; set; } = null!;
}
