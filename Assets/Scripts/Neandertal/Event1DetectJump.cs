using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1DetectJump : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(col.tag == "Player"){
            visited = true;
            manager.GetComponent<Event1Manager>().jumpDetected();
        } //FIXME optymalizacja
    }
}
