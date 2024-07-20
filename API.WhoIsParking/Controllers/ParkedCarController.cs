using API.WhoIsParking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.WhoIsParking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkedCarController : ControllerBase
{
    [HttpPost]
    public Task<IActionResult> PostAsync(ParkedCarModel parkedCar)
    {
        throw new NotImplementedException();
    }
}
