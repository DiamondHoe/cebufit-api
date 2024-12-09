using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.DTOs.Demand;
using CebuFitApi.DTOs.User;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Models;
using CebuFitApi.Mapping;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Mapping
{
    [TestSubject(typeof(MappingProfile))]
    public class MappingProfileTest
    {
        private readonly IMapper _mapper;

        public MappingProfileTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Theory]
        [InlineData("testLogin", "testPassword", "testName", true, 180, 75.5, "2000-01-01",
            PhysicalActivityLevelEnum.LightlyActive)]
        [InlineData("testLogin", "testPassword", "testName", false, 160, 60.0, "1990-05-15",
            PhysicalActivityLevelEnum.ModeratelyActive)]
        public void UserDTO_To_User_MapsCorrectly(string login, string password, string name, bool gender, int height,
            decimal weight, string birthDate, PhysicalActivityLevelEnum activityLevel)
        {
            var userDto = new UserDTO
            {
                Login = login,
                Password = password,
                Name = name,
                Gender = gender,
                Height = height,
                Weight = weight,
                BirthDate = DateTime.Parse(birthDate),
                PhysicalActivityLevel = activityLevel
            };

            var user = _mapper.Map<User>(userDto);

            Assert.Equal(userDto.Login, user.Login);
            Assert.Equal(userDto.Password, user.Password);
            Assert.Equal(userDto.Name, user.Name);
            Assert.Equal(userDto.Gender, user.Gender);
            Assert.Equal(userDto.Height, user.Height);
            Assert.Equal(userDto.Weight, user.Weight);
            Assert.Equal(userDto.BirthDate, user.BirthDate);
            Assert.Equal(userDto.PhysicalActivityLevel, user.PhysicalActivityLevel);
        }

        [Theory]
        [InlineData("testLogin", "testPassword", "testName", true, 180, 75.5, "2000-01-01",
            PhysicalActivityLevelEnum.LightlyActive)]
        [InlineData("testLogin", "testPassword", "testName", false, 160, 60.0, "1990-05-15",
            PhysicalActivityLevelEnum.ModeratelyActive)]
        public void User_To_UserDTO_MapsCorrectly(string login, string password, string name, bool gender, int height,
            decimal weight, string birthDate, PhysicalActivityLevelEnum activityLevel)
        {
            var user = new User
            {
                Login = login,
                Password = password,
                Name = name,
                Gender = gender,
                Height = height,
                Weight = weight,
                BirthDate = DateTime.Parse(birthDate),
                PhysicalActivityLevel = activityLevel
            };

            var userDto = _mapper.Map<UserDTO>(user);

            Assert.Equal(user.Login, userDto.Login);
            Assert.Equal(user.Password, userDto.Password);
            Assert.Equal(user.Name, userDto.Name);
            Assert.Equal(user.Gender, userDto.Gender);
            Assert.Equal(user.Height, userDto.Height);
            Assert.Equal(user.Weight, userDto.Weight);
            Assert.Equal(user.BirthDate, userDto.BirthDate);
            Assert.Equal(user.PhysicalActivityLevel, userDto.PhysicalActivityLevel);
        }

        [Fact]
        public void UserCreateDTO_To_User_MapsCorrectly()
        {
            var userCreateDto = new UserCreateDTO
            {
                Login = "testLogin",
                Email = "test@example.com",
                Password = "testPassword",
                Name = "testName",
                Gender = true,
                Height = 180,
                Weight = 75.5m,
                BirthDate = DateTime.Parse("2000-01-01"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.LightlyActive
            };

            var user = _mapper.Map<User>(userCreateDto);

            Assert.Equal(userCreateDto.Login, user.Login);
            Assert.Equal(userCreateDto.Email, user.Email);
            Assert.Equal(userCreateDto.Password, user.Password);
            Assert.Equal(userCreateDto.Name, user.Name);
            Assert.Equal(userCreateDto.Gender, user.Gender);
            Assert.Equal(userCreateDto.Height, user.Height);
            Assert.Equal(userCreateDto.Weight, user.Weight);
            Assert.Equal(userCreateDto.BirthDate, user.BirthDate);
            Assert.Equal(userCreateDto.PhysicalActivityLevel, user.PhysicalActivityLevel);
        }

        [Fact]
        public void User_To_UserCreateDTO_MapsCorrectly()
        {
            var user = new User
            {
                Login = "testLogin",
                Email = "test@example.com",
                Password = "testPassword",
                Name = "testName",
                Gender = true,
                Height = 180,
                Weight = 75.5m,
                BirthDate = DateTime.Parse("2000-01-01"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.LightlyActive
            };

            var userCreateDto = _mapper.Map<UserCreateDTO>(user);

            Assert.Equal(user.Login, userCreateDto.Login);
            Assert.Equal(user.Email, userCreateDto.Email);
            Assert.Equal(user.Password, userCreateDto.Password);
            Assert.Equal(user.Name, userCreateDto.Name);
            Assert.Equal(user.Gender, userCreateDto.Gender);
            Assert.Equal(user.Height, userCreateDto.Height);
            Assert.Equal(user.Weight, userCreateDto.Weight);
            Assert.Equal(user.BirthDate, userCreateDto.BirthDate);
            Assert.Equal(user.PhysicalActivityLevel, userCreateDto.PhysicalActivityLevel);
        }

        [Fact]
        public void UserLoginDTO_To_User_MapsCorrectly()
        {
            var userLoginDto = new UserLoginDTO
            {
                Login = "testLogin",
                Password = "testPassword"
            };

            var user = _mapper.Map<User>(userLoginDto);

            Assert.Equal(userLoginDto.Login, user.Login);
            Assert.Equal(userLoginDto.Password, user.Password);
        }

        [Fact]
        public void User_To_UserLoginDTO_MapsCorrectly()
        {
            var user = new User
            {
                Login = "testLogin",
                Password = "testPassword"
            };

            var userLoginDto = _mapper.Map<UserLoginDTO>(user);

            Assert.Equal(user.Login, userLoginDto.Login);
            Assert.Equal(user.Password, userLoginDto.Password);
        }

        [Fact]
        public void UserPublicDto_To_User_MapsCorrectly()
        {
            var userPublicDto = new UserPublicDto
            {
                Id = Guid.NewGuid(),
                Login = "testLogin",
                Name = "testName"
            };

            var user = _mapper.Map<User>(userPublicDto);

            Assert.Equal(userPublicDto.Id, user.Id);
            Assert.Equal(userPublicDto.Login, user.Login);
            Assert.Equal(userPublicDto.Name, user.Name);
        }

        [Fact]
        public void User_To_UserPublicDto_MapsCorrectly()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = "testLogin",
                Name = "testName"
            };

            var userPublicDto = _mapper.Map<UserPublicDto>(user);

            Assert.Equal(user.Id, userPublicDto.Id);
            Assert.Equal(user.Login, userPublicDto.Login);
            Assert.Equal(user.Name, userPublicDto.Name);
        }

        [Fact]
        public void UserDetailsDTO_To_User_MapsCorrectly()
        {
            var userDetailsDto = new UserDetailsDTO
            {
                Login = "testLogin",
                Name = "testName",
                Gender = true,
                Height = 180,
                Weight = 75.5m,
                BirthDate = DateTime.Parse("2000-01-01"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.LightlyActive
            };

            var user = _mapper.Map<User>(userDetailsDto);

            Assert.Equal(userDetailsDto.Login, user.Login);
            Assert.Equal(userDetailsDto.Name, user.Name);
            Assert.Equal(userDetailsDto.Gender, user.Gender);
            Assert.Equal(userDetailsDto.Height, user.Height);
            Assert.Equal(userDetailsDto.Weight, user.Weight);
            Assert.Equal(userDetailsDto.BirthDate, user.BirthDate);
            Assert.Equal(userDetailsDto.PhysicalActivityLevel, user.PhysicalActivityLevel);
        }

        [Fact]
        public void User_To_UserDetailsDTO_MapsCorrectly()
        {
            var user = new User
            {
                Login = "testLogin",
                Name = "testName",
                Gender = true,
                Height = 180,
                Weight = 75.5m,
                BirthDate = DateTime.Parse("2000-01-01"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.LightlyActive
            };

            var userDetailsDto = _mapper.Map<UserDetailsDTO>(user);

            Assert.Equal(user.Login, userDetailsDto.Login);
            Assert.Equal(user.Name, userDetailsDto.Name);
            Assert.Equal(user.Gender, userDetailsDto.Gender);
            Assert.Equal(user.Height, userDetailsDto.Height);
            Assert.Equal(user.Weight, userDetailsDto.Weight);
            Assert.Equal(user.BirthDate, userDetailsDto.BirthDate);
            Assert.Equal(user.PhysicalActivityLevel, userDetailsDto.PhysicalActivityLevel);
        }

        [Fact]
        public void UserUpdateDTO_To_User_MapsCorrectly()
        {
            var userUpdateDto = new UserUpdateDTO
            {
                Name = "updatedName",
                Gender = false,
                Height = 170,
                Weight = 65.0m,
                BirthDate = DateTime.Parse("1995-12-12"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.VeryActive
            };

            var user = _mapper.Map<User>(userUpdateDto);

            Assert.Equal(userUpdateDto.Name, user.Name);
            Assert.Equal(userUpdateDto.Gender, user.Gender);
            Assert.Equal(userUpdateDto.Height, user.Height);
            Assert.Equal(userUpdateDto.Weight, user.Weight);
            Assert.Equal(userUpdateDto.BirthDate, user.BirthDate);
            Assert.Equal(userUpdateDto.PhysicalActivityLevel, user.PhysicalActivityLevel);
        }

        [Fact]
        public void User_To_UserUpdateDTO_MapsCorrectly()
        {
            var user = new User
            {
                Name = "updatedName",
                Gender = false,
                Height = 170,
                Weight = 65.0m,
                BirthDate = DateTime.Parse("1995-12-12"),
                PhysicalActivityLevel = PhysicalActivityLevelEnum.VeryActive
            };

            var userUpdateDto = _mapper.Map<UserUpdateDTO>(user);

            Assert.Equal(user.Name, userUpdateDto.Name);
            Assert.Equal(user.Gender, userUpdateDto.Gender);
            Assert.Equal(user.Height, userUpdateDto.Height);
            Assert.Equal(user.Weight, userUpdateDto.Weight);
            Assert.Equal(user.BirthDate, userUpdateDto.BirthDate);
            Assert.Equal(user.PhysicalActivityLevel, userUpdateDto.PhysicalActivityLevel);
        }
    }
}