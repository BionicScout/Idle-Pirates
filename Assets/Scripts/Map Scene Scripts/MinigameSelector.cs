using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MinigameSelecter {
    static string[] minigameScenes = { "Combat", "Ship Cleaning", "Coconut Mini Game Instructions", "Ship Mini Game Instructions" };
    static int[] minigameWeights = { 40, 20, 20, 20 };

    //Method to get a single mini game
    //Method to make a list of games
    //Switch to minigame scenes

    public static void getMinigame() {
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
    }
}
