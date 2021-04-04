using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1Detect2Stop : MonoBehaviour
{
    public GameObject manager;

    private bool visited = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(visited) return;
        if(col.tag == "Player") {//FIXME optymalizacja
            visited = true;
            manager.GetComponent<Event1Manager>().stop2Detected();
        }
    }
}
