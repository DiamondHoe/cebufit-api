using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class DemandService : IDemandService
    {
        private readonly IMapper _mapper;
        private readonly IDemandRepository _demandRepository;
        private readonly IUserRepository _userRepository;

        public DemandService(
                IMapper mapper,
                IDemandRepository demandRepository,
                IUserRepository userRepository)
        {
            _mapper = mapper;
            _demandRepository = demandRepository;
            _userRepository = userRepository;
        }

        public async Task<DemandDTO> GetDemandAsync(Guid userId)
        {
            var demandEntity = await _demandRepository.GetDemandAsync(userId);
            var demandDTO = _mapper.Map<DemandDTO>(demandEntity);
            return demandDTO;
        }

        public async Task UpdateDemandAsync(DemandUpdateDTO demandUpdateDTO, Guid userId)
        {
            var demand = _mapper.Map<Demand>(demandUpdateDTO);

            if (await _userRepository.GetById(userId) != null)
            {
                await _demandRepository.UpdateDemandAsync(demand, userId);
            }
        }

        public async Task AutoCalculateDemandAsync(Guid userId)
        {
            var foundUser = await _userRepository.GetById(userId);
            if (foundUser != null)
            {
                //Calculate demand
                Demand demand = new Demand();
                demand.Calories = (int?)((int?)(
                    (10 * decimal.ToInt32(foundUser.Weight))
                    + (6.25m * foundUser.Height)
                    - (5 * (DateTime.UtcNow.Year - foundUser.BirthDate.Year))
                    + (foundUser.Gender ? -161 : 5))
                    * (foundUser.PhysicalActivityLevel switch
                    {
                        PhysicalActivityLevelEnum.Sedentary => 1.2,
                        PhysicalActivityLevelEnum.LightlyActive => 1.375,
                        PhysicalActivityLevelEnum.ModeratelyActive => 1.55,
                        PhysicalActivityLevelEnum.VeryActive => 1.725,
                        PhysicalActivityLevelEnum.SuperActive => 1.9,
                        _ => 1.0 // Default case
                    }));

                demand.CarbPercent = 40;
                demand.FatPercent = 30;
                demand.ProteinPercent = 30;

                await _demandRepository.UpdateDemandAsync(new Demand(), userId);
            }
        }
    }
}
