using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1Manager : MonoBehaviour
{
    public GameObject player;
    public GameObject hat;
    public bool visited = false;
    MainPlayerController mainPlayerController;

    public bool stopMet = false;
    public bool jumpMet = false;

    public float x_throw = 700f;
    public float y_throw = 470.0f;
    void Start()
    {
        mainPlayerController = player.GetComponent<MainPlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(visited) return;
        if(jumpMet && !stopMet) mainPlayerController.move(1.0f);
        
    }

    public void jumpDetected() {
        jumpMet = true;
        mainPlayerController.disableUserInteraction();
    }

    public void stopDetected() {
        stopMet = true;
        mainPlayerController.stopMoving();
        mainPlayerController.triggerSpecialAnimation("event1");
    }

    public void destroyHat() {
        Destroy(hat);
    }

    public void stop2Detected(){
        stopMet = true;
        //800, 530, 0
        mainPlayerController.stopMoving();
        Vector3 mousePos = new Vector3(x_throw, y_throw, 0f);
        mainPlayerController.leftMouse(mousePos);
        mainPlayerController.leftMouseReleaseFixedPercentage(mousePos, 1.3f);
        mainPlayerController.enableUserInteraction();
    }

    public void animationEnded() {
        stopMet = false;

        mainPlayerController.playerController = mainPlayerController.gameObject.GetComponent<IcyNeandertalController>();
        mainPlayerController.playerController.init();
    }


}
