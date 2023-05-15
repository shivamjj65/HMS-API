using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HMSfunctionApp
{

    public static class Function1
    {
        [FunctionName("Function1")]

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
//            await appointments.FlushAsync();


            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";


            return new OkObjectResult("Appointment Added");
        }
    }
}
