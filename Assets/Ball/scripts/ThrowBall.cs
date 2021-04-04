using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
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
	public float angularVelo = 100.0f;
	public bool onPlayer = true;

    Vector2 lookDirection;
    float lookAngle;
	public ParticleSystem particleSystem;
	public GameObject camera;
	private SmoothCamera2D smoothCamera;
	private ClassHolder classHolder;

	private PlayerController playerController;


	void Start() {
		classHolder = player.GetComponent<ClassHolder>();
		smoothCamera = camera.GetComponent<SmoothCamera2D>();
		playerController = player.GetComponent<PlayerController>();
	}
    void Update()
    {
		//if(classHolder.characterClass != CharacterClass.NEANDERTAL) return;
		setFirePointRotation();
        if (Input.GetMouseButtonDown(0))
        {
            holdStartTimer = Time.time;
        }

		if (Input.GetMouseButtonUp(0) && onPlayer)
        {
        	holdTime = Time.time - holdStartTimer;
			throwBall();
			playerController.setIsThrowing(true);
			onPlayer = false;
        }
    }

	private void throwBall(){
			GameObject ball = GameObject.Instantiate(bullet, firePoint.position, Quaternion.Euler(0,0,lookAngle));
			smoothCamera.target = ball.transform;

			ball.GetComponent<BallController>().setPlayer(gameObject);
			ball.GetComponent<BallController>().setCamera(camera);

		 	//bullet.transform.position = firePoint.position;
            //bullet.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

			float actualHoldTime =  Mathf.Clamp(holdTime, minHoldTime, maxHoldTime);
			float holdTimePercentage = actualHoldTime/maxHoldTime;
			float power = holdTimePercentage * (maxPower-minPower) + minPower;

            ball.GetComponent<Rigidbody2D>().velocity = firePoint.right * power;
			ball.GetComponent<Rigidbody2D>().angularVelocity = angularVelo;
	}

	public void playDust(){
		particleSystem.Play();
	}

	 public void canThrow()
	 {
	 	onPlayer = true;
		smoothCamera.target = gameObject.transform;
	 }

	private void setFirePointRotation(){
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x)  * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
	}



  
 
}