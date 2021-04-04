using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOGManager : MonoBehaviour
{
    public List<GameObject> allCurtains = new List<GameObject>();
    public GameObject disabledCurtain;
    void Start()
    {
        for(int i = 0; i<allCurtains.Count; i++){
            allCurtains[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void disableCurtain(GameObject curtain) {
        foreach(GameObject o in allCurtains){
            o.SetActive(true);
        }
        curtain.SetActive(false);
    }
}
