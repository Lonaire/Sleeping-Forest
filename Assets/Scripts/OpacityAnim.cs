using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityAnim : MonoBehaviour {
	private Text clickInfo;
	private Color color;
	private bool up = false;
	private bool down = false;
	public float Speed;

	void Start () {
		clickInfo = GetComponent<Text> ();
		color = new Color ();
		clickInfo.color = new Color (clickInfo.color.r, clickInfo.color.g, clickInfo.color.b, 0);
	}
	
	void Update () {
		color = clickInfo.color;
		if (up) {
			if (color.a < 1)
				color.a += Speed / 100;
			else {
				up = false;
				down = true;
			}
		} else if (down) {
			if (color.a > 0)
				color.a -= Speed / 100;
			else
				down = false;
		}
		clickInfo.color = new Color (color.r, color.g, color.b, color.a);
	}

	public void OnClick() {
		clickInfo.color = new Color (clickInfo.color.r, clickInfo.color.g, clickInfo.color.b, 0);
		up = true;
		down = false;
	}
}
