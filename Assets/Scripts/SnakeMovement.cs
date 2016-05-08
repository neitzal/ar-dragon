using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour {

	private List<Vector2> trajectory = new List<Vector2>();
	private Vector2[] segmentPositions;
	private float[] segmentAngles;
	public int segmentOffset = 4;
	public float speed = 1.0f;
	public float rotationSpeed = 1.0f;
	public float trajectoryRecordDelay = 0.5f; // seconds
	public int nTrajectoryPoints = 4;
	public int nSegments = 3;
	private float angle = 0.0f; // radians

	private float nextTrajectoryPointTime;
	private Vector2 currentPosition = new Vector2(0, 0);

	private List<GameObject> markers = new List<GameObject>();
	private List<GameObject> segments = new List<GameObject>();
	public GameObject markerPrefab;
	public GameObject segmentPrefab;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < nTrajectoryPoints; i++) {
			trajectory.Add(0.01f * Vector2.left * (nTrajectoryPoints - i));
//			markers.Add((GameObject) (Instantiate(markerPrefab)));
		}
		for (int i = 0; i < nSegments; i++) {
			segments.Add((GameObject) (Instantiate(segmentPrefab)));
		}

		nextTrajectoryPointTime = Time.time + trajectoryRecordDelay;
		segmentPositions = new Vector2[nSegments];
		segmentAngles = new float[nSegments];

		UpdatePositions();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			angle += rotationSpeed * Time.smoothDeltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			angle -= rotationSpeed * Time.smoothDeltaTime;
		}
		currentPosition += speed * Time.smoothDeltaTime * (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));

		if (Time.time > nextTrajectoryPointTime){
			nextTrajectoryPointTime += trajectoryRecordDelay;
			trajectory.Add(currentPosition);
			trajectory.RemoveAt(0);
		}

		UpdatePositions();
		UpdateObjects();
	}

	float getSegAngle(int index) {
		return -Mathf.Atan2(trajectory[index].y - trajectory[index + 1].y, trajectory[index].x - trajectory[index + 1].x);
	}

	void UpdatePositions() {
		float currentSegAngle = getSegAngle(0);
		float nextSegAngle = getSegAngle(0);
		float prevSegAngle = currentSegAngle;
		for (int i = 0; i < nSegments; i++) { 
			float relPosition = i * (1.0f * (nTrajectoryPoints-2) / (nSegments-1)) + 1 - (nextTrajectoryPointTime - Time.time) / trajectoryRecordDelay;
			int index = ((int) relPosition);
			float remainder = relPosition - index;

			Vector2 pos = Vector2.zero;
			float segAngle = 0;

			currentSegAngle = getSegAngle(index);
			if (index > 0) {
				prevSegAngle = getSegAngle(index - 1);
			} else {
				prevSegAngle = currentSegAngle;
			}

			if (index < trajectory.Count - 2) {
				nextSegAngle = getSegAngle(index + 1);
			} else {
				nextSegAngle = currentSegAngle;
			}

			if (Mathf.Abs(nextSegAngle - currentSegAngle) > Mathf.PI) {
				currentSegAngle += Mathf.Sign(nextSegAngle - currentSegAngle) * 2*Mathf.PI;
			}

			if (Mathf.Abs(currentSegAngle - prevSegAngle) > Mathf.PI) {
				prevSegAngle += Mathf.Sign(currentSegAngle - prevSegAngle) * 2*Mathf.PI;
			}

			if (index < trajectory.Count - 1) {				
				pos = (1 - remainder)*trajectory[index] + remainder*trajectory[index + 1];

			} else {
				pos = trajectory[index];
			}
			segAngle = Mathf.Max(0, 0.5f - remainder) * prevSegAngle + 
				(1 - Mathf.Abs(remainder - 0.5f))*currentSegAngle + 
				Mathf.Max(0, remainder - 0.5f)*nextSegAngle;
			segmentPositions[i] = pos;
			segmentAngles[i] = segAngle;
		}
	}

	void UpdateObjects() {
		for (int i = 0; i < markers.Count; i++) {
			GameObject marker = markers[i]; 
			marker.transform.position = new Vector3(trajectory[i].x, 0, trajectory[i].y );
		}

		for (int i = 0; i < segmentPositions.Length; i++) { 
			GameObject segment = segments[i];
			segment.transform.position = new Vector3(segmentPositions[i].x, 0, segmentPositions[i].y);
			segment.transform.eulerAngles = Mathf.Rad2Deg * segmentAngles[i] * Vector3.up;
		}
	}
}
