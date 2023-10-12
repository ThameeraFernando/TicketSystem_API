namespace TicketSystem_API.Models
{
    public class BookingStoreDatabaseSettings : IBookingStoreDatabaseSettings
    {
        public string BookingCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
