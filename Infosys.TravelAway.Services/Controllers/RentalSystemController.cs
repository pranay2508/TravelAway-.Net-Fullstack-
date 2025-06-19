using Infosys.TravelAway.DAL;
using Infosys.TravelAway.DAL.Models;
using Infosys.TravelAway.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Infosys.TravelAway.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalSystemController : Controller
    {
        private readonly RentalSystemRepository repository;
        private readonly JsonSerializerOptions jsonOptions;

        public RentalSystemController(RentalSystemRepository repository)
        {
            this.repository = repository;
            jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // ---------------- AUTHENTICATION ----------------

        [HttpPost]
        public int RegisterOwner(OwnerDTO owner)
        {
            int result;
            try
            {
                var entity = new Owner
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    EmailId = owner.EmailId,
                    UserPassword = owner.UserPassword,
                    ContactNumber = owner.ContactNumber
                };

                result = repository.RegisterOwner(entity);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        [HttpPost]
        public int LoginOwner(OwnerDTO owner)
        {
            int result;
            try
            {
                result = repository.OwnerLogin(owner.EmailId, owner.UserPassword);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        [HttpPost]
        public int RegisterSeeker(CustomerDTO cust)
        {
            int result;
            try
            {
                var customer = new Customer
                {
                    EmailId = cust.EmailId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    UserPassword = cust.UserPassword,
                    ContactNumber = cust.ContactNumber
                };

                result = repository.RegisterCustomer(customer);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        [HttpPost]
        public int LoginSeeker(CustomerDTO cust)
        {
            int result;
            try
            {
                result = repository.CustomerLogin(cust.EmailId, cust.UserPassword);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        // ---------------- PROPERTY MANAGEMENT ----------------

        [HttpGet]
        public JsonResult GetPropertiesByOwner(int ownerId)
        {
            List<PropertyDTO>? dtoList = new List<PropertyDTO>();

            try
            {
                var properties = repository.GetPropertiesByOwner(ownerId);

                if (properties != null)
                {
                    dtoList = properties.Select(p => new PropertyDTO
                    {
                        PropertyId = p.PropertyId,
                        OwnerId = p.OwnerId,
                        Country = p.Country,
                        State = p.State,
                        City = p.City,
                        PropertyTypeId = p.PropertyTypeId,
                        Price = p.Price,
                        Bedrooms = p.Bedrooms,
                        Bathrooms = p.Bathrooms,
                        ParkingIncluded = p.ParkingIncluded,
                        PetsAllowed = p.PetsAllowed,
                        FurnishingTypeId = p.FurnishingTypeId,
                        AvailabilityTypeId = p.AvailabilityTypeId,
                        AdditionalNotes = p.AdditionalNotes,
                        Status = p.Status
                    }).ToList();
                }
            }
            catch (Exception)
            {
                dtoList = null;
            }

            return Json(dtoList);
        }

        [HttpPost]
        public int AddProperty(PropertyDTO prop)
        {
            int result;
            try
            {
                var property = new Property
                {
                    OwnerId = prop.OwnerId,
                    Country = prop.Country,
                    State = prop.State,
                    City = prop.City,
                    PropertyTypeId = prop.PropertyTypeId,
                    Price = prop.Price,
                    Bedrooms = prop.Bedrooms,
                    Bathrooms = prop.Bathrooms,
                    ParkingIncluded = prop.ParkingIncluded,
                    PetsAllowed = prop.PetsAllowed,
                    FurnishingTypeId = prop.FurnishingTypeId,
                    AvailabilityTypeId = prop.AvailabilityTypeId,
                    AdditionalNotes = prop.AdditionalNotes,
                    Status = prop.Status
                };

                result = repository.AddProperty(property);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        [HttpPut]
        public int UpdateProperty(PropertyDTO prop)
        {
            try
            {
                var property = new Property
                {
                    PropertyId = prop.PropertyId,
                    OwnerId = prop.OwnerId,
                    Country = prop.Country,
                    State = prop.State,
                    City = prop.City,
                    PropertyTypeId = prop.PropertyTypeId,
                    Price = prop.Price,
                    Bedrooms = prop.Bedrooms,
                    Bathrooms = prop.Bathrooms,
                    ParkingIncluded = prop.ParkingIncluded,
                    PetsAllowed = prop.PetsAllowed,
                    FurnishingTypeId = prop.FurnishingTypeId,
                    AvailabilityTypeId = prop.AvailabilityTypeId,
                    AdditionalNotes = prop.AdditionalNotes,
                    Status = prop.Status
                };

                int result = repository.UpdateProperty(property);

                if (result == 0)
                    return 0;
                if (result == -1)
                    return -1;
                if (result == -99)
                    return -99;

                return 1;
            }
            catch (Exception)
            {
                return -99;
            }
        }

        [HttpDelete]
        public int DeleteProperty(int propertyId)
        {
            int result;
            try
            {
                result = repository.DeleteProperty(propertyId);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        // ---------------- PROPERTY VIEWING ----------------

        [HttpGet]
        public JsonResult GetAllAvailable()
        {
            var props = repository.GetAllAvailableProperties();
            return Json(props);
        }

        [HttpGet]
        public JsonResult GetPropertyById(int propertyId)
        {
            var prop = repository.GetPropertyById(propertyId);
            return Json(prop);
        }

        // ---------------- CONTACT SHARING / FOLLOW-UP ----------------

        [HttpPost]
        public int ShareContact(PropertyInterestDTO dto)
        {
            int result;
            try
            {
                var interest = new PropertyInterest
                {
                    PropertyId = dto.PropertyId,
                    CustomerId = dto.CustomerId,
                    SharedDate = dto.SharedDate
                };

                result = repository.ShareContact(interest);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        [HttpPatch]
        public int FollowUp(PropertyInterestDTO dto)
        {
            int result;
            try
            {
                result = repository.FollowUp(dto.InterestId, dto.LastFollowUpDate ?? DateTime.Now);
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

      [HttpGet("{propertyId}")]
public IActionResult GetInterestedSeekers(int propertyId)
{
    try
    {
        var seekers = repository.GetPropertyInterestsByPropertyId(propertyId);

        if (seekers == null || !seekers.Any())
        {
            return NotFound("No interested seekers found for this property.");
        }

        return Ok(seekers);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Internal server error: " + ex.Message);
    }
}
    }
}