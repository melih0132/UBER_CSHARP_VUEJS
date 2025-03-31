using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_cartebancaire_ctbr")]
[Index("NumeroCb", Name = "carte_bancaire_numerocb_key", IsUnique = true)]
public partial class CarteBancaire
{
    [Key]
    [Column("cb_id")]
    public int IdCb { get; set; }

    [Column("cb_numero")]
    public string NumeroCb { get; set; } = null!;

    [Column("cb_dateexpire")]
    public DateOnly DateExpireCb { get; set; }

    [Column("cb_cryptogramme")]
    public string Cryptogramme { get; set; } = null!;

    [Column("cb_typecarte")]
    [StringLength(30)]
    public string TypeCarte { get; set; } = null!;

    [Column("cb_typereseaux")]
    [StringLength(30)]
    public string TypeReseaux { get; set; } = null!;

    [InverseProperty("IdCbNavigation")]
    public virtual ICollection<Course>? Courses { get; set; } = new List<Course>();

    [ForeignKey("IdCb")]
    [InverseProperty("IdCbs")]
    public virtual ICollection<Client>? IdClients { get; set; } = new List<Client>();
}
