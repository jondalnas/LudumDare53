using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flying {
	public class EnemyController : MonoBehaviour {
		public float speed = 144;

		private Animator _anim;
		private AudioSource _audio;

		void Start() {
			_anim = GetComponent<Animator>();
			_audio = GetComponent<AudioSource>();
		}

		void Update() {
			transform.position += Vector3.left * speed * Time.deltaTime;

			if (transform.position.x < -110) Destroy(gameObject);
		}

		public void Hit() {
			Destroy(GetComponent<Collider2D>());

			_anim.SetTrigger("Dead");
			_audio.Play();
		}

		public void Destroy() {
			Destroy(gameObject);
		}
	}
}