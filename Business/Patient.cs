using System;
using System.Collections.Generic;

namespace Business;

public partial class Patient
{
    public int PatientId { get; set; }

    public string? FullName { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
