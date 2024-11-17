using CebuFitApi.DTOs.Demand;
using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs.User
{
    public class UserDetailsDTO
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } //False = Man, True = Woman
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public PhysicalActivityLevelEnum PhysicalActivityLevel { get; set; } = PhysicalActivityLevelEnum.LightlyActive;
        public UserDemandDTO? Demand { get; set; }
    }
}
