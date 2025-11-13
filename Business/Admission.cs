using System;
using System.Collections.Generic;

namespace Business;

public partial class Admission
{
    public int AdmissionId { get; set; }

    public int? PatientId { get; set; }

    public int? RoomId { get; set; }

    public DateTime AdmissionDate { get; set; }   // ✅ DateTime
    public DateTime? DischargeDate { get; set; }  // ✅ DateTime?
    public string? Status { get; set; }


    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual Patient? Patient { get; set; }

    public virtual Room? Room { get; set; }
}
