using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finnish : MonoBehaviour {
    private Game _game;
	void Start() {
        _game = FindAnyObjectByType<Game>();
	}

    public void Next() {
        _game.Next();
    }

    public void Reset() {
        _game.Reset();
    }
}
