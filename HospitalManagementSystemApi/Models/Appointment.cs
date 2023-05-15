using System;
using System.Collections.Generic;

namespace HospitalManagementSystemApi.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Bills = new HashSet<Bill>();
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool? Status { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
