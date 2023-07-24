using Guide.Report.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Report.Domain.Entities
{
    public class ReportEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime ReportRequestDate { get; set; }
        public TReportStatus ReportStatus { get; set; }
        public IEnumerable<ReportDetailEntity> Details { get; set; }
    }
}
