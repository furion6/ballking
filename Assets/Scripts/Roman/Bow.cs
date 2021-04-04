using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrow;
    public Transform firePoint;
    public GameObject player;
    private ClassHolder classHolder;

    public float  minPower = 5f;
	public float  maxPower = 12f;
	public float maxHoldTime = 1.0f;
	public float minHoldTime = 0.1f;
	private float holdTime = 0f;

	private float holdStartTimer = 0f;
	public bool onPlayer = true;

    Vector2 lookDirection;
    float lookAngle;
    private SmoothCamera2D smoothCamera;
    public GameObject camera;

    void Start()
    {
        classHolder = player.GetComponent<ClassHolder>();
        smoothCamera = camera.GetComponent<SmoothCamera2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //if(classHolder.characterClass != CharacterClass.ROMAN) return;

        setFirePointRotation();
        if (Input.GetMouseButtonDown(0))
        {
            holdStartTimer = Time.time;
        }

		if (Input.GetMouseButtonUp(0) && onPlayer)
        {
           holdTime = Time.time - holdStartTimer;
		   shootArrow();
		   onPlayer = false;
        }
    }

    private void shootArrow(){
        GameObject newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //newArrow.GetComponent<Rigidbody2D>().velocity = (mousePosition - new Vector2(firePoint.position.x, firePoint.position.y)) * launchForce;

       // GameObject ball = GameObject.Instantiate(bullet, firePoint.position, Quaternion.Euler(0,0,lookAngle));
        smoothCamera.target = newArrow.transform;

        newArrow.GetComponent<JavelinController>().setPlayer(gameObject);
        newArrow.GetComponent<JavelinController>().setCamera(camera);


        float actualHoldTime =  Mathf.Clamp(holdTime, minHoldTime, maxHoldTime);
        float holdTimePercentage = actualHoldTime/maxHoldTime;
        float power = holdTimePercentage * (maxPower-minPower) + minPower;

        newArrow.GetComponent<Rigidbody2D>().velocity = firePoint.right * power;

    }

    public void canThrow()
	 {
	 	onPlayer = true;
	 }

	private void setFirePointRotation(){
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x)  * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
	}

}
