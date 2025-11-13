using System;
using System.Collections.Generic;

namespace Business;

public partial class Room
{
    public int RoomId { get; set; }

    public string? RoomNumber { get; set; }

    public string? RoomType { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}
