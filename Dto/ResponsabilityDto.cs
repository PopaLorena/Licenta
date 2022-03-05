using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Licenta.Dto
{
    public class ResponsabilityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public MemberModel Responsible { get; set; }

        public int ResponsibleId { get; set; }

        [JsonIgnore]
        public Event Event { get; set; }

        public int EventId { get; set; }
    }
}
