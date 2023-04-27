using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public string currentCitytoSave;

    public string[] raidedCityListforSave;

    public long timeSaved;

    

    public Resource[] resourcesToSave;
    //public int[] resourceAmountsToSave;
    public string[] shipNamesToSave;
    public string[] crewNamesToSave;
    public int lastCrewActiveIndex;

    public int[] shipUses; //2 = trade, 1 = combat, 0 = none

    public bool firstMapLoadToSave = true;

    public SaveData_TimedActivityManager timeManagerSave;

}
