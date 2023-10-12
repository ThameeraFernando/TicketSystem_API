using MongoDB.Driver;
using TicketSystem_API.Models;


namespace TicketSystem_API.services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedules;

        public ScheduleService(IScheduleStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _schedules = database.GetCollection<Schedule>(settings.ScheduleCollectionName);
        }

        public Schedule Create(Schedule schedule)
        {
            _schedules.InsertOne(schedule);
            return schedule;
        }

        public List<Schedule> Get()
        {
            return _schedules.Find(schedule => true).ToList();
        }

        public Schedule Get(string id)
        {
            return _schedules.Find(schedule => schedule.id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _schedules.DeleteOne(schedule => schedule.id == id);
        }

        public void Update(string id, Schedule schedule)
        {
            _schedules.ReplaceOne(schedule => schedule.id == id, schedule);
        }
        public void Patch(string id, Schedule partialSchedule)
        {
            var filter = Builders<Schedule>.Filter.Eq(s => s.id, id);

            var updateDefinition = Builders<Schedule>.Update
                .Set(s => s.name, partialSchedule.name)
                .Set(s => s.startingPoint, partialSchedule.startingPoint)
                .Set(s => s.destination, partialSchedule.destination)
                .Set(s => s.arrivalTimeToEndStation, partialSchedule.arrivalTimeToEndStation)
                .Set(s => s.departureTimeFromStartStation, partialSchedule.departureTimeFromStartStation)
                .Set(s => s.ticketPrice, partialSchedule.ticketPrice)
                .Set(s => s.isActive, partialSchedule.isActive)
                .Set(s => s.availableSeats, partialSchedule.availableSeats)
                .Set(s => s.availableDates, partialSchedule.availableDates)
                .Set(s => s.date, partialSchedule.date)
                .Set(s => s.time, partialSchedule.time)
                .Set(s => s.availableTimes, partialSchedule.availableTimes)
                .Set(s => s.createdAt, partialSchedule.createdAt);

            _schedules.UpdateOne(filter, updateDefinition);
        }

    }
}
