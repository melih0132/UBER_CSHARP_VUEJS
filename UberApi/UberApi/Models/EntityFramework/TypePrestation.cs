using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_typeprestation_tpn")]
public partial class TypePrestation
{
    [Key]
    [Column("tpn_id")]
    public int IdPrestation { get; set; }

    [Column("tpn_libelle")]
    [StringLength(50)]
    public string? LibellePrestation { get; set; }

    [Column("tpn_description")]
    [StringLength(500)]
    public string? DescriptionPrestation { get; set; }

    [Column("tpn_image")]
    [StringLength(300)]
    public string? ImagePrestation { get; set; }

    [InverseProperty("IdPrestationNavigation")]
    public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();

    [ForeignKey("IdPrestation")]
    [InverseProperty("IdPrestations")]
    public virtual ICollection<Vehicule>? IdVehicules { get; set; } = new List<Vehicule>();
}
