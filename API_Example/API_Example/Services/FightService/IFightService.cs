using API_Example.Dtos.Fight;

namespace API_Example.Services.FightService
{
	public interface IFightService
	{
		Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
		Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
		Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
		Task<ServiceResponse<List<HighScoreDto>>> GetHighscore();
	}
}
