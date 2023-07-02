using API_Example.Dtos.Weapon;

namespace API_Example.Services.WeaponService
{
	public interface IWeaponService
	{
		Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
	}
}
