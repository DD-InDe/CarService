using Api.Models.Dtos;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetById([FromRoute] int id)
    {
        String action = "Просмотр заявки по id";
        try
        {
            OrderDto? order = await viewService.GetOrderById(id);
            if (order == null)
            {
                logService.LogAction(action, true);
                return NotFound();
            }

            logService.LogAction(action, true);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetAll()
    {
        String action = "Просмотр заявок";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetOrders());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet("clientMaterials/{orderId:int}")]
    public async Task<ActionResult<OrderMaterialClientDto>> GetClientMaterials([FromRoute] int orderId)
    {
        String action = "Просмотр материалов клиента по заявке";
        try
        {
            logService.LogAction(action,true);
            return Ok(await viewService.GetOrderMaterialsClientByOrderId(orderId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
    
    [HttpGet("serviceMaterials/{orderId:int}")]
    public async Task<ActionResult<OrderMaterialClientDto>> GetServiceMaterials([FromRoute] int orderId)
    {
        String action = "Просмотр материалов сервиса по заявке";
        try
        {
            logService.LogAction(action,true);
            return Ok(await viewService.GetOrderMaterialsServiceByOrderId(orderId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
    
    [HttpGet("services/{orderId:int}")]
    public async Task<ActionResult<OrderMaterialClientDto>> GetServices([FromRoute] int orderId)
    {
        String action = "Просмотр услуг по заявке";
        try
        {
            logService.LogAction(action,true);
            return Ok(await viewService.GetOrderServicesByOrderId(orderId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}