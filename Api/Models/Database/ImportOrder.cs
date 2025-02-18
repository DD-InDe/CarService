using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class ImportOrder
{
    public int Id { get; set; }

    public string? ClientFullname { get; set; }

    public string? EmployeeFullname { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateOnly? DateComplete { get; set; }

    public string? CarBrand { get; set; }

    public string? CarModel { get; set; }

    public string? CarNumber { get; set; }

    public string? CarVin { get; set; }

    public string? Services { get; set; }

    public string? Materials { get; set; }

    public string? ClientMaterials { get; set; }

    public string? Guid { get; set; }

    public virtual Transaction? Gu { get; set; }
}
