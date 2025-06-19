using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class PropertyInterest
{
    public int InterestId { get; set; }

    public int? PropertyId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime SharedDate { get; set; }

    public DateTime? LastFollowUpDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Property? Property { get; set; }
}
