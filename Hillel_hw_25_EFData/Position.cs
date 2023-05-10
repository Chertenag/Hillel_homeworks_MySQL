using System;
using System.Collections.Generic;

namespace Hillel_hw_25_EFData;

public partial class Position
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
