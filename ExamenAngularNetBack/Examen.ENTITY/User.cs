using System;
using System.Collections.Generic;

namespace Examen.ENTITY;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Users { get; set; }

    public string? Passsword { get; set; }

    public virtual ICollection<UserProduct> CustomerProducts { get; } = new List<UserProduct>();
}
