using ThAmCo.Event.Model;

namespace VueAppTS.Server.DTO
{
    public class EventItemDto
    {
            public int EventId { get; set; }
            public string EventType { get; set; }
            public string Title { get; set; }
    }

    public class EventTypeDto
    {
        public string id { get; set; }
        public string title { get; set; }
    }
}
