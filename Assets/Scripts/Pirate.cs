using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Pirate", menuName = "Pirate")]
public class Pirate : ScriptableObject {

    public string name;
    public string desc;

    public Sprite artwork;

    public int attack;
    public int health;

    public void Print() {
        Debug.Log(name + ": " + desc + "(" + attack + "/" + health +")");
    }
}
