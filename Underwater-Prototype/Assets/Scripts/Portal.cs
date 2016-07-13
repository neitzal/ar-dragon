using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public GameObject smallPortalBar;
	public GameObject largePortalBar;
	public GameObject portalExplosion;

	public int nBars = 8;
	public float radius = 1.0f;


	private Animator animator;
	private ParticleSystem ps;
	private GameObject snakeHead;

	void Awake() {
		animator = GetComponent<Animator>();
		ps = GetComponent<ParticleSystem>();
	}

	void Start() {

		for (int i = 0; i < nBars; i++) {
			float rotation = i * (360f / nBars);
			var bar = (GameObject)Instantiate(largePortalBar, transform.position, Quaternion.Euler(rotation*Vector3.up));
			bar.transform.SetParent(transform);
			bar.GetComponentInChildren<ParticleSystem>().transform.localPosition = radius * Vector3.right;


			var smallBar = (GameObject)Instantiate(smallPortalBar, transform.position, Quaternion.Euler((22.5f + rotation)*Vector3.up));
			smallBar.transform.SetParent(transform);
			smallBar.GetComponentInChildren<ParticleSystem>().transform.localPosition = radius * Vector3.right;


		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("DragonHead")) {
			animator.SetTrigger("EnterPortal");
			snakeHead = other.gameObject;
			snakeHead.GetComponent<HeadMovement>().OnEnterPortal();
			GameObject.Find("LevelWonOverlay").GetComponent<Animator>().SetTrigger("LevelWon");

		}
	}

	public void Explode() {
		var explosion = (GameObject) Instantiate(portalExplosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
		GetComponent<ChangeScene> ().ChangeToScene ("watertoland");
	}
}
