using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class FurnishingType
{
    public int FurnishingTypeId { get; set; }

    public string FurnishingStatus { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
