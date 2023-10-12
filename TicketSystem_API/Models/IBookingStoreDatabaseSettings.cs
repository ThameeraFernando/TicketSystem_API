namespace TicketSystem_API.Models
{
    public interface IBookingStoreDatabaseSettings
    {
        string BookingCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
