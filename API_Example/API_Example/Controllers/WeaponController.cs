using API_Example.Dtos.Weapon;
using API_Example.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Example.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeaponController : ControllerBase
	{
		private readonly IWeaponService _weaponService;

		public WeaponController(IWeaponService weaponService)
        {
			_weaponService = weaponService;
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
		{
			return Ok(await _weaponService.AddWeapon(newWeapon));
		}
    }
}
