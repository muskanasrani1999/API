using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpAPIDbContext dbContext;

        public SignUpController(SignUpAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetDetails()
        {
            return Ok(dbContext.Sign.ToList());
            
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task <IActionResult> GetDetail([FromRoute] Guid id)
        {
            var details = await dbContext.Sign.FindAsync(id);
            if (details == null)
            {
                return NotFound();

            }
            return Ok(details);
        }

        [HttpPost]
        public async Task<IActionResult> AddDetails(AddDetail adddetail)
        {
            var signup = new Signup()
            {
                Id = Guid.NewGuid(),
                Name = adddetail.Name,
                Phone = adddetail.Phone


            };
            await dbContext.Sign.AddAsync(signup);
            await dbContext.SaveChangesAsync();

            return Ok(signup);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDetails([FromRoute] Guid id, Update updatedetails)
        {
            var details=await dbContext.Sign.FindAsync(id);

            if (details != null)
            {
                details.Name = updatedetails.Name;
                details.Phone = updatedetails.Phone;
                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDetails([FromRoute] Guid id)
        {
            var details = await dbContext.Sign.FindAsync(id);
            if (details != null)
            {
                dbContext.Remove(details);
                await dbContext.SaveChangesAsync();
                return Ok(details);
            }
            return NotFound();
        }
    }
}
