using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public class GameController : MonoBehaviour {
		public float obsticalsPerSecond = 1;
		private float _timeToNextObstical;

		[System.Serializable]
		public class Obstical {
			public float chance;
			public GameObject obstical;
		}

		public List<Obstical> obsticals;

		public PlayerController player;

		[HideInInspector]
		public List<ObsticalController> currObsticals;

		public enum GameState {
			INTRO, OUTRO, IN_GAME, HIT
		}

		private GameState _state = GameState.IN_GAME;

		private Score _score;

		void Start() {
			_score = FindAnyObjectByType<Score>();
		}

		void Update() {
			if (_state != GameState.IN_GAME) return;

			_timeToNextObstical -= Time.deltaTime;
			if (_timeToNextObstical > 0) return;

			_timeToNextObstical = Random.Range(0.5f, 1.5f) * 1 / obsticalsPerSecond;
			Spawn();
		}

		private void Spawn() {
			GameObject obstical = obsticals[0].obstical;

			float r = Random.value;
			foreach (Obstical o in obsticals) {
				r -= o.chance;

				if (r < 0) {
					obstical = o.obstical;
					break;
				}
			}

			float x = Random.Range(-player.maxPosition, player.maxPosition);

			Instantiate(obstical, new Vector3(x, 0, 0), Quaternion.identity);
		}

		public void Hit() {
			_state = GameState.HIT;
			player.Hit();

			_score.GotHit();

			foreach (ObsticalController o in currObsticals) {
				o.Pause();
			}
		}

		public void GetUp() {
			_state = GameState.IN_GAME;

			foreach (ObsticalController o in currObsticals) {
				o.Resume();
			}
		}

		public bool IsInGame() {
			return _state == GameState.IN_GAME;
		}
	}
}