using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinController : MonoBehaviour
{
    private GameObject player;
    public GameObject camera;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D theCollision ){
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("normal_floor") || theCollision.gameObject.layer == LayerMask.NameToLayer("floor")) 
        {
            foreach(ContactPoint2D contact in theCollision.contacts)
            {
                    if(contact.normal.y < 0.1f)
                    {
                        teleportPlayerWithOffset(player.GetComponent<BoxCollider2D>().bounds.size.y);
                    }else{
                        teleportPlayer();
                    }
                    Destroy(gameObject);
                    camera.GetComponent<SmoothCamera2D>().target = player.transform;
                    this.player.GetComponent<Bow>().canThrow();
            }
        }
        
    }

    public void setPlayer(GameObject player){
       this.player = player;
    }

    private void teleportPlayerWithOffset(float offset){
        player.transform.position = new Vector2(transform.position.x, transform.position.y - offset);
        player.GetComponent<ThrowBall>().playDust();
    }

    private void teleportPlayer(){
	    player.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        player.GetComponent<ThrowBall>().playDust();
		//joinToPlayer();
	}

    
    public void setCamera(GameObject camera){
        this.camera = camera;
    }
}