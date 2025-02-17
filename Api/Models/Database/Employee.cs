using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Employee
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
