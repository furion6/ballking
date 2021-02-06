using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanFallCheckZoneScript : MonoBehaviour {

	// check if can collides with this zone to count number of fallen cans
	void OnTriggerEnter2D(Collider2D col)
	{
		// if FallChecker cans child collides with the zone then
		// destroy this checker so can counted only once
		if (col.gameObject.tag.Equals ("FallChecker")) {

			// increase score number
			GameControlScript.instance.IncreaseScoreNumber ();

			// destroy FallChecker cans child
			Destroy (col.gameObject);
		}
	}

}
