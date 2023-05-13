
using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class UsersController : ControllerBase
{

    private readonly DataContext _context;

    public UsersController(DataContext dataContext)
    {
        _context = dataContext;
    }

    /// <summary>
    /// Mostrar todos los usuarios
    /// </summary>
    /// <returns>Todos los usuaruos</returns>
    /// <response code="200">Devuelve el listado de usuarios</response>
    /// <response code="500">Si hay algún error</response>
    
    [HttpGet ]
    public ActionResult<List<User>> Get()
    {
        List<User> user =_context.User.OrderByDescending(x => x.name).ToList();
        //Revisar orden
       
        return   Ok(user);
        
    }
 /*   [HttpGet]
    [Route("{id}")]
    public ActionResult<User> Get(int id)
    {

        //buscar por nombre
        return user == null? NotFound()
            : Ok(user);
    }
*/
   /// <summary>
    /// Buscar por nombre 
    /// </summary>
    /// <returns>nombre de  usuario</returns>
    /// <response code="200">Devuelve el listado de usuarios con el mismo nombre</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("name")] 
    public ActionResult<User> Get(string name ,bool gender)
    {
        
  List<User> user =_context.User.Where(x=> x.gender == gender && x.name.Contains(name)).OrderByDescending(x=>x.name).ToList();
        //buscar por nombre   
        return user == null? NotFound()
            : Ok(user);
    }
       /// <summary>
    /// Buscar usuarios por ID
    /// </summary>
    /// <returns>el usuario con este id</returns>
    /// <response code="200">Devuelve el listado de usuarios con este id</response>
    /// <response code="500">Si hay algún error</response>
       [HttpGet]
    [Route("{id:int}")]
    public ActionResult<User> Get(int id)
    {
    User User = _context.User.Find(id);
        return User == null? NotFound()
            : Ok(User);
    }


    /// <summary>
    /// añadir usuarios
    /// </summary>
    /// <returns>Todos los usuarios</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
   /* [HttpPost]
    public ActionResult<User> Post([FromBody] User user)
    {

        user.id=0;
        _context.User.Add(user);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + user.id;
        return Created(resourceUrl, user);
    }
*/

    
    [HttpPost]
    public ActionResult<User> Post([FromBody] User user)
    {
         User existingUserItems= _context.User.Find(user.id);
        if (existingUserItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.User.Add(user);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + user.id;
        return Created(resourceUrl, user);
    }
    /// <summary>
    ///Actualizar los usurarios
    /// </summary>
    /// <returns>Todos los usuarios</returns>
    /// <response code="201">Devuelve el listado de usuarios</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id:int}")]
    public ActionResult<User> Update([FromBody] User user, int id)
    {
        User userToUpdate = _context.User.Find(id);
        if (userToUpdate == null)
        {
            return NotFound("usuario no encontrado");
        }
        userToUpdate.name=user.name;
        userToUpdate.email=user.email;
        userToUpdate.password=user.password;
        userToUpdate.gender=user.gender;
        userToUpdate.dateInscription=user.dateInscription;
         userToUpdate.puntosAcumulados=user.puntosAcumulados;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + userToUpdate.name;
 
        return Created(resourceUrl, userToUpdate);
    }

    /// <summary>
    /// Eliminar usuarios seleccionados
    /// </summary>
    /// <returns>Todos los usuarios</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
        [HttpDelete ]
         [Route("{id}")]
    public  ActionResult Delete (int id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        else
        {
            User userToDelete = _context.User.Find(id);
            if (userToDelete == null)
            {
                return NotFound("menu no encontrado");
            }
            _context.User.Remove(userToDelete);
             _context.SaveChanges();
            var orders = _context.OrderPros.ToList();
            orders.ForEach(o =>
            {
                if (o.UserId == id)
                {
                    _context.OrderPros.Remove(o);
                }
                _context.SaveChanges();
                
            });
            return Ok();

        }
        }

}
