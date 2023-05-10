using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class GasoleoController : ControllerBase
{

    private readonly DataContext _context;

    public GasoleoController(DataContext dataContext)
    {
        _context = dataContext;
    }
    

    /// <summary>
    /// Mostrar todos los gasoleos
    /// </summary>
    /// <returns>Todos los gasoleos</returns>
    /// <response code="200">Devuelve el listado de gasoleo</response>
    /// <response code="500">Si hay algún error</response>
    
    [HttpGet]
    public ActionResult<List<TipoGasoleo>> Get()
    {
        List<TipoGasoleo> tipoGasoleo =_context.TipoGasoleos.OrderByDescending(x => x.contamina).ToList();
        //Revisar orden
       
        return   Ok(tipoGasoleo);
        
    }

/// <summary>
    /// Mostrar  los gasoleos con este id
    /// </summary>
    /// <returns>Todos los gasoleos con este id</returns>
    /// <response code="200">Devuelve el listado de gasoleos con este id</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("contamina")] 
    public ActionResult<TipoGasoleo> Get(bool contamina)
    {
        
  List<TipoGasoleo> tipoGasoleo =_context.TipoGasoleos.Where(x=> x.contamina == contamina).ToList();
        //buscar por bool   
        return tipoGasoleo == null? NotFound()
            : Ok(tipoGasoleo);
    }
    /// <summary>
    /// añadir gasoleos
    /// </summary>
    /// <returns>Todos los gasoleos</returns>
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
    public ActionResult<TipoGasoleo> Post([FromBody] TipoGasoleo tipoGasoleo )
    {
         TipoGasoleo existingGasoleoItems= _context.TipoGasoleos.Find(tipoGasoleo.id);
         
        if (existingGasoleoItems != null)
        {
            return Conflict("Ya existe un elemento ");
        }
        _context.TipoGasoleos.Add(tipoGasoleo);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + tipoGasoleo.id;
       
        return Created(resourceUrl, tipoGasoleo);
    }
    /// <summary>
    ///Actualizar los gasoleos
    /// </summary>
    /// <returns>Todos los gasoleos</returns>
    /// <response code="201">Devuelve el listado de gasoleos</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id}")]
    public ActionResult<TipoGasoleo> Update([FromBody] TipoGasoleo tipoGasoleo, int id)
    {
        TipoGasoleo gasoleoToUpdate = _context.TipoGasoleos.Find(id);
         
        if (gasoleoToUpdate == null)
        {
            return NotFound("usuario no encontrado");
        }
        gasoleoToUpdate.name=tipoGasoleo.name;
        gasoleoToUpdate.price=tipoGasoleo.price;
        gasoleoToUpdate.date=tipoGasoleo.date;
        gasoleoToUpdate.contamina=tipoGasoleo.contamina;
        gasoleoToUpdate.description=tipoGasoleo.description;
        gasoleoToUpdate.number=tipoGasoleo.number;

        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + gasoleoToUpdate.number;

        return Created(resourceUrl, gasoleoToUpdate);
    }

    /// <summary>
    /// Eliminar gasoleos seleccionados
    /// </summary>
    /// <returns>Todos los gasoleos</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
        [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        TipoGasoleo gasoleoToDelete = _context.TipoGasoleos.Find(id);
        if (gasoleoToDelete == null)
        {
            return NotFound("menu no encontrado");
        }
        _context.TipoGasoleos.Remove(gasoleoToDelete);
        _context.SaveChanges();
        return Ok(gasoleoToDelete);
    }

}