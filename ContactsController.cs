using Microsoft.AspNetCore.Mvc;
using SOSBackend.Data;
using SOSBackend.Models;

namespace SOSBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public IActionResult GetContacts(string userId)
        {
            var contacts = _context.Contacts.Where(c => c.UserId == userId).ToList();
            return Ok(contacts);
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok(contact);
        }
    }
}
