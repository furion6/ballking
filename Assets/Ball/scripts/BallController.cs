using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
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
                    Destroy(gameObject);
                    this.player.GetComponent<ThrowBall>().canThrow();
                }
            }
        }
    }

    public void setPlayer(GameObject player){
       this.player = player;
    }

    private void teleportPlayer(){
	    player.transform.position = new Vector2(transform.position.x, transform.position.y + 2.5f);
        player.GetComponent<ThrowBall>().playDust();
		//joinToPlayer();
	}
}
