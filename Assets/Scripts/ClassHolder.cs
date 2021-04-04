using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterClass{
    NEANDERTAL,
    EGYPTIAN,
    ROMAN,
    CRUSADER,
    GRENADIER,
    
}

public class ClassHolder : MonoBehaviour
{
    public Dictionary<string, PlayerControllerInterface> classDictionary = new Dictionary<string, PlayerControllerInterface>();

    void Awake() {
        classDictionary.Add("NEANDERTAL", GetComponent<NeandertalController>());
        classDictionary.Add("ICYNEANDERTAL", GetComponent<IcyNeandertalController>());
        classDictionary.Add("EGYPTIAN", GetComponent<EgyptianController>());
    }
}
