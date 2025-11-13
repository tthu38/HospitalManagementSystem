using System;
using System.Collections.Generic;

namespace Business;

public partial class Billing
{
    public int BillId { get; set; }

    public int? AdmissionId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? PaymentDate { get; set; }  // ✅ đúng kiểu


    public string? PaymentMethod { get; set; }

    public bool? Paid { get; set; }

    public virtual Admission? Admission { get; set; }
}
