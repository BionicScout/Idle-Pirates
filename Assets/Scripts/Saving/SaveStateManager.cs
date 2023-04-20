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

    public void SaveGame(GameData saveData) 
    {
        //Get data

        saveData.currentCitytoSave =
        CityInbetweenManagementScript.currentCity;

        saveData.raidedCityListforSave =
            new string[CityInbetweenManagementScript.citesThatHaveBeenRaided.Count];

        for (int i = 0; i < CityInbetweenManagementScript.citesThatHaveBeenRaided.Count; i++)
        {
            saveData.raidedCityListforSave[i] = 
                (CityInbetweenManagementScript.citesThatHaveBeenRaided[i]);
        }

        saveData.resourcesFromSave = new Resource[Inventory.instance.resources.Count];

        for (int i = 0; i < Inventory.instance.resources.Count; i++)
        {
            saveData.resourcesFromSave[i] = (Inventory.instance.resources[i]);
        }

        saveData.crewNamesToSave = new string[Inventory.instance.crew.Count];

        for (int i = 0; i < Inventory.instance.crew.Count; i++)
        {
            saveData.crewNamesToSave[i] = (Inventory.instance.crew[i].crewName);
        }

        saveData.shipNamesToSave = new string[Inventory.instance.ships.Count];

        for (int i = 0; i < Inventory.instance.ships.Count; i++)
        {
            saveData.shipNamesToSave[i] = (Inventory.instance.ships[i].GetShipName());
        }



        saveData.timeSaved =
            System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy  hh:mm tt");

        saveData.firstMapLoadToSave = SceneSwitcher.firstMapLoad;


        //Get data for TimeActivityManager
        TimeQueryList_Saveable queryList = new TimeQueryList_Saveable(TimedActivityManager.instance.timeQueries);
        // saveTime = System.DateTime.Now;

    //Save Objects
        FileStream dataStream = new FileStream(filePath, FileMode.Create); //try to save data of scene
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

            for (int i = 0; i < saveData.raidedCityListforSave.Length; i++)
            {
                CityInbetweenManagementScript.citesThatHaveBeenRaided.Add
                    (saveData.raidedCityListforSave[i]);
            }

            for (int i = 0; i < saveData.resourcesFromSave.Length; i++)
            {
                Inventory.instance.resources.Add
                    (saveData.resourcesFromSave[i]);
            }

            for (int i = 0; i < saveData.crewNamesToSave.Length; i++)
            {

                //search template for crew Name
                //Inventory.instance.crewTemplates.Add

                MainCrewMembers crew = Inventory.instance.crewTemplates.Find(x => x.name == saveData.crewNamesToSave[i]);


                Inventory.instance.crew.Add(new InventoryCrew(crew));
            }

            for (int i = 0; i < saveData.shipNamesToSave.Length; i++)
            {
                MainShips ship = Inventory.instance.shipTemplates.Find(x => x.name == saveData.shipNamesToSave[i]);

                Inventory.instance.ships.Add(new InventoryShip(ship));
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


    public void DeleteData()
    {
        GameData saveData = new GameData();
        SaveGame(saveData);
        saveData.firstMapLoadToSave = true;


        Inventory.instance.crew.Clear();
        Inventory.instance.ships.Clear();

        int goldStartingAmount = Inventory.instance.resources[0].amount;

        for (int i = 0; i < Inventory.instance.resources.Count; i++)
        {
            Inventory.instance.resources[i].SubtractAmount
                (Inventory.instance.resources[i].amount);
        }

        Inventory.instance.resources[0].amount = goldStartingAmount;

       
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
