using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public string currentCitytoSave;

    public List<string> raidedCityListforSave = new List<string>();

    public string timeSaved;

    public Inventory inventoryToSave;

}
