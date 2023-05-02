using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public class GameController : MasterGameController {
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

		private Score _score;

		public override void Start() {
			base.Start();
			_score = FindAnyObjectByType<Score>();
		}

		public override void UpdateLogic() {
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
			DamageTaken();
			player.Hit();

			_score.GotHit();

			foreach (ObstacleController o in currObstacles) {
				o.Pause();
			}
		}

		public void GetUp() {
			StartGame();

			foreach (ObstacleController o in currObstacles) {
				o.Resume();
			}
		}

		public override void EndGame() {
			base.EndGame();

			player.End();
		}
	}
}