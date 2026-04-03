using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.Features.Vehiculos.Queries;
using RentalCar.Application.Interfaces;

namespace RentalCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoRepository _repo;

        public VehiculosController(IVehiculoRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("buscar")]
        public async Task<IActionResult> Buscar(BuscarVehiculosQuery query)
        {
            var result = await _repo.BuscarDisponibles(
                query.LocalidadRecogidaId,
                query.FechaRecogida,
                query.FechaDevolucion
            );

            return Ok(result);
        }
    }
}
