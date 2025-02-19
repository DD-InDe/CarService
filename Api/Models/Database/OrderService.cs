using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class OrderService
{
    public int Id { get; set; }

    public int? ExecutorId { get; set; }

    public int? ServiceId { get; set; }

    public int? Count { get; set; }

    public int? OrderId { get; set; }

    public virtual Employee? Executor { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Service? Service { get; set; }
}
