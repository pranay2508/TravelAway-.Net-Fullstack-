using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string EmailId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
