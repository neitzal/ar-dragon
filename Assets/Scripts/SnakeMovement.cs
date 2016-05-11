using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeMovement : MonoBehaviour {

	private List<Vector2> waypoints = new List<Vector2>();
	private List<Vector2> segmentPositions;
	private List<float> segmentAngles;
	public int segmentOffset = 4;
	public float speed = 1.0f;
	public float rotationSpeed = 1.0f;
	public float trajectoryRecordSpacing = 0.2f; // units
	public int nInitialWayPoints = 4;
	private float waypointSegmmentRatio;
	public int nInitialSegments = 3;
	private float headAngle = 0.0f; // radians
	public int lengthIncreasePerFood = 3;
	public int nTorsoOffset = 3; // How many of the first body segments are ignored w.r.t. head collision
	private float movedDistance = 0.0f;

	private float nextTrajectoryPointDistance;
	private Vector2 headPosition = new Vector2(0, 0);

	private List<GameObject> markers = new List<GameObject>();
	private List<GameObject> segments = new List<GameObject>();
	public GameObject markerPrefab;
	public GameObject segmentPrefab;
	public GameObject snakeHead;

	private bool gameOver = false;

	public float wigglePeriod = 1.0f; // Seconds
	[Range(0.0f, 0.3f)]
	public float wiggleAmplitude = 0.5f;
	private float currentWigglePosition;

	private Vector2 arenaBound;

	private bool running = false;

	// Use this for initialization
	void Start () {
		var floor = GameObject.Find("Floor");
		arenaBound = 5.0f * new Vector2(
			floor.transform.localScale.x, 
			floor.transform.localScale.y);
		waypointSegmmentRatio = 1.0f * nInitialWayPoints / nInitialSegments;
		for (int i = 0; i < nInitialWayPoints; i++) {
			waypoints.Add(0.2f * Vector2.left * (nInitialWayPoints - i));
//			markers.Add((GameObject) (Instantiate(markerPrefab)));
		}
		for (int i = 0; i < nInitialSegments; i++) {
			segments.Add((GameObject) (Instantiate(segmentPrefab)));
		}

		nextTrajectoryPointDistance = movedDistance + trajectoryRecordSpacing;
		segmentPositions = new List<Vector2>();
		segmentAngles = new List<float>();
		for (int i = 0; i < nInitialSegments; i++) {
			segmentPositions.Add(Vector2.zero);
			segmentAngles.Add(0);
		}

		UpdatePositions();
	}


	public void UnPause() {
		running = true;
	}

	// Update is called once per frame
	void Update () {
		if (gameOver) {
			Color color = Time.time % 0.4f > 0.2 ? (new Color(0.5f,0.1f,0.1f)) : Color.black;
			foreach (var segment in segments) {
				segment.GetComponentInChildren<Renderer>().material.color = color;
				snakeHead.GetComponent<Renderer>().material.color = color;
			}
			return;
		}

		if (running == false) return;

		if (Input.GetKey(KeyCode.A)) {
			headAngle += rotationSpeed * Time.smoothDeltaTime;
		} else if (Input.GetKey(KeyCode.D)) {
			headAngle -= rotationSpeed * Time.smoothDeltaTime;
		}
		headPosition += speed * Time.smoothDeltaTime * (new Vector2(Mathf.Cos(headAngle), Mathf.Sin(headAngle)));
		movedDistance += speed * Time.smoothDeltaTime;
		checkArenaBound();

		if (movedDistance > nextTrajectoryPointDistance){
			nextTrajectoryPointDistance += trajectoryRecordSpacing;
			currentWigglePosition = wiggleAmplitude * Mathf.Sin(Time.time * 2 * Mathf.PI / wigglePeriod);
			waypoints.Add(headPosition + currentWigglePosition * (new Vector2(-Mathf.Sin(headAngle), Mathf.Cos(headAngle))));
			waypoints.RemoveAt(0);
		}

		UpdatePositions();
		UpdateObjects();
	}

	public void checkArenaBound() {
		if (Mathf.Abs(headPosition.x) > arenaBound.x || Mathf.Abs(headPosition.y) > arenaBound.y) {
			ObstacleHit();
		}
	}

	public void FoodEaten() {
		for (int i = 0; i < lengthIncreasePerFood*waypointSegmmentRatio; i++) {
			waypoints.Insert(0, waypoints[0]);
		}
		for (int i = 0; i < lengthIncreasePerFood; i++) {
			segmentPositions.Insert(0, waypoints[0]);
			segmentAngles.Insert(0, 0);
			segments.Add((GameObject) Instantiate(segmentPrefab));
		}
	}

	public void SlowDown() {
		Time.timeScale = 0.5f;
	}

	public void ObstacleHit() {
		gameOver = true;
	}

	float getSegAngle(int index) {
		return -Mathf.Atan2(waypoints[index].y - waypoints[index + 1].y, waypoints[index].x - waypoints[index + 1].x);
	}

	void UpdatePositions() {
		snakeHead.transform.position = new Vector3(headPosition.x, 0, headPosition.y);
		snakeHead.transform.eulerAngles = -headAngle * Mathf.Rad2Deg * Vector3.up;

		float currentSegAngle = getSegAngle(0);
		float nextSegAngle = getSegAngle(0);
		float prevSegAngle = currentSegAngle;
		for (int i = 0; i < segmentPositions.Count; i++) { 
			float relPosition = i * (1.0f * (waypoints.Count-2) / (segmentPositions.Count-1)) + 1 - (nextTrajectoryPointDistance - movedDistance) / trajectoryRecordSpacing;
			int index = ((int) relPosition);

			float remainder = relPosition - index;
			if (index >= waypoints.Count - 1) {
				index -= 1;
				remainder = 1.0f;
			}

			Vector2 pos = Vector2.zero;
			float segAngle = 0;

			currentSegAngle = getSegAngle(index);
			if (index > 0) {
				prevSegAngle = getSegAngle(index - 1);
			} else {
				prevSegAngle = currentSegAngle;
			}

			if (index < waypoints.Count - 2) {
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

			if (index < waypoints.Count - 1) {				
				pos = (1 - remainder)*waypoints[index] + remainder*waypoints[index + 1];

			} else {
				pos = waypoints[index];
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
			marker.transform.position = new Vector3(waypoints[i].x, 0, waypoints[i].y );
		}

		for (int i = 0; i < segmentPositions.Count; i++) { 
			GameObject segment = segments[i];
			segment.transform.position = new Vector3(segmentPositions[i].x, 0, segmentPositions[i].y);
			segment.transform.eulerAngles = Mathf.Rad2Deg * segmentAngles[i] * Vector3.up;
			if (i < segmentPositions.Count - nTorsoOffset) {
				segment.GetComponentInChildren<SnakeBodyCollision>().firstSegment = false;
			} else {
				segment.GetComponentInChildren<SnakeBodyCollision>().firstSegment = true;
			}
		}
	}


}
