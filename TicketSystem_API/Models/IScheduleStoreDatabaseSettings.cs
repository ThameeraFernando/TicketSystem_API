namespace TicketSystem_API.Models
{
    public interface IScheduleStoreDatabaseSettings
    {
        string ScheduleCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
