using System;
using System.Collections.Generic;

namespace Business;

public partial class Billing
{
    public int BillId { get; set; }

    public int? AdmissionId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public virtual Admission? Admission { get; set; }
}
