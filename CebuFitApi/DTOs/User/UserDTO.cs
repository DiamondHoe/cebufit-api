﻿namespace CebuFitApi.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; } //False = Man, True = Woman
        public DateTime BirthDate { get; set; }
        public int KcalDemand { get; set; }
    }
}
