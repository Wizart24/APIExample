﻿using API_Example.Dtos.Weapon;
using System.Security.Claims;

namespace API_Example.Services.WeaponService
{
	public class WeaponService : IWeaponService
	{
		private readonly DataContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IMapper _mapper;

		public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
			_httpContextAccessor = httpContextAccessor;
			_context = context;
			_mapper = mapper;
		}

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
		{
			var response = new ServiceResponse<GetCharacterDto>();

			try
			{
				var character = await _context.Characters
					.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && 
						c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User
							.FindFirstValue(ClaimTypes.NameIdentifier)!));

				if (character is null)
				{
					response.Success = false;
					response.Message = "Character Not Found.";
					return response;
				}

				var weapon = new Weapon
				{
					Name = newWeapon.Name,
					Damage = newWeapon.Damage,
					Character = character
				};

				_context.Weapons.Add(weapon);
				await _context.SaveChangesAsync();

				response.Data = _mapper.Map<GetCharacterDto>(character);
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = ex.Message;
			}

			return response;
		}
	}
}
