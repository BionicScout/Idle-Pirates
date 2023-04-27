using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MinigameSelecter {
    static string[] minigameNames = { "Combat", "Ship Cleaning", "Coconut Mini", "Ship Mini"};
    static string[] minigameScenes = { "Combat", "Ship Cleaning Instructions", "Coconut Mini Game Instructions", "Ship Sailing Mini Game Instructions" };
    static string[] minigameDecriptions = { 
        "Your fleet was attack by a neighbooring city", 
        "Your ship got covered in gunk while sailing", 
        "Your crew ran out of food", 
        "Your crew attempted to get a bonus for a quick deleivery" };
    static int[] minigameWeights = { 50, 50, 50, 50};

    public static string getMinigame() {
        int totalWeight = 0;

        foreach(int n in minigameWeights)
            totalWeight += n;

        UnityEngine.Debug.Log("Total Weight: " + totalWeight);

        int selectedWeight = (int)((Random.value * totalWeight) % totalWeight);
        UnityEngine.Debug.Log("Random Num: " + selectedWeight);

        string selectedGame = "";
        int maxWeight = 0;
        for(int i = 0; i < minigameWeights.Length; i++) {
            maxWeight += minigameWeights[i];
            selectedGame = minigameScenes[i];

            if(selectedWeight <= maxWeight)
                break;
            
        }

        UnityEngine.Debug.Log("Scene: " + selectedGame);

        return selectedGame;
    }

    public static List<string> getMinigameList(int numberOfGames) {
        if(numberOfGames > minigameWeights.Length)
            numberOfGames = minigameWeights.Length;

        List<string> scenes = new List<string>();

        for(int i = 0; i < numberOfGames; i++) {
            string str = getMinigame();

            while(scenes.Contains(str))
                str = getMinigame();

            scenes.Add(str);
        }

        return scenes;
    }

    public static List<string> getDescribtionList(List<string> sceneNames) {
        List<string> descriptions = new List<string>();

        foreach(string scene in sceneNames) {
            for(int i = 0; i < minigameDecriptions.Length; i++){
                if(minigameScenes[i] == scene) {
                    descriptions.Add(minigameDecriptions[i]);
                    break;
                }
            }
        }

        return descriptions;
    }
}
