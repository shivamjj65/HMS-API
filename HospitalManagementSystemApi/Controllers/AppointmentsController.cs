using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemApi.Models;
using System.Net;

namespace HospitalManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly HMSDbContext _context;
        private readonly IConfiguration config;

        public AppointmentsController(HMSDbContext context, IConfiguration config)
        {
            _context = context;
            this.config = config;
        }

        // GET: api/Appointments  -- All appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            return await _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();



        }

        // GET: api/Appointments/5    -- By Appointment ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
          if (_context.Appointments == null)
          {
              return NotFound();
          }
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // GET: api/Appointments/    -- By Patient Id
        [HttpGet("appointmentsByPatientId")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByPatientId(int patientId)
        {
            // Retrieve the appointments using the patient ID.
            var appointments = await _context.Appointments.Where(a => a.PatientId == patientId).Include(a => a.Doctor).ToListAsync();

            return appointments;
        }


        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            //server side validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



          //  if (_context.Appointments == null)
          //{
          //    return Problem("Entity set 'HMSDbContext.Appointments'  is null.");
          //}

            //added code
            //upload Blob
            bool isUploaded = await BlobHelper.UploadBlob(config, appointment);
            if (isUploaded)
            {
                return Ok(new
                {
                    Message = "Appointment add in progress"
                });
            }
            return StatusCode((int)HttpStatusCode.InternalServerError);

            //_context.Appointments.Add(appointment);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_context.Appointments == null)
            {
                return NotFound();
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // Approve
        [HttpPost("approve/{id}")]
        public async Task<ActionResult<Appointment>> ApproveAppointment(int id)
        {
            //server side validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = true;
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return appointment;
        }

        // Reject
        [HttpPost("reject/{id}")]
        public async Task<ActionResult<Appointment>> RejectAppointment(int id)
        {
            //server side validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = false;
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return appointment;
        }
    }
}
