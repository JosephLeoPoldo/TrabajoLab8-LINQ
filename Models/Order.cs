using System;
using System.Collections.Generic;

namespace Lab8_BryanCama.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int ClientId { get; set; }

    public int ProductId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
