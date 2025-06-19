using Infosys.TravelAway.DAL.Models;
using Infosys.TravelAway.Shared.DTO;



namespace Infosys.TravelAway.DAL
{
    public class RentalSystemRepository
    {
        private readonly RentalSystemDbContext _context;

        public RentalSystemRepository(RentalSystemDbContext context)
        {
            _context = context;
        }

        // ---------------- AUTH ----------------

        public int RegisterCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public int RegisterOwner(Owner owner)
        {
            try
            {
                _context.Owners.Add(owner);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public int CustomerLogin(string email, string password)
        {
            try
            {
                var customerId = (from cust in _context.Customers
                                  where cust.EmailId == email && cust.UserPassword == password
                                  select cust.CustomerId).FirstOrDefault();
                return customerId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public int OwnerLogin(string email, string password)
        {
            try
            {
                var ownerId = (from own in _context.Owners
                               where own.EmailId == email && own.UserPassword == password
                               select own.OwnerId).FirstOrDefault();
                return ownerId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        // ---------------- PROPERTY OWNER ----------------

        public List<Property>? GetPropertiesByOwner(int ownerId)
        {
            try
            {
                return (from prop in _context.Properties
                        where prop.OwnerId == ownerId
                        select prop).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int AddProperty(Property property)
        {
            try
            {
                _context.Properties.Add(property);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public int UpdateProperty(Property property)
        {
            try
            {

                var existingProperty = _context.Properties.FirstOrDefault(p => p.PropertyId == property.PropertyId);

                if (existingProperty == null)
                    return 0;

                if (existingProperty.OwnerId != property.OwnerId)
                    return -1;


                existingProperty.Country = property.Country;
                existingProperty.State = property.State;
                existingProperty.City = property.City;
                existingProperty.PropertyTypeId = property.PropertyTypeId;
                existingProperty.Price = property.Price;
                existingProperty.Bedrooms = property.Bedrooms;
                existingProperty.Bathrooms = property.Bathrooms;
                existingProperty.ParkingIncluded = property.ParkingIncluded;
                existingProperty.PetsAllowed = property.PetsAllowed;
                existingProperty.FurnishingTypeId = property.FurnishingTypeId;
                existingProperty.AvailabilityTypeId = property.AvailabilityTypeId;
                existingProperty.AdditionalNotes = property.AdditionalNotes;
                existingProperty.Status = property.Status;

                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }


        public int DeleteProperty(int propertyId)
        {
            try
            {
                var interests = _context.PropertyInterests
                                        .Where(i => i.PropertyId == propertyId)
                                        .ToList();
                if (interests.Any())
                {
                    _context.PropertyInterests.RemoveRange(interests);
                }

                var property = _context.Properties.Find(propertyId);
                if (property == null)
                {
                    return 0;
                }
                _context.Properties.Remove(property);

                _context.SaveChanges();

                return 1; 
            }
            catch (Exception)
            {
                return -99; 
            }
        }


        // ---------------- PROPERTY VIEW FOR SEEKERS ----------------

        public List<Property>? GetAllAvailableProperties()
        {
            try
            {
                return _context.Properties.Where(p => p.Status == "Available").ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Property? GetPropertyById(int propertyId)
        {
            try
            {
                return _context.Properties.FirstOrDefault(p => p.PropertyId == propertyId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // ---------------- CONTACT SHARING ----------------

        public int ShareContact(PropertyInterest interest)
        {
            try
            {
                _context.PropertyInterests.Add(interest);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public int FollowUp(int interestId, DateTime followUpDate)
        {
            try
            {
                var interest = _context.PropertyInterests.Find(interestId);
                if (interest != null)
                {
                    interest.LastFollowUpDate = followUpDate;
                    return _context.SaveChanges();
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -99;
            }
        }

        public List<PropertyInterestDTO> GetPropertyInterestsByPropertyId(int propertyId)
        {
            try
            {
                var result = (from pi in _context.PropertyInterests
                              join c in _context.Customers on pi.CustomerId equals c.CustomerId
                              where pi.PropertyId == propertyId
                              select new PropertyInterestDTO
                              {
                                  InterestId = pi.InterestId,
                                  PropertyId = pi.PropertyId,
                                  CustomerId = pi.CustomerId,
                                  CustomerName = c.FirstName, 
                                  EmailId = c.EmailId,
                                  ContactNumber = c.ContactNumber,
                                  SharedDate = pi.SharedDate,
                                  LastFollowUpDate = pi.LastFollowUpDate
                              }).ToList();

                return result;
            }
            catch
            {
                return new List<PropertyInterestDTO>();
            }
        }


    }
}
