using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Hangfire;
using Monitory.Core.Models;

namespace Monitory.Tasks.Checks
{
    public class WebCheckHelper
    {
        #region CUD
        public static void CreateRecurringEvent(WebCheck check)
        {
            // Create a recurring event
            string checkID = check.WebCheckID.ToString();
            RecurringJob.AddOrUpdate(checkID, () => CheckDomainForHttps(check), Cron.MinuteInterval(check.Delay));
        }

        public static void UpdateRecurringEvent(WebCheck check)
        {
            // update existing event
            string checkID = check.WebCheckID.ToString();
            RecurringJob.AddOrUpdate(checkID, () => CheckDomainForHttps(check), Cron.MinuteInterval(check.Delay));
        }

        public static void DeleteRecurringEvent(WebCheck check)
        {
            // Delete existing event
            string checkID = check.WebCheckID.ToString();
            RecurringJob.RemoveIfExists(checkID);
        }
        #endregion

        #region Checks
        public static void CheckDomainForHttp(WebCheck check)
        {
            string domain = check.Domain;
            var request = (HttpWebRequest)WebRequest.Create("https://" + domain);
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    // Success
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        Console.WriteLine(streamReader.ReadToEnd());
                }
            }
        }

        public static void CheckDomainForHttps(WebCheck check)
        {
            string domain = check.Domain;

            var request = (HttpWebRequest)WebRequest.Create("https://" + domain);
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    // Success
                    Console.WriteLine("------------------------ ok ------------------------");

                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                        Console.WriteLine(streamReader.ReadToEnd());
                }
            }
        }
        #endregion
    }
}
