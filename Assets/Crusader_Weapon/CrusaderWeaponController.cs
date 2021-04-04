using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusaderWeaponController : MonoBehaviour
{
    private GameObject camera;
    private GameObject player;

    private PhysicsMaterial2D normal;
    private PhysicsMaterial2D bouncy;
    private bool jumpedAlready = false;
    void Start()
    {
        normal = new PhysicsMaterial2D();
        normal.bounciness = 0f;
        normal.friction = 0.4f;

        bouncy = new PhysicsMaterial2D();
        bouncy.friction = 0f;
        bouncy.bounciness = 0.8f;

        GetComponent<Rigidbody2D>().sharedMaterial = bouncy;
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D theCollision ){
        if(jumpedAlready == false) return;
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("normal_floor")) 
        {
            foreach(ContactPoint2D contact in theCollision.contacts)
            {
                if(contact.normal.y > 0.1f)
                {
                    teleportPlayer();
                    Destroy(gameObject);
                    camera.GetComponent<SmoothCamera2D>().target = player.transform;
                    this.player.GetComponent<ThrowCrusaderWeapon>().canThrow();
                    GetComponent<Rigidbody2D>().sharedMaterial = bouncy;
                }
            }
        }
    }

    public void OnCollisionExit2D(Collision2D theCollision ){
        jumpedAlready = true;
        GetComponent<Rigidbody2D>().sharedMaterial = normal;
    }

    public void setPlayer(GameObject player){
       this.player = player;
    }

    private void teleportPlayer(){
	    player.transform.position = new Vector2(transform.position.x, transform.position.y + 2.5f);
        player.GetComponent<ThrowCrusaderWeapon>().playDust();
		//joinToPlayer();
	}

    public void setCamera(GameObject camera){
        this.camera = camera;
    }
}
