using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public string currentCitytoSave;

    public string[] raidedCityListforSave;

    public string timeSaved;

    

    public Resource[] resourcesFromSave;
    public string[] shipNamesToSave;
    public string[] crewNamesToSave;

    public bool firstMapLoadToSave = true;



}
