using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerControllerInterface
{

    void init();
    void move(float x);
    void stopMoving();
    void startThrowing(Vector3 mousePos);
    void endThrowing(float holdTimePercentage, Transform whereToSpawn);
    void release();
    void Flip();
    void triggerSpecialAnimation(string animation);    
}
