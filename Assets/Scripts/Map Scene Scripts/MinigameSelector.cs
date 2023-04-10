using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MinigameSelecter {
    static string[] minigameScenes = { "Combat", "Ship Cleaning Game Instructions", "Coconut Mini Game Instructions", "Ship Mini Game Instructions" };
    static int[] minigameWeights = { 1, 1, 1, 1};

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
}
