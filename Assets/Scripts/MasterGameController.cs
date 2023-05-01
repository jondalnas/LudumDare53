using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MasterGameController : MonoBehaviour {
	public enum GameState {
		INTRO, OUTRO, IN_GAME, DAMAGE_TAKEN
	}

	private GameState _state = GameState.INTRO;

	public virtual void Update() {
		if (!IsInGame()) return;

        UpdateLogic();
	}

    public abstract void UpdateLogic();

	public void StartGame() {
		_state = GameState.IN_GAME;
	}

	public void EndGame() {
		_state = GameState.OUTRO;
	}

	public void DamageTaken() {
		_state = GameState.DAMAGE_TAKEN;
	}

	public bool IsInGame() {
		return _state == GameState.IN_GAME;
	}
}
