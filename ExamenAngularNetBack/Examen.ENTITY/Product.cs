using System;
using System.Collections.Generic;

namespace Examen.ENTITY;

public partial class Product
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public string? Img { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<UserProduct> CustomerProducts { get; } = new List<UserProduct>();

    public virtual ICollection<ProductShop> ProductShops { get; } = new List<ProductShop>();


}
