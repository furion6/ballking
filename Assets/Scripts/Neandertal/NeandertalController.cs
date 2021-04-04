using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeandertalController : MonoBehaviour, PlayerControllerInterface
{
    // Start is called before the first frame update
    public GameObject bullet;
    public GameObject camera;

    public Animator animator;
    public float minPower;
    public float maxPower;

    public float angularVeloMin;
    public float angularVeloMax;

    public float speed;                
    private Rigidbody2D rb2d;        
    public RuntimeAnimatorController animatorController;

    public GameObject event1;

    private MainPlayerController mainPlayerController;


//FIXME REFACTOR
    private float holdTimePercentage;
    private Transform whereToSpawn;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainPlayerController = GetComponent<MainPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(float moveHorizontal){
        if(mainPlayerController.isFalling) {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            animator.SetBool("isWalking", false);
            return;
        }

        rb2d.velocity = new Vector2(moveHorizontal*speed , rb2d.velocity.y);
        animator.SetBool("isWalking", moveHorizontal!=0f );
        if(moveHorizontal >0 && !mainPlayerController.m_FacingRight) Flip();
        else if (moveHorizontal < 0 && mainPlayerController.m_FacingRight) Flip();

    }


    public void stopMoving() {
        rb2d.velocity = new Vector2(0,0);
        animator.SetBool("isWalking", false);
    }
    public void startThrowing(Vector3 mousePos) {
        //FIXME to uproscic mozna ale tak przejrzysciej?
        this.stopMoving();

        if(mousePos.x > this.transform.position.x && !mainPlayerController.m_FacingRight) Flip();
        else if(mousePos.x < this.transform.position.x && mainPlayerController.m_FacingRight) Flip();
        animator.SetTrigger("startThrow");

    }
    public void endThrowing(float holdTimePercentage, Transform whereToSpawn) {
            animator.SetTrigger("endThrow");
			this.holdTimePercentage = holdTimePercentage;
            this.whereToSpawn = whereToSpawn;

    }

        public void release() {
			GameObject ball = GameObject.Instantiate(bullet, whereToSpawn.position, whereToSpawn.rotation);

			ball.GetComponent<BallController>().setPlayer(gameObject);
			ball.GetComponent<BallController>().setCamera(camera);

			float power = holdTimePercentage * (maxPower-minPower) + minPower;

            ball.GetComponent<Rigidbody2D>().velocity = whereToSpawn.right * power;

            float angularVelocity =  Random.Range(angularVeloMin, angularVeloMax) * (Random.Range(0,2)*2-1);
			ball.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;

    }

    public void Flip()
	{
        GetComponent<MainPlayerController>().Flip();
	}

    public void init() {
        animator.runtimeAnimatorController = animatorController;
    }

    public void triggerSpecialAnimation(string animation) {
        animator.SetTrigger(animation);
    }

    public void notifyToDestroyHat() {
        event1.GetComponent<Event1Manager>().destroyHat();
    }

    public void notifyAnimationEnded() {
        event1.GetComponent<Event1Manager>().animationEnded();
    }
}
