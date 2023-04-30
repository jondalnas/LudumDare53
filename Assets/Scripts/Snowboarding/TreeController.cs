using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowboarding {
	public class TreeController : MonoBehaviour {
		public void Destroy() {
			Destroy(gameObject);
		}
	}
}