using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfing {
	public class GameController : MonoBehaviour {
		private enum GameState {
			INTRO, OUTRO, IN_GAME, FELL_OFF
		}

		private GameState state = GameState.IN_GAME;

		public Score score;
		public BarController balanceBar;
		public PlayerController player;

		[HideInInspector]
		public float targetBalance = 0.5f;

		private float _oldTargetBalance, _newTargetBalance;
		private bool hasHitTarget = true;
		public float targetBalanceSpeed = 0.15f;
		public float maxTargetDist = 0.4f;

		public float turnChancePerSec = 0.8f;

		void Start() {
			score = GameObject.FindAnyObjectByType<Score>();
		}

		void Update() {
			if (state == GameState.IN_GAME) {
				if (!hasHitTarget) {
					float balanceDir = _newTargetBalance - targetBalance > 0 ? 1 : -1;

					targetBalance += balanceDir * targetBalanceSpeed * Time.deltaTime;

					if (Mathf.Abs(_newTargetBalance - targetBalance) < 0.01f) {
						hasHitTarget = true;
					}
				} else {
					// This is a little off, the math does not check out
					if (turnChancePerSec * Time.deltaTime > Random.value) {
						_oldTargetBalance = targetBalance;
						_newTargetBalance = targetBalance + Random.Range(-maxTargetDist, maxTargetDist);

						if (_newTargetBalance < 0.1f) _newTargetBalance = 0.1f;
						if (_newTargetBalance > 0.9f) _newTargetBalance = 0.9f;

						hasHitTarget = false;
					}
				}
			}

			balanceBar.TargetValue = targetBalance;
		}
	
		public void Loose() {
			Debug.Log("Sploosh!");
			score.GotHit();
			player.FallDown();

			state = GameState.FELL_OFF;
		}

		public void GotUp() {
			state = GameState.IN_GAME;

			hasHitTarget = true;
			targetBalance = 0.5f;
			player.Reset();
		}

		public bool InGame() {
			return state == GameState.IN_GAME;
		}
	}
}