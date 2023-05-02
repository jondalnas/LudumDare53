using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    private int _currScene;

    void Start() {
        if (FindObjectsOfType<Game>().Length > 1) Destroy(gameObject);

        _currScene = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject);
    }

	public void Next() {
        _currScene++;

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
}
