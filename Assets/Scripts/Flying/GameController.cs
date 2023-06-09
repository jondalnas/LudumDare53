using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flying {
	public class GameController : MasterGameController {
		public float spawnsPerSecond = 3;
		private float _timeToNextSpwan;

		public PlayerController player;

		[System.Serializable]
		public class SpwanChance {
			public float chance;
			public GameObject enemy;
		}

		public SpwanChance[] spawnChances;

		public float seekingChance = 0.4f;

		private Score _score;

		public GameObject cloud;
		public float cloudsPerSecond = 3;
		private float _timeToNextCloud;

		public override void Start() {
			base.Start();
			_score = FindAnyObjectByType<Score>();
		}

		public override void Update() {
			base.Update();
			_timeToNextCloud -= Time.deltaTime;
			if (_timeToNextCloud < 0) {
				Instantiate(cloud, new Vector3(100, player.maxPosition * Random.Range(-1f, 1f), 1), Quaternion.identity);

				_timeToNextCloud = 1 / cloudsPerSecond * Random.Range(0.5f, 1.5f);
			}
		}

		public override void UpdateLogic() {
			_timeToNextSpwan -= Time.deltaTime;
			if (_timeToNextSpwan < 0) {
				Spwan();

				_timeToNextSpwan = 1 / spawnsPerSecond * Random.Range(0.5f, 1.5f);
			}
		}

		private void Spwan() {
			GameObject spawn = spawnChances[0].enemy;

			float r = Random.value;
			foreach (SpwanChance c in spawnChances) {
				r -= c.chance;

				if (r < 0) {
					spawn = c.enemy;
					break;
				}
			}

			if (seekingChance > Random.value) {
				Instantiate(spawn, new Vector3(100, player.transform.position.y, -1), Quaternion.identity);
			} else {
				Instantiate(spawn, new Vector3(100, player.maxPosition * Random.Range(-1f, 1f), -1), Quaternion.identity);
			}
		}

		public void HitEnemy() {
			if (!IsInGame()) return;

			player.Hit();

			_score.GotHit();
		}

		public override void EndGame() {
			base.EndGame();

			player.End();
		}
	}
}