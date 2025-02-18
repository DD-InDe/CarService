using System;
using System.Collections.Generic;

namespace Api.Models.Database;

public partial class Person
{
    public int Id { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }
}
