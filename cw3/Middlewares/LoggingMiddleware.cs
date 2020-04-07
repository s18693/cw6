using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw3.Middlewares
{
    public class LoggingMiddleware
    {
        string fileName = "requestsLog.txt";

        private readonly RequestDelegate _next;
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Our code 
            //Sprawdzam na konsoli kolejnosc i liczbe
            System.Console.WriteLine("Try to save");

            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string metoda = httpContext.Request.Method.ToString();
                string sciezka = httpContext.Request.Path;
                string querystring = httpContext.Request.QueryString.ToString();
                
                string body = string.Empty;

                using (StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                }

                //zapis do pliku
                System.Console.WriteLine($"{metoda} {sciezka} {querystring} {body}");
                File.WriteAllText(fileName, $"{metoda} {sciezka} {querystring} {body}");

            }
            await _next(httpContext);
        }

    }
}
