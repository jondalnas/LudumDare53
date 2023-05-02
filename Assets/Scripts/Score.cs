using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {
	public static float SCORE;
	public static int LIVES = 3;

	public float scoreChange = 10;

	public float hitPenalty = 10;
	public float invTime = 0.5f;
	private float _time;

	private TextMeshProUGUI _scoreText;
	private List<Transform> _lives = new();

	void Start() {
		if (SceneManager.GetActiveScene().buildIndex >= 5) {
			_scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();

			return;
		}

		_scoreText = GameObject.FindGameObjectWithTag("Score").transform.Find("Score").GetComponent<TextMeshProUGUI>();
		foreach (Transform child in GameObject.FindGameObjectWithTag("Lives").transform) {
			_lives.Add(child);
		}
	}

	void Update() {
		_scoreText.text = ((int) SCORE) + "00";

		if (SceneManager.GetActiveScene().buildIndex >= 5) return;

		if (LIVES == 0) {
			_lives[0].gameObject.SetActive(false);
			_lives[1].gameObject.SetActive(false);
			_lives[2].gameObject.SetActive(false);
		} else if (LIVES == 1) {
			_lives[0].gameObject.SetActive(true);
			_lives[1].gameObject.SetActive(false);
			_lives[2].gameObject.SetActive(false);
		} else if (LIVES == 2) {
			_lives[0].gameObject.SetActive(true);
			_lives[1].gameObject.SetActive(true);
			_lives[2].gameObject.SetActive(false);
		} else if (LIVES == 3) {
			_lives[0].gameObject.SetActive(true);
			_lives[1].gameObject.SetActive(true);
			_lives[2].gameObject.SetActive(true);
		}

		_time -= Time.deltaTime;
		if (_time > 0) return;

		SCORE += scoreChange * Time.deltaTime;
	}

	public void GotHit() {
		if (_time > 0) return;

		SCORE -= hitPenalty;
		_time = invTime;
		LIVES--;

		if (LIVES < 0) {
			Die();
			return;
		}
	}

	public void Die() {
		FindAnyObjectByType<Game>().Fail();
	}

	public static void Reset() {
		SCORE = 0;
		LIVES = 3;
	}
}
