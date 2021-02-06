using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
	public GameObject player;
    public float  minPower = 5f;
	public float  maxPower = 12f;
	public float maxHoldTime = 1.0f;
	public float minHoldTime = 0.1f;
	private float holdTime = 0f;

	private float holdStartTimer = 0f;

	public bool onPlayer = true;

    Vector2 lookDirection;
    float lookAngle;
	private Vector2 startingBallPosition;

	void Start() {
			bullet.GetComponent<Rigidbody2D>().isKinematic = true;
			startingBallPosition = bullet.transform.localPosition;
	}
    void Update()
    {
		setFirePointRotation();
        if (Input.GetMouseButtonDown(0))
        {
            holdStartTimer = Time.time;
        }

		if (Input.GetMouseButtonUp(0) && onPlayer)
        {
           holdTime = Time.time - holdStartTimer;
		   throwBall();
		   onPlayer = false;
        }

		if (Input.GetKey(KeyCode.DownArrow)) joinToPlayer();
    }

	private void throwBall(){
			bullet.GetComponent<Rigidbody2D>().isKinematic = false;
		 	bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
			Debug.Log("holdtime-> " + holdTime);

			float actualHoldTime =  Mathf.Clamp(holdTime, minHoldTime, maxHoldTime);
			float holdTimePercentage = actualHoldTime/maxHoldTime;
			float power = holdTimePercentage * (maxPower-minPower) + minPower;

            bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * power;
			Debug.Log("power-> " + power);
	}

	private void joinToPlayer()
	{
		onPlayer = true;
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		bullet.GetComponent<Rigidbody2D>().isKinematic = true;
		bullet.transform.localPosition = startingBallPosition;

	}

	private void setFirePointRotation(){
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x)  * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
	}

	private void teleportPlayer(){
		player.transform.position = new Vector2(bullet.transform.position.x, bullet.transform.position.y + 1.5f);
		joinToPlayer();
	}

   public void OnCollisionEnter2D(Collision2D theCollision ){
   if (theCollision.gameObject.layer == LayerMask.NameToLayer("normal_floor")) 
   {
     foreach(ContactPoint2D contact in theCollision.contacts)
     {
       if(contact.normal.y > 0.1f)
       {
         teleportPlayer();
       }
     }
   }
 }
}