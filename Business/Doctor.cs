using System;
using System.Collections.Generic;

namespace Business;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? FullName { get; set; }

    public int? DepartmentId { get; set; }

    public string? Specialization { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
