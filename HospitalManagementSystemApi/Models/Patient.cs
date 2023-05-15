using System;
using System.Collections.Generic;

namespace HospitalManagementSystemApi.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            Bills = new HashSet<Bill>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; } = null!;
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
