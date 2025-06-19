using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.TravelAway.Shared.DTO
{
    public class PropertyInterestDTO
    {
        public int InterestId { get; set; }
        public int? PropertyId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime SharedDate { get; set; }
        public DateTime? LastFollowUpDate { get; set; }
        public string? CustomerName { get; set; }
         public string? EmailId { get; set; }         
        public string? ContactNumber { get; set; } 

    }
}