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

        void Update() {
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
    }
}