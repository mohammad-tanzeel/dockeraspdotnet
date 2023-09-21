using Microsoft.AspNetCore.Mvc;
using System.Runtime.Loader;
using TestAspDotNetCore.Data;
using TestAspDotNetCore.Models;

namespace TestAspDotNetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(dbContext.Contacts.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAontact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contacts()
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

               if(contact != null)
                {
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;
                contact.FullName = updateContactRequest.FullName;
                contact.Phone = updateContactRequest.Phone;
                
                    await dbContext.SaveChangesAsync();
                    return Ok(contact);
                }
               return NotFound();
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                    return Ok(contact);
            }
            return NotFound(contact);

        }
    }
}
