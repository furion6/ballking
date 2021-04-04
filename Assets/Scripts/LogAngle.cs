using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAngle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Clamp0360(gameObject.transform.rotation.eulerAngles.z));
    }

    public float Clamp0360(float eulerAngles)
     {
         float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
         if (result < 0)
         {
             result += 360f;
         }
         return result;
     }
}
