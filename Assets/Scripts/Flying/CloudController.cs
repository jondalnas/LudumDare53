using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flying {
    public class CloudController : MonoBehaviour {
        public float speed = 72;

        public Sprite[] sprites;

        void Start() {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }

        void Update() {
                transform.position += Vector3.left * speed * Time.deltaTime;

                if (transform.position.x < -110) Destroy(gameObject);
        }
    }
}