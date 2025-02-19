using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateOnly? DateComplete { get; set; }

    public string? CarBrand { get; set; }

    public string? CarModel { get; set; }

    public string? CarNumber { get; set; }

    public string? CarVin { get; set; }

    public int? StatusId { get; set; }

    public int? ClientId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderMaterialClient> OrderMaterialClients { get; set; } = new List<OrderMaterialClient>();

    public virtual ICollection<OrderMaterialService> OrderMaterialServices { get; set; } = new List<OrderMaterialService>();

    public virtual ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();

    public virtual Status? Status { get; set; }
}
