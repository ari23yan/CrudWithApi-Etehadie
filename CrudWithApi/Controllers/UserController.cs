using CrudWithApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWithApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CrudDbContext _crudDbContext;

        public UserController(CrudDbContext crudDbContext)
        {
            _crudDbContext = crudDbContext;
        }
        [HttpGet(Name ="Test")]
        public IEnumerable<User> GetAllUsers()
        {
            return _crudDbContext.Users.ToList();
        }

        [HttpGet]
        public IActionResult GetUsersById([FromQuery]int id)
        {
            var res = _crudDbContext.Users.FirstOrDefault(i=> i.Id== id);
            if (res == null)
            {
                return NotFound();
            }

            

           return new ObjectResult(res);
           // return new JsonResult(Ok(res));
        }

        [HttpPost]
        public IActionResult Create([FromBody] User use)
        {
            if(use == null)
            {
                return BadRequest();
            }
            User user = new User()
            {
                Id = use.Id,
                Name = use.Name,
                Password = use.Password,
            };
            _crudDbContext.Users.Add(user);
            _crudDbContext.SaveChanges();
            return Ok();
            //("Create User", new { id = use.Id }, use); 
        //    return await _crudDbContext.Create(use);

        }

        [HttpPut]
        public IActionResult Update( int id ,[FromBody] User use)
        {
            if(use == null)
            {
                return BadRequest();

            }
            var res = _crudDbContext.Users.FirstOrDefault(p => p.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            res.Name = use.Name;
            res.Password = use.Password;

            _crudDbContext.Users.Update(res);
            _crudDbContext.SaveChanges();

            return  Ok();

        }

        [HttpDelete]
        public IActionResult DeleteUser(int id )
        {

            var res = _crudDbContext.Users.FirstOrDefault(pp => pp.Id == id);
            if(res== null)
            {
                return BadRequest();

            }
            _crudDbContext.Users.Remove(res);
            _crudDbContext.SaveChanges();

            return Ok();
        }


    }
}
