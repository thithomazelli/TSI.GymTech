using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSI.GymTech.Entity.Models;
using TSI.GymTech.Manager.EntityManagers;

namespace TSI.GymTech.WebAPI.WebAPI
{
    public class AccessLogController : ApiController
    {
        AccessLogManager _accessLogManager;

        public AccessLogController()
        {
            _accessLogManager = new AccessLogManager();
        }

        public IHttpActionResult GetAccessLogGroupBy()
        {
            var items = _accessLogManager.FindAll().Data
                .Where(_ => _.AccessType != Entity.Enumerates.GateStatusType.Denied)
                            //&& _.Person.ProfileType == Entity.Enumerates.PersonType.Student)
                .GroupBy(_ => _.CreateDate?.ToString("dd/MMM (ddd)"))
                .Select(group => new { Date = group.Key, Qty = group.Count() })
                .Reverse()
                .Take(12)
                .Reverse()
                .ToList();

            List<string> labels = new List<string>();
            List<int> data = new List<int>();

            foreach (var item in items)
            {
                labels.Add(item.Date);
                data.Add(item.Qty);
            }

            return Json(new {
                Success = true,
                Labels = labels.ToArray(),
                Data = data.ToArray()
            });
        }
    }
}