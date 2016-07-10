using UnityEngine;
using System.Collections;

public class FoodBehavior : MonoBehaviour {
		
	public ParticleSystem particle;
	Animator animator;

	private AudioSource audiosource { get { return GetComponent<AudioSource> (); } }
	public AudioClip foodsound;

	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent<AudioSource> ();
		audiosource.clip = foodsound;
		audiosource.playOnAwake = false;

	}

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody == null) {
			Debug.Log("Something collided with food, without having a rigid body component!");
		} else if (collision.rigidbody.CompareTag("DragonHead")) {

			collision.rigidbody.GetComponent<FoodEatWave>().InitiateWave();

			// TODO: if body is near level boundary segment gets created outside of level

			// TODO: sometimes segment animation get broken and wiggling looks not very smooth
			collision.rigidbody.GetComponent<HeadMovement>().CreateSegments(5);

			animator.SetTrigger("Disappear");
			particle.Play();
		}
	}
		

	void Update() {
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Disappeared")) {
			Debug.Log("disappeared");
			animator.SetTrigger("Appear");
			Reposition ();
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.CompareTag("Obstacle")) {
			Debug.Log("food collision with obstacle, new position");
			Reposition ();
		}

		if (collision.rigidbody != null) {
			if (collision.gameObject.CompareTag ("DragonHead")) {
				if (!audiosource.isPlaying) {
					audiosource.Play ();
				}
			}
		}

	}

	void Reposition() {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		//TODO: create vector from level boundaries

		//TODO: make indicator for next food position (maybe like a compass needle)

		transform.position = new Vector3(
			Random.Range(-5f, 5f), 
			transform.position.y, 
			Random.Range(-5f, 5f));
	}
}
