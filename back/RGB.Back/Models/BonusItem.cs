﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RGB.Back.Models;

public partial class BonusItem
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BonusId { get; set; }

    public bool Using { get; set; }

    public virtual BonusProduct Bonus { get; set; }

    public virtual Member Member { get; set; }
}