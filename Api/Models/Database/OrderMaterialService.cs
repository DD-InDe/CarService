using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class OrderMaterialService
{
    public int Id { get; set; }

    public int? Count { get; set; }

    public int? MaterialId { get; set; }

    public int? OrderId { get; set; }

    public virtual Material? Material { get; set; }

    public virtual Order? Order { get; set; }
}
