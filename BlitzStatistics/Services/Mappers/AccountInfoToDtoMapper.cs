using AutoMapper;
using BlitzStatistics.Model;
using System;
using System.Linq;

namespace BlitzStatistics.Services.Mappers
{
	public class AccountInfoToDtoMapper
	{
		private readonly IMapper _mapper;

		public AccountInfoToDtoMapper()
		{
			_mapper = new Mapper(new MapperConfiguration(m => m.CreateMap<WotBlitzStatician.Model.AccountInfo, AccountInfoDto>()
				.ForMember(dest => dest.NickName, 
						op => op.MapFrom(s => s.AccountClanInfo == null ? s.NickName : $"{s.NickName} [{s.AccountClanInfo.ClanTag}]"))
				.ForMember(dest => dest.Battles, op => op.MapFrom(s => s.AccountInfoStatistics.Single().Battles))
				.ForMember(dest => dest.Wn7, op => op.MapFrom(s => s.AccountInfoStatistics.Single().Wn7))
				.ForMember(dest => dest.AvgTier, op => op.MapFrom(s => s.AccountInfoStatistics.Single().AvgTier))
				.ForMember(dest => dest.Winrate,
						op => op.MapFrom(s => Convert.ToDouble(100 * s.AccountInfoStatistics.Single().Wins)/ s.AccountInfoStatistics.Single().Battles))
				.ForMember(dest => dest.AvgDamage,
						op => op.MapFrom(s => Convert.ToDouble(s.AccountInfoStatistics.Single().DamageDealt)/ s.AccountInfoStatistics.Single().Battles))
				.ForMember(dest => dest.AvgXp,
						op => op.MapFrom(s => Convert.ToDouble(s.AccountInfoStatistics.Single().Xp)/ s.AccountInfoStatistics.Single().Battles))
			));
		}

		public AccountInfoDto Map(WotBlitzStatician.Model.AccountInfo accountInfo)
		{
			return _mapper.Map<AccountInfoDto>(accountInfo);
		}
	}
}
