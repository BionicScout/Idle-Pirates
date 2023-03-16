using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "resourceInfo", menuName = "Resources/MainResource", order = 1)]

// https://docs.unity3d.com/Manual/class-ScriptableObject.html
// https://www.youtube.com/watch?v=aPXvoWVabPY&ab_channel=Brackeys
public class MainResources : ScriptableObject {
    public Resource.Type type;
    public string resourceName;
    public int buyValue;
    public Sprite sprite;
}
