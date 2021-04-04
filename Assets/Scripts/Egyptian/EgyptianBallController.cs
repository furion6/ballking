using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgyptianBallController : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    void Start()
    {

    }

    public float tempParam = 1.5f;

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D theCollision ){
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("normal_floor") || theCollision.gameObject.layer == LayerMask.NameToLayer("floor")) 
        {
            foreach(ContactPoint2D contact in theCollision.contacts)
            {
                if(contact.normal.y > 0.1f)
                {
                    teleportPlayer();
                    Destroy(gameObject);
                    camera.GetComponent<SmoothCamera2D>().target = player.transform;
                    this.player.GetComponent<MainPlayerController>().ballLanded();
                }
            }
        }
    }

    public void setPlayer(GameObject player){
       this.player = player;
    }

    private void teleportPlayer(){
	    player.transform.position = new Vector2(transform.position.x, transform.position.y + tempParam);
        //player.GetComponent<ThrowBall>().playDust();
		//joinToPlayer();
	}

    public void setCamera(GameObject camera){
        this.camera = camera;
    }
}
