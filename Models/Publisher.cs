using System;
using System.Collections.Generic;

namespace MvcEFCore6.Models;

public partial class Publisher
{
    public string PubId { get; set; } = null!;

    public string? PubName { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
