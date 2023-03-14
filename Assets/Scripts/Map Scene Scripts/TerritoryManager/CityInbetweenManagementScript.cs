using System.Collections.Generic;

public static class CityInbetweenManagementScript {
    public static string currentCity;

    public static List<string> citesThatHaveBeenRaided = new List<string>();

    public static void newRaidedCity(string name) {
        currentCity = name;
        citesThatHaveBeenRaided.Add(name);
    }
}
