using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class UserCreateDTO
    {
        public string Role { get; set; } = RoleEnum.User.ToString();
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } //False = Man, True = Woman
        public DateTime BirthDate { get; set; }
        public int KcalDemand { get; set; }
    }
}
