using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PathfindingList {
    List<Node> nodes;
    List<float> distances;

    public PathfindingList() {
        nodes = new List<Node>();
        distances = new List<float>();
    }

    public void add(float dist, Node node) {
        nodes.Add(node);
        distances.Add(dist);

        Sort();
    }

    public void remove(Node node) {
        int index = getIndex(node);

        distances.RemoveAt(index);
        nodes.RemoveAt(index);
    }

    public int getIndex(Node node) {
        return nodes.IndexOf(node);
    }

    public int count() {
        return distances.Count;
    }

    public float getDistance(Node node) {
        int index = getIndex(node);

        if(index != -1)
            return distances[index];

        return -1; //Return -1 because all distacne are postive
    }

    public List<Node> getNodes() {
        return nodes;
    }

    public Node getFirst() {
        return nodes[0];
    }

    public float getDistance(int i) {
        return distances[i];
    }

    public Node getNode(int i) {
        return nodes[i];
    }

    public void printInfo() {
        Debug.Log("--------------------------------------------------------------------------------------");

        for(int i = 0; i < distances.Count; i++) {
            Debug.Log("(" + distances[i] + ", " + nodes[i] + ")");
        }

    }

    /*
       procedure insertionSort(A: list of sortable items)
           n = length(A)
           for i = 1 to n - 1 do
               j = i
               while j > 0 and A[j-1] > A[j] do
                   swap(A[j], A[j-1])
                   j = j - 1
               end while
           end for
        end procedure 
    */
    public void Sort() { // Based off of insertion sort    
        //for(int i = 1; i < distances.Count; i++) {
        //    for(int j = 1; j > 0; j--) {
        //        if(distances[j - 1] > distances[j]) {

        //            Debug.Log(distances[j - 1] + " > " + distances[j]);
        //            float tempDist = distances[j - 1];
        //            distances[j - 1] = distances[j];
        //            distances[j] = tempDist;

        //            Node tempNode = nodes[j - 1];
        //            nodes[j - 1] = nodes[j];
        //            nodes[j] = tempNode;
        //        }
        //    }
        //}

        for(int i = 1; i < distances.Count; ++i) {
            float key = distances[i];
            Node nodeKey = nodes[i];
            int j = i - 1;

            // Move elements of arr[0..i-1],
            // that are greater than key,
            // to one position ahead of
            // their current position
            while(j >= 0 && distances[j] > key) {
                distances[j + 1] = distances[j];
                nodes[j + 1] = nodes[j];
                j = j - 1;
            }
            distances[j + 1] = key;
            nodes[j + 1] = nodeKey;
        }

        //printInfo();
    }
}
