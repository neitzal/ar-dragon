using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodEatWave : MonoBehaviour {

	private float segmentInitScale;

	public float eatWaveSpeed = 4.0f;
	public float eatWaveLength = 4.0f;
	public float eatWaveAmplitude = 0.8f;
	private List<float> eatWavePositions = new List<float>();

	private List<Rigidbody> segmentBodies;


	// Use this for initialization
	void Start () {
		segmentBodies = GetComponent<HeadMovement>().segmentBodies;
		
		segmentInitScale = segmentBodies[0].transform.localScale.x;
	
	}

	public void InitiateWave() {
		eatWavePositions.Add(0.0f);
	}

	public void UpdateWave() {
		float[] relScales = new float[segmentBodies.Count];

		foreach (float eatWavePosition in eatWavePositions) {
			for (int i = 0; i < segmentBodies.Count; i++) {
				if (i <= Mathf.CeilToInt(eatWavePosition) && i >= Mathf.CeilToInt(eatWavePosition - eatWaveLength)) {
					float d = eatWavePosition - i;
					relScales[i] += eatWaveAmplitude * Mathf.Max(0, Mathf.Sin(Mathf.PI * d / eatWaveLength));
				} 
			}
		}

		for (int i = 0; i < segmentBodies.Count; i++) {
			segmentBodies[i].transform.localScale = segmentInitScale * (1 + relScales[i]) * Vector3.one;
		}
	}


	void Update() {
		for (int i = 0; i < eatWavePositions.Count; i++) {
			eatWavePositions[i] += Time.smoothDeltaTime * eatWaveSpeed;
		}
		UpdateWave();
	}
}
