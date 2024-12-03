using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using VueAppTS.Server.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VueAppTS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly ThAmCo.Events.Model.EventsDbContext _context;
        private readonly HttpClient _httpClient;

        public EventsController(ThAmCo.Events.Model.EventsDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }


        /// <summary>
        /// Get all Event Types from external API
        /// </summary>
        /// <returns>List of EventTypeDto</returns>
        /// 
        [HttpGet("eventtypes")]
        public async Task<ActionResult<IEnumerable<EventTypeDto>>> GetEventTypes()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://thamcovenues.azurewebsites.net/api/eventtypes");
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Failed to fetch event types from external API.");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                var eventTypes = JsonSerializer.Deserialize<IEnumerable<EventTypeDto>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(eventTypes);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error fetching event types: {ex.Message}");
            }
        }

        /// <summary>
        /// Get all Events
        /// </summary>
        /// <returns>List of all EventItemDto</returns>
        [HttpGet("all")]
        public ActionResult<IEnumerable<EventItemDto>> GetAll()
        {
            var items = _context.Events;
            var DTO = items.Select(i => new EventItemDto
            {
                EventId = i.EventId,
                EventType = i.EventType,
                Title = i.Title,
            }).ToList();

            return Ok(DTO);
        }

     
        [HttpGet]
        public ActionResult<IEnumerable<EventItemDto>> Get(string Id)
        {
            var items = _context.Events.ToList();
            if (Id != null || Id != "")
            {
                items = items.Where(i => i.EventType == Id).ToList();
            }

            var DTO = items.Select(i => new EventItemDto
            {
                EventId = i.EventId,
                EventType = i.EventType,
                Title = i.Title,
            }).ToList();

            return Ok(DTO);
        }

       

        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
