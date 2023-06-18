﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMSProje.Models;

[Table("tblUserTypes")]
public partial class TblUserTypes
{
    [Key]
    public byte UserTypeId { get; set; }

    [Required]
    [StringLength(150)]
    public string UserTpeName { get; set; }

    [InverseProperty("UserType")]
    public virtual ICollection<TblUser> TblUser { get; set; } = new List<TblUser>();
}