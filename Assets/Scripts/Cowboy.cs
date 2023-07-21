using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cowboy", menuName = "ScriptableObjects/Cowboy", order = 1)]
public class Cowboy : ScriptableObject
{
    public string cowboyName;

    public Cowboy(string cowboyName)
    {
        this.cowboyName = cowboyName;
    }

}
