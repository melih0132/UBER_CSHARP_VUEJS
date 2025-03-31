using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_lieufavori_lfs")]
public partial class LieuFavori
{
    [Key]
    [Column("lfs_id")]
    public int IdLieuFavori { get; set; }

    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("lfs_nom")]
    [StringLength(100)]
    public string NomLieu { get; set; } = null!;

    [ForeignKey("IdAdresse")]
    [InverseProperty("LieuFavoris")]
    public virtual Adresse? IdAdresseNavigation { get; set; } = null!;

    [ForeignKey("IdClient")]
    [InverseProperty("LieuFavoris")]
    public virtual Client? IdClientNavigation { get; set; } = null!;
}
