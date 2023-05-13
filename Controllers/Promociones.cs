
using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class PromocionController : ControllerBase
{

    private readonly DataContext _context;

    public PromocionController(DataContext dataContext)
    {
        _context = dataContext;
    }

    /// <summary>
    /// Mostrar todas las promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="200">Devuelve el listado de promociones</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    public ActionResult<List<Promocion>> Get()
    {
        List<Promocion> promocion = _context.Promociones.ToList();


        return promocion == null ? NotFound()
              : Ok(promocion);
    }
    /// <summary>
    /// Buscar por id
    /// </summary>
    /// <returns>Todas las promociones con el mismo id</returns>
    /// <response code="200">Devuelve el listado de promociones con este id</response>
    /// <response code="500">Si hay algún error</response>
    [HttpGet]
    [Route("{id:int}")]
    public ActionResult<Promocion> Get(int id)
    {
        Promocion Promocion = _context.Promociones.Find(id);
        return Promocion == null ? NotFound()
            : Ok(Promocion);
    }
    /// <summary>
    /// Buscar por nombre
    /// </summary>
    /// <returns>Todos los grados con el mismo nombre</returns>
    /// <response code="200">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    //Buscar por nombre
    [HttpGet]
    [Route("name")]
    public ActionResult<Promocion> Get(string name)
    {
        List<Promocion> promocion = _context.Promociones.Where(x => x.name.Contains(name)).OrderByDescending(x => x.name).ToList();
        //buscar por nombre   
        return promocion == null ? NotFound()

            : Ok(promocion);
    }
        [HttpGet]
    [Route("vigor")] 
    public ActionResult<TipoGasoleo> Get(bool vigor)
    {
        
  List<Promocion> promociones =_context.Promociones.Where(x=> x.vigor == vigor).ToList();
        //buscar por bool   
        return promociones == null? NotFound()
            : Ok(promociones);
    }
    /// <summary>
    /// añadir promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<Promocion> Post([FromBody] Promocion promocion)
    {

        promocion.id = 0;
        _context.Promociones.Add(promocion);
        _context.SaveChanges();

        string resourceUrl = Request.Path.ToString() + "/" + promocion.id;
        return Created(resourceUrl, promocion);
    }
    /// <summary>
    ///Actualizar las promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="201">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPut("{id}")]
    public ActionResult<Promocion> Update([FromBody] Promocion promocion, int id)
    {
        Promocion promocionToUpdate = _context.Promociones.Find(id);
        if (promocionToUpdate == null)
        {
            return NotFound("grado no encontrado");
        }
        promocionToUpdate.name = promocion.name;
        promocionToUpdate.description = promocion.description;
        promocionToUpdate.vigor = promocion.vigor;
        promocionToUpdate.date = promocion.date;
        promocionToUpdate.price = promocion.price;
        promocionToUpdate.descuento = promocion.descuento;
        promocionToUpdate.cantidadPersonas = promocion.cantidadPersonas;



        _context.SaveChanges();
        string resourceUrl = Request.Path.ToString() + "/" + promocionToUpdate.name;
        
        return Created(resourceUrl, promocionToUpdate);
    }

    /// <summary>
    /// Eliminar grados seleccionados
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        Promocion promocionToDelete = _context.Promociones.Find(id);
        if (promocionToDelete == null)
        {
            return NotFound("menu no encontrado");
        }
        _context.Promociones.Remove(promocionToDelete);
        _context.SaveChanges();
        if (promocionToDelete == null)
        {
            return NotFound();
        }
        return Ok(promocionToDelete);
    }

}
