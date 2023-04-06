using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "attacks")]
public class Attacks : ScriptableObject {
    public enum TYPE { HULL, SAIL, CREW, NORMAL }

    public string attackName = "Undefined Attack Name";
    public TYPE type = TYPE.NORMAL;
    public int baseDamage = 1;
}