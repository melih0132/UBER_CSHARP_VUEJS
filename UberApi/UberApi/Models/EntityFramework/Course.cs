using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_course_crs")]
public partial class Course
{
    [Key]
    [Column("crs_id")]
    public int IdCourse { get; set; }

    [Column("crr_id")]
    public int? IdCoursier { get; set; }

    [Column("cb_id")]
    public int? IdCb { get; set; }

    [Column("adr_id")]
    public int IdAdresse { get; set; }

    [Column("res_id")]
    public int IdReservation { get; set; }

    [Column("adr_adr_id")]
    public int AdrIdAdresse { get; set; }

    [Column("tpn_id")]
    public int IdPrestation { get; set; }

    [Column("crs_date")]
    public DateOnly DateCourse { get; set; }

    [Column("crs_heure")]
    public TimeOnly HeureCourse { get; set; }

    [Column("crs_prix")]
    [Precision(8, 2)]
    public decimal PrixCourse { get; set; }

    [Column("crs_statut")]
    [StringLength(20)]
    public string StatutCourse { get; set; } = null!;

    [Column("crs_note")]
    [Precision(2, 1)]
    public decimal? NoteCourse { get; set; }

    [Column("crs_commentaire")]
    [StringLength(1500)]
    public string? CommentaireCourse { get; set; }

    [Column("crs_pourboire")]
    [Precision(8, 2)]
    public decimal? Pourboire { get; set; }

    [Column("crs_distance")]
    [Precision(8, 2)]
    public decimal? Distance { get; set; }

    [Column("crs_temps")]
    public int? Temps { get; set; }

    [ForeignKey("AdrIdAdresse")]
    [InverseProperty("CourseAdrIdAdresseNavigations")]
    public virtual Adresse? AdrIdAdresseNavigation { get; set; } = null!;

    [ForeignKey("IdAdresse")]
    [InverseProperty("CourseIdAdresseNavigations")]
    public virtual Adresse? IdAdresseNavigation { get; set; } = null!;

    [ForeignKey("IdCb")]
    [InverseProperty("Courses")]
    public virtual CarteBancaire? IdCbNavigation { get; set; } = null!;

    [ForeignKey("IdCoursier")]
    [InverseProperty("Courses")]
    public virtual Coursier? IdCoursierNavigation { get; set; }

    [ForeignKey("IdPrestation")]
    [InverseProperty("Courses")]
    public virtual TypePrestation? IdPrestationNavigation { get; set; } = null!;

    [ForeignKey("IdReservation")]
    [InverseProperty("Courses")]
    public virtual Reservation? IdReservationNavigation { get; set; } = null!;
}
