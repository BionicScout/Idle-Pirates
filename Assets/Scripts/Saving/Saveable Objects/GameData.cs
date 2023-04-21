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

    

    public Resource[] resourcesToSave;
    //public int[] resourceAmountsToSave;
    public string[] shipNamesToSave;
    public string[] crewNamesToSave;
    public int lastCrewActiveIndex;

    public int[] lastShipsinCombatIndexes;

    public bool firstMapLoadToSave = true;



}
