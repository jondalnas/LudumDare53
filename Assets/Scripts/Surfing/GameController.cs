using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surfing {
	public class GameController : MasterGameController {
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

		public override void Update() {
			base.Update();

			balanceBar.TargetValue = targetBalance;
		}

		public override void UpdateLogic() {
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
	
		public void Loose() {
			DamageTaken();

			score.GotHit();
			player.FallDown();
		}

		public void GotUp() {
			StartGame();

			hasHitTarget = true;
			targetBalance = 0.5f;
			player.Reset();
		}
	}
}