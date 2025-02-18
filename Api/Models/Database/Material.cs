using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Material
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<OrderMaterialService> OrderMaterialServices { get; set; } = new List<OrderMaterialService>();
}
