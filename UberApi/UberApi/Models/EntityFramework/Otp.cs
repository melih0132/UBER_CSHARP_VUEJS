using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UberApi.Models.EntityFramework;

[Table("t_e_otp_otp")]
public partial class Otp
{
    [Key]
    [Column("otp_id")]
    public int IdOtp { get; set; }

    [Column("clt_id")]
    public int IdClient { get; set; }

    [Column("otp_code")]
    [StringLength(6)]
    public string CodeOtp { get; set; } = null!;

    [Column("otp_dategeneration", TypeName = "timestamp without time zone")]
    public DateTime DateGeneration { get; set; }

    [Column("otp_dateexpiration", TypeName = "timestamp without time zone")]
    public DateTime DateExpiration { get; set; }

    [Column("otp_utilise")]
    public bool? Utilise { get; set; }

    [ForeignKey("IdClient")]
    [InverseProperty("Otps")]
    public virtual Client IdClientNavigation { get; set; } = null!;
}
