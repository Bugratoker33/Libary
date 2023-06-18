﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMSProje.Models;

[Table("tblUser")]
public partial class TblUser
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    public byte UserTypeId { get; set; }

    [Required]
    [StringLength(200)]
    [Unicode(false)]
    public string UserEmail { get; set; }

    [Required]
    [StringLength(64)]
    [Unicode(false)]
    public string UserPw { get; set; }

    [Required]
    [Column("RegisterIP")]
    [StringLength(16)]
    [Unicode(false)]
    public string RegisterIp { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RegisterDate { get; set; }

    [Required]
    [StringLength(40)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(42)]
    public string LastName { get; set; }

    [Required]
    [StringLength(36)]
    [Unicode(false)]
    public string SaltOfPw { get; set; }

    public int RankId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TblRecords> TblRecords { get; set; } = new List<TblRecords>();

    [ForeignKey("UserTypeId")]
    [InverseProperty("TblUser")]
    public virtual TblUserTypes UserType { get; set; }
}