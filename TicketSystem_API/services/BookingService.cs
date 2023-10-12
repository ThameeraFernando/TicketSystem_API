using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using TicketSystem_API.Models;

namespace TicketSystem_API.services
{
    public class BookingService : IBookingService
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingService(IMongoClient mongoClient, IBookingStoreDatabaseSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bookings = database.GetCollection<Booking>(settings.BookingCollectionName);
        }

        public List<Booking> Get()
        {
            return _bookings.Find(booking => true).ToList();
        }

        public Booking Get(string id)
        {
            return _bookings.Find(booking => booking.id == id).FirstOrDefault();
        }

        public Booking Create(Booking booking)
        {
            // Check if there are already 4 bookings with the same NIC
            var existingBookings = _bookings.Find(b => b.nic == booking.nic).ToList();
            if (existingBookings.Count >= 4)
            {
                throw new Exception("You can only have a maximum of 4 bookings with the same NIC.");
            }
            _bookings.InsertOne(booking);
            return booking;
        }

        public void Update(string id, Booking booking)
        {
            _bookings.ReplaceOne(b => b.id == id, booking);
        }

        public void Patch(string id, Booking partialBooking)
        {
            var filter = Builders<Booking>.Filter.Eq(b => b.id, id);

            var updateDefinition = Builders<Booking>.Update
                .Set(b => b.startingPoint, partialBooking.startingPoint)
                .Set(b => b.destination, partialBooking.destination)
                .Set(b => b.date, partialBooking.date)
                .Set(b => b.arrivalTimeToEndStation, partialBooking.arrivalTimeToEndStation)
                .Set(b => b.departureTimeFromStartStation, partialBooking.departureTimeFromStartStation)
                .Set(b => b.availableDates, partialBooking.availableDates)
                .Set(b => b.availableTimes, partialBooking.availableTimes)
                .Set(b => b.userEmail, partialBooking.userEmail)
                .Set(b => b.scheduleID, partialBooking.scheduleID)
                .Set(b => b.nic, partialBooking.nic)
                .Set(b => b.createdAt, partialBooking.createdAt);

            _bookings.UpdateOne(filter, updateDefinition);
        }

        public void Remove(string id)
        {
            _bookings.DeleteOne(booking => booking.id == id);
        }
        public List<Booking> GetBookingsByUserEmail(string userEmail)
        {
            var filter = Builders<Booking>.Filter.Eq(b => b.userEmail, userEmail);
            return _bookings.Find(filter).ToList();
        }
    }
}

