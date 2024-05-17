using System;
using System.Collections.Generic;

namespace Examen.ENTITY;

public partial class ProductShop
{
    public int Id { get; set; }

    public int? IdShop { get; set; }

    public int? IdProduct { get; set; }

    public DateTime? Date { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Shop? IdShopNavigation { get; set; }
}
