using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainKid : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fogManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "fogCol") //FIXME optymalizacja
        fogManager.GetComponent<FOGManager>().disableCurtain(gameObject.transform.parent.gameObject);
    }
}
