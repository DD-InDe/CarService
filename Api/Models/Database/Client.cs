using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Client
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual Person IdNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
