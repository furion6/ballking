using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyNeandertalController : MonoBehaviour, PlayerControllerInterface
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

    public bool isOnIce;
    public bool isOnSlide;

    public float onIceParam=2f;

    public Transform bottom;

    public float SLIDING_SPEED = 10.0f;

//FIXME REFACTOR
    private float holdTimePercentage;
    private Transform whereToSpawn;

    private MainPlayerController mainPlayerController;

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

    private float Clamp0360(float eulerAngles)
     {
         float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
         if (result < 0)
         {
             result += 360f;
         }
         return result;
     }

    void FixedUpdate() {
        //TODO to musi byc inaczej, moze goslider z Triggera jesli bedzie dzialac?
        if(mainPlayerController.isSliding){
            //Transform trans = mainPlayerController.goSlider.transform;
            RaycastHit2D hit = Physics2D.Raycast(bottom.position, -Vector2.up, 1.5f, LayerMask.GetMask("floor"));
            if(!hit.collider ) return;
            Transform trans = hit.transform;
        
            float angle = Clamp0360(trans.rotation.eulerAngles.z) * Mathf.Deg2Rad;

            float y = Mathf.Sin(angle) * SLIDING_SPEED;
            float x = cwiartka(Clamp0360(trans.rotation.eulerAngles.z))* Mathf.Cos(angle) * SLIDING_SPEED;
            if(y>0) y*=-1.0f;
            rb2d.velocity = new Vector2(x,y);

            Debug.Log("SQRT = " + (x*x + y*y));
            Debug.Log("x-> " + x);
            Debug.Log("y->" + y);
            Debug.Log("Kąt: " + Clamp0360(trans.rotation.eulerAngles.z));
        }
    }

    float cwiartka(float angle) {
        if(angle > 0  && angle < 90) return -1.0f;
        else if(angle > 90  && angle < 180) return 1.0f;
        else if(angle >  180 && angle < 270) return -1.0f;
        else return 1.0f; 
    }

    public void move(float moveHorizontal){
        //TODO wyciągnąć wyżej?
        updateOnIce();
        if(mainPlayerController.isSliding) return;
        if(mainPlayerController.isFalling){
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            animator.SetBool("isWalking", false);
            return;
        }

        if(isOnIce){
            rb2d.AddForce(new Vector2 (onIceParam*moveHorizontal * speed, rb2d.velocity.y));
        }else {
            rb2d.velocity = new Vector2(moveHorizontal*speed , rb2d.velocity.y);
        }
        animator.SetBool("isWalking", moveHorizontal!=0f );
        if(moveHorizontal >0 && !mainPlayerController.m_FacingRight) Flip();
        else if (moveHorizontal < 0 && mainPlayerController.m_FacingRight) Flip();

    }

    private void updateOnIce(){//FIXME optymalizacja?
        RaycastHit2D hit = Physics2D.Raycast(bottom.position, -Vector2.up, 1.5f,LayerMask.GetMask("normal_floor"));
        if(hit.collider){
            Debug.Log("updateOnIce: " + hit.collider.gameObject);
            if(hit.collider.tag == "ICE") {
                isOnIce = true;
            }
            else
                isOnIce = false;
        }
    }

    public void stopMoving() {
        rb2d.velocity = new Vector2(0,0);
        animator.SetBool("isWalking", false);
    }
    public void startThrowing(Vector3 mousePos) {
        //FIXME to uproscic mozna ale tak przejrzysciej?
        if(!isOnIce)this.stopMoving();

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

        ball.GetComponent<IcyBallController>().setPlayer(gameObject);
        ball.GetComponent<IcyBallController>().setCamera(camera);

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
        //FIXME patrz na velocity i ustaw Flip'a
    }

    public void triggerSpecialAnimation(string animation) {
        animator.SetTrigger(animation);
    }


    /*
    //FIXME To z MainPlayerController info powinno byc wysylane tutaj
    private void OnCollisionEnter2D(Collision2D other) { //FIXME sprawdzanie czy podloga
        if (other.gameObject.tag == "ICE") { 
            //isOnIce = true; 
        } else{
            //isOnIce = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "ICE") { 
        //    isOnIce = false; 
        } 
    }*/
}
