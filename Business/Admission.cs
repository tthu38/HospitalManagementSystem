using System;
using System.Collections.Generic;

namespace Business;

public partial class Admission
{
    public int AdmissionId { get; set; }

    public int? PatientId { get; set; }

    public int? RoomId { get; set; }

    public DateOnly? AdmissionDate { get; set; }

    public DateOnly? DischargeDate { get; set; }

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual Patient? Patient { get; set; }

    public virtual Room? Room { get; set; }
}
