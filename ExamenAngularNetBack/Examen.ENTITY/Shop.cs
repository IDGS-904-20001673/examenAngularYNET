using System;
using System.Collections.Generic;

namespace Examen.ENTITY;

public partial class Shop
{
    public int Id { get; set; }

    public string? Branch { get; set; }

    public string? Address { get; set; }
    public virtual ICollection<ProductShop> ProductShops { get; } = new List<ProductShop>();

}
