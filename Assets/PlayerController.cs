using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Animator animator;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //FIXME - po teleportacji, żeby animacji nie było
        if(rb2d.velocity.y < -0.1f) {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            animator.SetBool("isWalking", false);
            return;
        }
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");
        //Debug.Log(moveHorizontal + "   " );
        rb2d.velocity = new Vector2(moveHorizontal*speed , rb2d.velocity.y);
        animator.SetBool("isWalking", moveHorizontal!=0f );
        if(moveHorizontal >0 && !m_FacingRight) Flip();
        else if (moveHorizontal < 0 && m_FacingRight) Flip();
    }

    private void Flip()
	{
        //Debug.Log("flipping");
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public void setIsThrowing(bool isThrowing){
        animator.SetTrigger("Throw");
    }

    
}