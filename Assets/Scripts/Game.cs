using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    private int _currScene;

	private AudioSource _aud;

    void Start() {
        if (FindObjectsOfType<Game>().Length > 1) Destroy(gameObject);

		_aud = GetComponent<AudioSource>();

        _currScene = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject);
    }

	public void Next() {
        _currScene++;

		if (_currScene > 4) {
			StopAudio();
		}

		if (_currScene > 5) {
            Reset();
            return;
        }



        SceneManager.LoadScene(_currScene);
    }

    public void Reset() {
        Score.Reset();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

	public void Fail() {
		StopAudio();
		SceneManager.LoadScene(6);
	}

	public void StopAudio() {
		_aud.Stop();
	}
}
