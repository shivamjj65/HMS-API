using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AddToAppointmentFunction
{
    public static class AddToAppFunc
    {
        [FunctionName("AddToAppFunc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Sql("dbo.Appointments", "SqlConnString")] IAsyncCollector<Appointment> appointments,
            ILogger log)
        {
            log.LogInformation("Add to Appointment Function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Appointment apt = JsonConvert.DeserializeObject<Appointment>(requestBody);


            await appointments.AddAsync(apt);
            //await appointments.FlushAsync();

            return new OkObjectResult("Appointment Added");
        }
    }
}
