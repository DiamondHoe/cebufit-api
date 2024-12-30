using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs.User
{
    public class UserUpdateDTO
    {
        //jeżeli potrzebujesz zmianę maila lub inne dane, dodaj je tutaj
        //a później w UserService.cs w metodzie UpdateAsync
        public string? Name { get; set; }
        public bool? Gender { get; set; } //False = Man, True = Woman
        public int? Height { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? BirthDate { get; set; }
        public PhysicalActivityLevelEnum? PhysicalActivityLevel { get; set; } = PhysicalActivityLevelEnum.LightlyActive;
        public UserDemandCreateDTO? Demand { get; set; }
    }
}
