using System;
using System.Collections.Generic;

namespace Business;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? FullName { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
