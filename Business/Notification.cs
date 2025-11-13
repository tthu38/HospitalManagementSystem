using System;
using System.Collections.Generic;

namespace Business;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? RecipientRole { get; set; }

    public int? RecipientId { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }
}
