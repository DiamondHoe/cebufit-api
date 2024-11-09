namespace CebuFitApi.Helpers.Enums
{
    public enum PhysicalActivityLevelEnum
    {
        Sedentary = 1, //brak aktywności (praca biurowa, brak ćwiczeń) - 1.2
        LightlyActive = 2, //niska aktywność (lekkie ćwiczenia 1-3 razy w tygodniu) - 1.375
        ModeratelyActive = 3, //umiarkowana aktywność (umiarkowane ćwiczenia 3-5 razy w tygodniu) - 1.55
        VeryActive = 4, //duża aktywność (intensywne ćwiczenia 6-7 razy w tygodniu) - 1.725
        SuperActive = 5 // bardzo duża aktywność (ciężka praca fizyczna lub trening dwa razy dziennie) - 1.9
    }
}
