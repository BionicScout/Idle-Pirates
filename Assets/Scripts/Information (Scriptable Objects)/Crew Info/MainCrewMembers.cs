using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "crewInfo", menuName = "Crew Member")]
public class MainCrewMembers : ScriptableObject
{
    public string crewName;
    public int speed;
    public int attack;
    public int health;
    public int cost;
    public Sprite crewImage;


}
