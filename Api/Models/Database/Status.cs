﻿using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Status
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
