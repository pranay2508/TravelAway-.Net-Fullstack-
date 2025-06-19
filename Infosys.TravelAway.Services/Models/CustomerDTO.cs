using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.TravelAway.Services.Models
{
    public class CustomerDTO
    {
     public int CustomerId { get; set; }

    public string EmailId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;
    }
}