using System;
using System.Collections.Generic;

namespace HospitalManagementSystemApi.Models
{
    public partial class Bill
    {
        public int Id { get; set; }
        public int? AppointmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal? Amount { get; set; }

        public virtual Appointment? Appointment { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
