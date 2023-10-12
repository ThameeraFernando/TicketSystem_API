namespace TicketSystem_API.Models
{
    public class ScheduleStoreDatabaseSettings : IScheduleStoreDatabaseSettings
    {
        public string ScheduleCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
