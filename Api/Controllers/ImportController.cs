using Api.Models.Database;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/imports")]
public class ImportController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{guid}")]
    public async Task<ActionResult<Transaction>> GetById([FromRoute] String guid)
    {
        String action = "Просмотр транзакции по id";
        try
        {
            Transaction? transaction = await viewService.GetTransactionById(guid);
            if (transaction == null)
            {
                logService.LogAction(action, false);
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
    public async Task<ActionResult<List<Transaction>>> GetAll()
    {
        String action = "Просмотр транзакции";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetTransactions());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet("orders/{guid}")]
    public async Task<ActionResult<ImportOrder>> GetImportById([FromRoute] String guid)
    {
        String action = "Просмотр импорта по транзакции";
        try
        {
            Transaction? transaction = await viewService.GetTransactionById(guid);
            if (transaction == null)
            {
                logService.LogAction(action, false);
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
}