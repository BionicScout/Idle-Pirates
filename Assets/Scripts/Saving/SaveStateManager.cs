using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine.SceneManagement;
using System;

public class SaveStateManager : MonoBehaviour {
    //private DialogueManager dialogueManager;
    string filePath;
    //private SceneNamesScript sceneNames;

    [SerializeField]
    private string menuSceneName;

    //public ExcursionBreakMenu exbMenu;
    //ExcursionBreakDialogueManager exbDialogueManager;

    static public SaveStateManager instance;

    private void Awake() {
        filePath = Application.persistentDataPath + "/save.data";

        // Check there are no other copies of this class in the scene
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    //private void Start() {
    //    dialogueManager = FindObjectOfType<DialogueManager>();
    //}

    //public void StartGame()
    //{

    //}

    public void SaveGame(GameData saveData) {
    //Save Raided Cities
        saveData.currentCitytoSave =
        CityInbetweenManagementScript.currentCity;

        saveData.raidedCityListforSave =
            new string[CityInbetweenManagementScript.citesThatHaveBeenRaided.Count];

        for(int i = 0; i < CityInbetweenManagementScript.citesThatHaveBeenRaided.Count; i++) {
            saveData.raidedCityListforSave[i] =
                (CityInbetweenManagementScript.citesThatHaveBeenRaided[i]);
        }

        saveData.resourcesToSave = new Resource[Inventory.instance.resources.Count];

    //Save Inventory
        for(int i = 0; i < Inventory.instance.resources.Count; i++) {
            saveData.resourcesToSave[i] = (Inventory.instance.resources[i]);
            saveData.resourcesToSave[i].amount = (Inventory.instance.resources[i].amount);
        }

        saveData.crewNamesToSave = new string[Inventory.instance.crew.Count];

        for(int i = 0; i < Inventory.instance.crew.Count; i++) {
            saveData.crewNamesToSave[i] = (Inventory.instance.crew[i].crewName);
            if(Inventory.instance.crew[i].active == true) {
                saveData.lastCrewActiveIndex = i;
            }
        }

        saveData.shipNamesToSave = new string[Inventory.instance.ships.Count];
        saveData.lastShipsinCombatIndexes = new int[3];

        int x = 0;
        for(int i = 0; i < Inventory.instance.ships.Count; i++) {

            saveData.shipNamesToSave[i] = (Inventory.instance.ships[i].GetShipName());

            if(Inventory.instance.ships[i].use == InventoryShip.USED_IN.combat) {
                saveData.lastShipsinCombatIndexes[x] = i;
                x++;
            }
        }

        //Need to save amount for resources
        //for (int i = 0; i < Inventory.instance.resources.Count; i++)
        //{
        //    saveData.resourceAmountsToSave[i] = (Inventory.instance.resources[i].amount);
        //}

        //Save TimedActivityManager and All Trade Deals
        saveData.timeManagerSave = new SaveData_TimedActivityManager();

    //Last Time Saved
        saveData.timeSaved =
            System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy  hh:mm tt");

        saveData.firstMapLoadToSave = SceneSwitcher.firstMapLoad;


        //Get data for TimeActivityManager
        TimeQueryList_Saveable queryList =
            new TimeQueryList_Saveable(TimedActivityManager.instance.timeQueries);
        // saveTime = System.DateTime.Now;

        //Save Objects
        FileStream dataStream = new FileStream(filePath, FileMode.Create);
        //try to save data of scene
        BinaryFormatter converter = new BinaryFormatter();

        converter.Serialize(dataStream, saveData);
        //converter.Serialize(dataStream, queryList);
        // converter.Serialize(dataStream, saveTime);


        dataStream.Close();
        Debug.Log("Game saved");

    }


    public GameData LoadGame() {

        //Check if File Exists before loading game
        if(File.Exists(filePath)) {
            //Load Data from File
            FileStream dataStream = new FileStream(filePath, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();

            GameData saveData = converter.Deserialize(dataStream) as GameData;
            //TimeQueryList_Saveable queryList = converter.Deserialize(dataStream) as TimeQueryList_Saveable;
            // DateTime saveTime = converter.Deserialize(dataStream) as DateTime;

            //Load data into game
            CityInbetweenManagementScript.currentCity
                = saveData.currentCitytoSave;

            for(int i = 0; i < saveData.raidedCityListforSave.Length; i++) {
                CityInbetweenManagementScript.citesThatHaveBeenRaided[i] =
                    (saveData.raidedCityListforSave[i]);
            }

            for(int i = 0; i < saveData.resourcesToSave.Length; i++) {
                Inventory.instance.resources[i] =
                    (saveData.resourcesToSave[i]);
                Inventory.instance.resources[i].amount =
                    saveData.resourcesToSave[i].amount;
            }

            //for (int i = 0; i < saveData.resourceAmountsToSave.Length; i++)
            //{
            //    Inventory.instance.resources[i].amount = saveData.resourceAmountsToSave[i];
            //}

            for(int i = 0; i < saveData.crewNamesToSave.Length; i++) {

                //search template for crew Name
                //Inventory.instance.crewTemplates.Add

                MainCrewMembers crew = Inventory.instance.crewTemplates.Find(x => x.name
                == saveData.crewNamesToSave[i]);


                Inventory.instance.crew.Add(new InventoryCrew(crew));
            }
            if(Inventory.instance.crew.Count != 0) {
                Inventory.instance.crew[saveData.lastCrewActiveIndex].active = true;
            }

            for(int i = 0; i < saveData.shipNamesToSave.Length; i++) {
                MainShips ship = Inventory.instance.shipTemplates.Find(x => x.name
                == saveData.shipNamesToSave[i]);

                Inventory.instance.ships.Add(new InventoryShip(ship));


            }
            for(int i = 0; i < saveData.lastShipsinCombatIndexes.Length; i++) {

                Inventory.instance.ships[saveData.lastShipsinCombatIndexes[i]].use
                    = InventoryShip.USED_IN.combat;


            }


            //Inventory.instance = saveData.inventoryToSave;

            SceneSwitcher.firstMapLoad = saveData.firstMapLoadToSave;


            //Loading the time saved


            //Load data for Time Queries
            //queryList.load();
            //TimedActivityManager

            //Stop Loading

            SceneSwitcher.instance.A_LoadScene("Map Scene");

            dataStream.Close();
            return saveData;
        }
        else {
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }



    }


    public void DeleteData() {
        GameData saveData = new GameData();
        SaveGame(saveData);

        //Debug.Log("Save Data:" + saveData.currentCitytoSave);
        //Debug.Log("Save Data:" + saveData.firstMapLoadToSave);

        CityInbetweenManagementScript.currentCity = "";

        CityInbetweenManagementScript.citesThatHaveBeenRaided.Clear();

        saveData.firstMapLoadToSave = true;
        Debug.Log(saveData.firstMapLoadToSave);

        Inventory.instance.crew.Clear();
        Inventory.instance.ships.Clear();

        int startingGold = Inventory.instance.startingGoldAmount;

        for(int i = 1; i < Inventory.instance.resources.Count; i++) {
            Inventory.instance.resources[i].amount = 0;
        }

        Inventory.instance.resources[0].amount = startingGold;


    }


    //public GameData DeleteData() {
    //    GameData saveData = new GameData();
    //    SaveGame(saveData);
    //    saveData.firstMapLoadToSave = true;


    //    Inventory.instance.crew.Clear();
    //    Inventory.instance.ships.Clear();

    //    int goldStartingAmount = Inventory.instance.resources[0].amount;

    //    for (int i = 0; i < Inventory.instance.resources.Count; i++)
    //    {
    //        Inventory.instance.resources[i].SubtractAmount
    //            (Inventory.instance.resources[i].amount);
    //    }

    //    Inventory.instance.resources[0].amount = goldStartingAmount;

    //    return saveData;
    //}


    public void ExitGame() {

    }
}
