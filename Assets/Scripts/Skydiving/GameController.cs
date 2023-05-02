using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skydiving {
	public class GameController : MasterGameController {
		public float spawnsPerSecond = 1;
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

		public override void Start() {
			base.Start();
			_score = FindAnyObjectByType<Score>();
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
				Instantiate(spawn, new Vector3(player.transform.position.x, 76, 0), Quaternion.identity);
			} else {
				Instantiate(spawn, new Vector3(player.posBounds.x * Random.Range(-1f, 1f), 76, 0), Quaternion.identity);
			}
		}

		public void Hit() {
			player.GotHit();

			_score.GotHit();
		}
	}
}