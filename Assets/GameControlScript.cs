using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour {

	public static GameControlScript instance = null; // instance of GameControl
	int numberOfBalls = 3; // number of attempts to throw
	int score = 0; // score counter
	public int maxScore = 6;
	public Text ballsText, scoreText, youLoseText, youWinText; // references to text objects
	public GameObject ball; // ball game object prefab to instantiate
	bool noMovingCans; // to check if all af the cans are not moving

	// Use this for initialization
	void Start () {
		// creating an instance of GameControl which is Singletone
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		// turn off Win and Lose signs
		youLoseText.gameObject.SetActive (false);
		youWinText.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// display number of attempts and score value
		ballsText.text = "Balls: " + numberOfBalls;
		scoreText.text = "Score: " + score;

		// if score value equals to 6 then call WinGameOver method
		if (score == maxScore) {
			WinGameOver ();
		}

		// if you lose last attempt
		if (numberOfBalls <= 0) 
		{
			// then check if some cans are still moving so they can fall off the table
			CheckIfCansLeft ();

			// if they are not moving and you hit off less then 6 cans
			if (noMovingCans && score < maxScore)

			// then the game is over
			LoseGameOver (); 
		}

	}

	// decrease number of balls and instantiate new one if you have more attempts to throw
	public void DecreaseNumberOfBalls()
	{
		numberOfBalls -= 1;
		if (numberOfBalls > 0)
		Instantiate (ball, new Vector2 (-6f, -2f), Quaternion.identity);
	}

	// increase score value by 1
	public void IncreaseScoreNumber()
	{
		score += 1;
	}

	// restart current scene
	void RestartGame()
	{
		SceneManager.LoadScene ("Scene01");
	}

	// if you Lose
	void LoseGameOver()
	{
		// then turn on the You Lose sign
		youLoseText.gameObject.SetActive (true);

		// and restart game in 4 secconds
		Invoke ("RestartGame", 4f);
	}

	// if you Win
	void WinGameOver()
	{
		// turn on You Win sign
		youWinText.gameObject.SetActive (true);

		// restart game in 4 secconds
		Invoke ("RestartGame", 4f);
	}

	// check if all of the cans stoped moving
	void CheckIfCansLeft()
	{
		int cansNotMoving = 0; // number of not moving cans

		// get an array of all of the cans in the scene by their tags
		GameObject[] cansLeft = GameObject.FindGameObjectsWithTag ("Can");

		// check if velocity of each of the can is 0
		for (int i = 0; i < cansLeft.Length; i++) {
			if (cansLeft [i].GetComponent<Rigidbody2D> ().velocity.x == 0
				&& cansLeft [i].GetComponent<Rigidbody2D> ().velocity.y == 0)

				// if it is then increase number of not moving cans
				cansNotMoving += 1;
		}

		// all of the cans are not moving then noMovingCans is true
		if (cansNotMoving == cansLeft.Length)
			noMovingCans = true;
	}

}
