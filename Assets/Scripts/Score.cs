using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
	public static float SCORE;
	public static int LIVES = 3;

	public float scoreChange = 10;

	public float hitPenalty = 10;
	public float invTime = 0.5f;
	private float _time;


	void Update() {
		_time -= Time.deltaTime;
		if (_time > 0) return;

		SCORE += scoreChange * Time.deltaTime;
	}

	public void GotHit() {
		if (_time > 0) return;

		SCORE -= hitPenalty;
		_time = invTime;
		LIVES--;
	}
}
