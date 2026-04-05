using Microsoft.AspNetCore.Mvc;
using mvc_backend_1.Models;
using mvc_backend_1.Service;

namespace mvc_backend_1.Controllers;

[Route("[controller]")]
[ApiController]
public class ReserveController : Controller
{
    private readonly ReserveService _reserveService;

    public ReserveController(ReserveService reserveService)
    {
        _reserveService = reserveService;
    }

    [HttpGet("GenerateQueue")]
    public async Task<ActionResult<ResponseModel<Reserve>>> GenerateQueue()
    {
        ResponseModel<Reserve> result = await _reserveService.GenerateQueue();
        return Ok(result);
    }

    [HttpGet("CheckExistQueue")]
    public async Task<ActionResult<ResponseModel<bool>>> CheckExistQueue()
    {
        ResponseModel<bool> result = await _reserveService.CheckExistQueue();
        return Ok(result);
    }

    [HttpGet("FindLastQueue")]
    public async Task<ActionResult<ResponseModel<Reserve>>> FindLastQueue()
    {
        ResponseModel<Reserve> result = await _reserveService.FindLastQueue();
        return Ok(result);
    }

    [HttpGet("ClearQueue")]
    public ActionResult<ResponseModel<bool>> ClearQueue()
    {
        ResponseModel<bool> result = _reserveService.ClearQueue();
        return Ok(result);
    }
}
