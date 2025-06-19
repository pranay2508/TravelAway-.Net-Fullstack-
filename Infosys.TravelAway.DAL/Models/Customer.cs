using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string EmailId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public virtual ICollection<PropertyInterest> PropertyInterests { get; set; } = new List<PropertyInterest>();
}
