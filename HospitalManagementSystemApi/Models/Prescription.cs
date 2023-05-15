using System;
using System.Collections.Generic;

namespace HospitalManagementSystemApi.Models
{
    public partial class Prescription
    {
        public int Id { get; set; }
        public int? AppointmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string PrescriptionDetails { get; set; } = null!;

        public virtual Appointment? Appointment { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
