using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour {
     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     public Transform target;
     private Camera camera;

     private void Start() {
         camera = GetComponent<Camera>();
     }
 
     // Update is called once per frame
     void Update () 
     {
         if (target)
         {
             Vector3 point = camera.WorldToViewportPoint(target.position);
             Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             Vector3 destination = new Vector3(transform.position.x,transform.position.y + delta.y + 15.0f,transform.position.z);
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }
     
     }
 }