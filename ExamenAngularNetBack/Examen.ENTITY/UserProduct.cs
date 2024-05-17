using Examen.ENTITY;
using System;
using System.Collections.Generic;

namespace Examen.ENTITY;

public partial class UserProduct
{
    public int Id { get; set; }

    public int? IdCustomer { get; set; }

    public int? IdProduct { get; set; }

    public virtual User? IdCustomerNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
