
using Microsoft.AspNetCore.Mvc;
using Data;
using Classes;


namespace Classes;


[ApiController]
[Route("[Controller]")]

public class OrderController : ControllerBase
{

    private readonly DataContext _context;

    public OrderController(DataContext dataContext)
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
    public ActionResult<List<OrderPro>> Get()
    {
        List<OrderPro> promocion = _context.OrderPros.ToList();


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
    public ActionResult  GetByUserId(int id)
    {
        var  list = _context.OrderPros.Where(e => e.UserId==id).ToList();
    if (list.Count==0){
        return NotFound();
        
    }
    else{
        var lista= new List<Promocion>() ;
        list.ForEach((Action<OrderPro>)(e=>{
            var promocion= Queryable.Where<Promocion>(_context.Promociones, a => a.id == e.PromocionId).FirstOrDefault<Promocion>();
            lista.Add(promocion);
        }));
        return Ok(lista);
    }
   
    }
    /// <summary>
    /// Buscar por nombre
    /// </summary>
    /// <returns>Todos los grados con el mismo nombre</returns>
    /// <response code="200">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    //Buscar por nombre
   /* [HttpGet]
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
    }*/
    /// <summary>
    /// añadir promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="201">Se ha creado correctamente</response>
    /// <response code="500">Si hay algún error</response>
    [HttpPost]
    public ActionResult<OrderPro> Post([FromBody] OrderPro orderPro)
    {
        if (orderPro==null){
            return BadRequest();
        }
        else{
            var promocionId=_context.User.Find(orderPro.PromocionId);//Bucamos en el context si el id de promociones existe;
            var userId=_context.User.Find(orderPro.UserId);//Bucamos en el context si el id de promociones existe;
            if(promocionId==null || userId==null ){
                return NotFound();
            }
            else{
                 _context.OrderPros.Add(orderPro);
        _context.SaveChanges();
         string resourceUrl = Request.Path.ToString() + "/" + orderPro.id;
        return Created(resourceUrl, orderPro);
            }
        }
       

       
        
    }
    /// <summary>
    ///Actualizar las promociones
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="201">Devuelve el listado de grados</response>
    /// <response code="500">Si hay algún error</response>
    
    /// <summary>
    /// Eliminar grados seleccionados
    /// </summary>
    /// <returns>Todas las promociones</returns>
    /// <response code="200">Se ha eliminado</response>
    /// <response code="500">Si hay algún error</response>
    /*
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
    }*/

}
