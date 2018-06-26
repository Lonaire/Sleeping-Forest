using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeController : MonoBehaviour {
	
	[SerializeField] private Sprite[] stages;
	[SerializeField] private Button Tree;

	public void OnProgressChanged(Slider slider) {
		float value = slider.value;
		int stage;
		if (value >= 0 && value < 0.16f)
			stage = 0;
		else if (value >= 0.16f && value < 0.36f)
			stage = 1;
		else if (value >= 0.36f && value < 0.56f)
			stage = 2;
		else if (value >= 0.56f && value < 0.71f)
			stage = 3;
		else if (value >= 0.71f && value < 0.85f)
			stage = 4;
		else
			stage = 5;

		Tree.GetComponent<Image> ().sprite = stages [stage];
	}
}
