using System;
using System.Collections.Generic;

namespace Infosys.TravelAway.DAL.Models;

public partial class Property
{
    public int PropertyId { get; set; }

    public int? OwnerId { get; set; }

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public int? PropertyTypeId { get; set; }

    public decimal Price { get; set; }

    public int Bedrooms { get; set; }

    public int Bathrooms { get; set; }

    public bool ParkingIncluded { get; set; }

    public bool PetsAllowed { get; set; }

    public int? FurnishingTypeId { get; set; }

    public int? AvailabilityTypeId { get; set; }

    public string? AdditionalNotes { get; set; }

    public string? Status { get; set; }

    public virtual AvailabilityType? AvailabilityType { get; set; }

    public virtual FurnishingType? FurnishingType { get; set; }

    public virtual Owner? Owner { get; set; }

    public virtual ICollection<PropertyInterest> PropertyInterests { get; set; } = new List<PropertyInterest>();

    public virtual PropertyType? PropertyType { get; set; }
}
