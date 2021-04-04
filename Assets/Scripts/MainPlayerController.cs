using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{

    public float maxHoldTime = 1.0f;
    public float minHoldTime = 0.1f;
    public PlayerControllerInterface playerController;
    private Rigidbody2D rigidbody2D;
    public Transform bottom;
    private float moveHorizontal;
	private float holdTime = 0f;
    private float holdStartTimer = 0f;
    public Transform firePoint; //Punkt, skąd rzucana jest broń

    private bool onPlayer = true;
    private bool isThrowing = false;
    private bool userInteractionDisabled = false;

    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public bool isFalling = false;
    public bool isSliding = false;
    public GameObject goSlider;

    public float MIN_VELOCITY = -30.0f; 

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start() {

        //playerController = GetComponent<NeandertalController>();
        //playerController = GetComponent<IcyNeandertalController>();
        //playerController = GetComponent<EgyptianController>();
        playerController = GetComponent<RomanController>();
        playerController.init();
    }

    // Update is called once per frame
    void Update()
    {
        if(userInteractionDisabled) return;
        updateFallingState(); //FIXME POCZYTAC CZY NIE DO FixedUpdate()
        updateSlidingState(); //JW
        moveHorizontal = Input.GetAxis ("Horizontal");
        //FIXME czemu to nie moze byc w getmousedown?
        
        if (Input.GetMouseButtonDown(0) && onPlayer)
        {
            leftMouse(Input.mousePosition);
        }

		if (Input.GetMouseButtonUp(0) && onPlayer && isThrowing)
        {
            leftMouseRelease(Input.mousePosition);
        }
    }

        void FixedUpdate() {
        if(userInteractionDisabled) return;
        if(!isThrowing /*&& onPlayer*/)
            playerController.move(moveHorizontal);

        clampVelocities();
    }

    public void updateFallingState(){
        //TODO prędkość y z configu
        //TODO ta długość vektora z transforma? albo z configu
        RaycastHit2D hit = Physics2D.Raycast(bottom.position, -Vector2.up, 1.5f, LayerMask.GetMask("normal_floor"));
        isFalling = (!hit.collider) && (rigidbody2D.velocity.y < -0.1f);
    }

    public void updateSlidingState(){
        //TODO prędkość y z configu
        //TODO ta długość vektora z transforma? albo z configu
        //RaycastHit2D hit = Physics2D.Raycast(bottom.position, -Vector2.up, 1.5f, LayerMask.GetMask("floor"));
        //isSliding = (hit.collider);
    }
    public void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public void leftMouse(Vector3 mousePos){
        holdStartTimer = Time.time;
        //playerController.stopMoving();
        playerController.startThrowing(Camera.main.ScreenToWorldPoint(mousePos));
        isThrowing = true;
    }

    public void leftMouseRelease(Vector3 mousePos){
        setFirePointRotation(mousePos);//SetFirePointRotation problem z flipem
        holdTime = Time.time - holdStartTimer;
        float actualHoldTime =  Mathf.Clamp(holdTime, minHoldTime, maxHoldTime);
        float holdTimePercentage = actualHoldTime/maxHoldTime;
        playerController.endThrowing(holdTimePercentage, firePoint.transform);
        //throwBall();
        //playerController.setIsThrowing(true);
        onPlayer = false;
    }

    public void leftMouseReleaseFixedPercentage(Vector3 mousePos, float percentage){
        setFirePointRotation(mousePos);//SetFirePointRotation problem z flipem
        playerController.endThrowing(percentage, firePoint.transform);

        onPlayer = false;
    }

    public void disableUserInteraction() {
        userInteractionDisabled = true;
    }

    public void enableUserInteraction() {
        userInteractionDisabled = false;
    }


    private void clampVelocities(){
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Clamp(rigidbody2D.velocity.y, MIN_VELOCITY, 30.0f));
    }

    public void releaseBall(){
        playerController.release();
        isThrowing = false;
    }

    public void move(float x){
        playerController.move(x);
    }

    public void stopMoving(){
        Debug.Log("jakies stop moving?");
        playerController.stopMoving();
    }

    public void setFirePointRotation(Vector3 mousePos) {
        Vector2 lookDirection = Camera.main.ScreenToWorldPoint(mousePos) - new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x)  * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
	}

    public void ballLanded() {
        onPlayer = true;
    }

    public void triggerSpecialAnimation(string anim) {
        playerController.triggerSpecialAnimation(anim);
    }

    private void OnTriggerEnter2D(Collider2D other) { //FIXME sprawdzanie czy podloga
        if(other.gameObject.tag == "SLIDE"){
            isSliding = true;
            goSlider = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "SLIDE"){
            isSliding = false;
        }
    }

}
