namespace CebuFitApi.DTOs
{
    public class UserCreateDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } //False = Man, True = Woman
        //Wyrzucić do nowej entity UserDemands
        public DateTime BirthDate { get; set; }
        public int KcalDemand { get; set; }
    }
}
