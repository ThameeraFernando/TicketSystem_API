using TicketSystem_API.Models;

namespace TicketSystem_API.services
{
    public interface IBookingService
    {
        List<Booking> Get();
        Booking Get(string id);
        Booking Create(Booking booking);
        void Update(string id, Booking booking);
        void Patch(string id, Booking booking);
        void Remove(string id);

        List<Booking> GetBookingsByUserEmail(string userEmail);
    }
}
