using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour {
	public Sprite arrowUp, arrowDown, arrowLeft, arrowRight, arrowUpLeft, arrowUpRight, arrowDownLeft, arrowDownRight;
	public Transform insideArrow, outsideArrow;
	private SpriteRenderer _insideArrowRenderer, _outsideArrowRenderer;
	
	public int insideWidth = 25, insideHeight = 12;
	public int outsideWidth = 19, outsideHeight = 8;

	private float _targetValue, _value;

	public float TargetValue {
		get { return _targetValue; }
		set { 
			_targetValue = value;
			if (_targetValue < 0) _targetValue = 0;
			if (_targetValue > 1) _targetValue = 1;
		}
	}

	public float Value {
		get { return _value; }
		set { 
			_value = value;
			if (_value < 0) _value = 0;
			if (_value > 1) _value = 1;
		}
	}

	void Start() {
		_insideArrowRenderer = insideArrow.GetComponent<SpriteRenderer>();
		_outsideArrowRenderer = outsideArrow.GetComponent<SpriteRenderer>();
	}

	void Update() {
		insideArrow.localPosition = new Vector3(-Mathf.Cos(_targetValue * Mathf.PI) * insideWidth / 2, Mathf.Sin(_targetValue * Mathf.PI) * insideHeight - 8, -1);
		outsideArrow.localPosition = new Vector3(-Mathf.Cos(_value * Mathf.PI) * outsideWidth / 2, Mathf.Sin(_value * Mathf.PI) * outsideHeight - 8, -1);

		if (_targetValue < 1/8f) {
			_insideArrowRenderer.sprite = arrowRight;
		} else if (_targetValue < 3/8f) {
			_insideArrowRenderer.sprite = arrowDownRight;
		} else if (_targetValue < 5/8f) {
			_insideArrowRenderer.sprite = arrowDown;
		} else if (_targetValue < 7/8f) {
			_insideArrowRenderer.sprite = arrowDownLeft;
		} else {
			_insideArrowRenderer.sprite = arrowLeft;
		}

		if (_value < 1/8f) {
			_outsideArrowRenderer.sprite = arrowLeft;
		} else if (_value < 3/8f) {
			_outsideArrowRenderer.sprite = arrowUpLeft;
		} else if (_value < 5/8f) {
			_outsideArrowRenderer.sprite = arrowUp;
		} else if (_value < 7/8f) {
			_outsideArrowRenderer.sprite = arrowUpRight;
		} else {
			_outsideArrowRenderer.sprite = arrowRight;
		}
	}
}
