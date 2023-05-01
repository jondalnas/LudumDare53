using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public class SnowpileController : ObstacleController {
		public List<Sprite> sprites;

		public override void Start() {
			base.Start();
			GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
		}
	}
}