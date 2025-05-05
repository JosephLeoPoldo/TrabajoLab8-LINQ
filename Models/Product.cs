using System;
using System.Collections.Generic;

namespace Lab8_BryanCama.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
