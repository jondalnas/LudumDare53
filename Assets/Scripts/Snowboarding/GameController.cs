using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public class GameController : MonoBehaviour {
		public float obstaclesPerSecond = 1;
		private float _timeToNextObstacle;

		[System.Serializable]
		public class Obstacle {
			public float chance;
			public GameObject obstacle;
		}

		public List<Obstacle> obstacles;

		public PlayerController player;

		[HideInInspector]
		public List<ObstacleController> currObstacles;

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

			_timeToNextObstacle -= Time.deltaTime;
			if (_timeToNextObstacle > 0) return;

			_timeToNextObstacle = Random.Range(0.5f, 1.5f) * 1 / obstaclesPerSecond;
			Spawn();
		}

		private void Spawn() {
			GameObject obstacle = obstacles[0].obstacle;

			float r = Random.value;
			foreach (Obstacle o in obstacles) {
				r -= o.chance;

				if (r < 0) {
					obstacle = o.obstacle;
					break;
				}
			}

			float x = Random.Range(-player.maxPosition, player.maxPosition);

			Instantiate(obstacle, new Vector3(x, 0, 0), Quaternion.identity);
		}

		public void Hit() {
			_state = GameState.HIT;
			player.Hit();

			_score.GotHit();

			foreach (ObstacleController o in currObstacles) {
				o.Pause();
			}
		}

		public void GetUp() {
			_state = GameState.IN_GAME;

			foreach (ObstacleController o in currObstacles) {
				o.Resume();
			}
		}

		public bool IsInGame() {
			return _state == GameState.IN_GAME;
		}
	}
}