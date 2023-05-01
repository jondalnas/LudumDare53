using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skydiving {
	public class GameController : MonoBehaviour {
		public float spawnsPerSecond = 1;
		private float _timeToNextSpwan;

		public PlayerController player;

		[System.Serializable]
		public class SpwanChance {
			public float chance;
			public GameObject enemy;
		}

		public SpwanChance[] spawnChances;

		private Score _score;

		void Start() {
			_score = FindAnyObjectByType<Score>();
		}

		void Update() {
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

			Instantiate(spawn, new Vector3(player.posBounds.x * Random.Range(-1f, 1f), 76, 0), Quaternion.identity);
		}

		public void Hit() {
			player.GotHit();

			_score.GotHit();
		}
	}
}