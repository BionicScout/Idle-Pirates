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
    //Get data
        saveData.currentCityNumberForSave =
            CityInbetweenManagementScript.currentStaticCityNumber;

        for(int i = 0; i < CityInbetweenManagementScript.staticCityList.Count; i++) {
            saveData.cityListforSave.Add(CityInbetweenManagementScript.staticCityList[i]);
        }

    //Get data for TimeActivityManager
        TimeQueryList_Saveable queryList = new TimeQueryList_Saveable(TimedActivityManager.instance.timeQueries);
        // saveTime = System.DateTime.Now;

    //Save Objects
        FileStream dataStream = new FileStream(filePath, FileMode.Create); //try to save data of scene
        BinaryFormatter converter = new BinaryFormatter();

        converter.Serialize(dataStream, saveData);
        converter.Serialize(dataStream, queryList);
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
            TimeQueryList_Saveable queryList = converter.Deserialize(dataStream) as TimeQueryList_Saveable;
           // DateTime saveTime = converter.Deserialize(dataStream) as DateTime;

            //Load data into game
            CityInbetweenManagementScript.currentStaticCityNumber
                = saveData.currentCityNumberForSave;

            for(int i = 0; i < saveData.cityListforSave.Count; i++) {
                CityInbetweenManagementScript.staticCityList.Add(saveData.cityListforSave[i]);
            }

        //Load data for Time Queries
            queryList.load();
            //TimedActivityManager

            //Stop Loading

            SceneSwitcher.instance.A_LoadScene(menuSceneName);

            dataStream.Close();
            return saveData;
        }
        else {
            Debug.LogError("Save file not found in " + filePath);
            return null;
        }



    }



    public GameData DeleteData() {
        GameData saveData = new GameData();
        SaveGame(saveData);
        return saveData;
    }


    public void ExitGame() {

    }
}
