using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class OrderMaterialClient
{
    public int Id { get; set; }

    public int? Count { get; set; }

    public string? Name { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
