using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D theCollision ){
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("normal_floor")) 
        {
            foreach(ContactPoint2D contact in theCollision.contacts)
            {
                if(contact.normal.y > 0.1f)
                {
                    teleportPlayer();
                    Destroy(transform.parent.gameObject);
                    camera.GetComponent<SmoothCamera2D>().target = player.transform;
                    this.player.GetComponent<ThrowGrenade>().canThrow();
                }
            }
        }
    }

    

    public void setPlayer(GameObject player){
       this.player = player;
    }

    private void teleportPlayer(){
	    player.transform.position = new Vector2(transform.position.x, transform.position.y + 2.5f);
        player.GetComponent<ThrowGrenade>().playDust();
		//joinToPlayer();
	}

    public void setCamera(GameObject camera){
        this.camera = camera;
    }
}
