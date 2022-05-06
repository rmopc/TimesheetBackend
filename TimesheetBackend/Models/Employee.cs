﻿using System;
using System.Collections.Generic;

namespace TimesheetBackend.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Timesheets = new HashSet<Timesheet>();
        }

        public int IdEmployee { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Active { get; set; }
        public string? ImageLink { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
