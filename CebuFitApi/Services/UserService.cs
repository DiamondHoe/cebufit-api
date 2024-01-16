using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<User> AuthenticateAsync(UserLoginDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            var userAuthenticated = await _userRepository.AuthenticateAsync(userEntity);
            return userAuthenticated;
        }

        public async Task<bool> CreateAsync(UserCreateDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            userEntity.Id = Guid.NewGuid();
            bool isRegistered = await _userRepository.CreateAsync(userEntity);
            return isRegistered;
        }
    }
}
