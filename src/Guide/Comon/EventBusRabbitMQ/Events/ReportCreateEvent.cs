using EventBusRabbitMQ.Comon;
using EventBusRabbitMQ.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events
{
    public class ReportCreateEvent : IEvent
    {
        public string Id { get; set; }
        public string ReportType { get; set; }
        public DateTime ReportRequestDate { get; set; }
        public TReportStatus ReportStatus { get; set; }
      }
}
