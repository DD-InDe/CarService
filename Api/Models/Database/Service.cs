using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
}
