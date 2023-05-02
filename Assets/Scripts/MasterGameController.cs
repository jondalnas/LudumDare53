using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MasterGameController : MonoBehaviour {
	public enum GameState {
		INTRO, OUTRO, IN_GAME, DAMAGE_TAKEN
	}

	private const float _gameLength = 10;
	private float _timer;

	private GameState _state = GameState.INTRO;

	[HideInInspector]
	public Game _game;

	public virtual void Start() {
		_game = FindAnyObjectByType<Game>();
	}

	public virtual void Update() {
		if (IsNotInCutscene()) {
			_timer += Time.deltaTime;

			if (_timer > _gameLength) {
				EndGame();
			}
		}

		if (!IsInGame()) return;

        UpdateLogic();
	}

    public abstract void UpdateLogic();

	public void StartGame() {
		_state = GameState.IN_GAME;
	}

	public virtual void EndGame() {
		_state = GameState.OUTRO;
	}

	public void DamageTaken() {
		_state = GameState.DAMAGE_TAKEN;
	}

	public bool IsInGame() {
		return _state == GameState.IN_GAME;
	}

	public bool IsNotInCutscene() {
		return _state == GameState.IN_GAME || _state == GameState.DAMAGE_TAKEN;
	}

	public void Next() {
		_game.Next();
	}
}
