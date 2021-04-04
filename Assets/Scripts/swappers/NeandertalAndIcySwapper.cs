using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeandertalAndIcySwapper : MonoBehaviour
{

    public GameObject player;
    public string firstControllerName;
    public string secondControllerName;

    private PlayerControllerInterface firstController;
    private PlayerControllerInterface secondController;


    private MainPlayerController controller;

    private ClassHolder classHolder;

    public bool vertical = true;

    void Start()
    {
        classHolder = player.GetComponent<ClassHolder>();
        controller = player.GetComponent<MainPlayerController>();
        firstController = classHolder.classDictionary[firstControllerName];
        secondController = classHolder.classDictionary[secondControllerName];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FIXME [BUG] mozliwe ze wyjdzie z 0 velocity?
    public void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player" || other.tag == "Ball") {
            if(vertical){
                if(other.GetComponent<Rigidbody2D>().velocity.y > 0){
                    controller.playerController = secondController;
                } 
                else if((other.GetComponent<Rigidbody2D>().velocity.y < 0)){
                    controller.playerController = firstController;
                }
            }else{
                if(other.GetComponent<Rigidbody2D>().velocity.x > 0){
                    controller.playerController = secondController;
                } 
                else if((other.GetComponent<Rigidbody2D>().velocity.x < 0)){
                    controller.playerController = firstController;
                }
            }
            controller.playerController.init();
        }
    }
}
