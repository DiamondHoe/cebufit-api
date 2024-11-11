using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class UserDemandService : IUserDemandService
    {
        private readonly IMapper _mapper;
        private readonly IUserDemandRepository _demandRepository;
        private readonly IUserRepository _userRepository;

        public UserDemandService(
                IMapper mapper,
                IUserDemandRepository demandRepository,
                IUserRepository userRepository)
        {
            _mapper = mapper;
            _demandRepository = demandRepository;
            _userRepository = userRepository;
        }

        public async Task<UserDemandDTO?> GetDemandAsync(Guid userId)
        {
            var demandEntity = await _demandRepository.GetDemandAsync(userId);
            var demandDTO = _mapper.Map<UserDemandDTO>(demandEntity);
            return demandDTO;
        }

        public async Task UpdateDemandAsync(UserDemandUpdateDTO demandUpdateDTO, Guid userId)
        {
            var demand = _mapper.Map<UserDemand>(demandUpdateDTO);

            if (await _userRepository.GetById(userId) != null)
            {
                await _demandRepository.UpdateDemandAsync(demand, userId);
            }
        }

        public async Task AutoCalculateDemandAsync(Guid userId, UserDemandCreateDTO? demandCreateDTO = null)
        {
            var foundUser = await _userRepository.GetById(userId);
            if (foundUser == null) return;

            var foundDemand = await _demandRepository.GetDemandAsync(userId);
            if (foundDemand == null) return;

            foundDemand = DemandHelper.CalculateDemand(foundUser);
            await _demandRepository.UpdateDemandAsync(foundDemand, userId);
        }
    }
}
