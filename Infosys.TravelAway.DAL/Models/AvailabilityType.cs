using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class AvailabilityType
{
    public int AvailabilityTypeId { get; set; }

    public string AvailabilityStatus { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
