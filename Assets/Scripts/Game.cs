using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    private int _currScene;

    void Start() {
        _currScene = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject);
    }

	public void Next() {
        _currScene++;

        if (_currScene > SceneManager.sceneCountInBuildSettings) _currScene = 0;

        SceneManager.LoadScene(_currScene);
    }
}
