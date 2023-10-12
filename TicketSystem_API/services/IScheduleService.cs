using TicketSystem_API.Models;

namespace TicketSystem_API.services
{
    public interface IScheduleService
    {
        List<Schedule> Get();
        Schedule Get(string id);
        Schedule Create(Schedule schedule);
        void Update(string id, Schedule schedule);
        void Patch(string id, Schedule schedule);
        void Remove(string id);
       
    }
}
