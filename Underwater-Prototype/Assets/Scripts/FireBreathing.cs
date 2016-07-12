using UnityEngine;
using System.Collections;

public class FireBreathing : MonoBehaviour {

	public GameObject fireElement;
	public float strength = 1;
	public float amountPerSecond = 20;
	public Vector3 initRelativeOffset = new Vector3(0.0f, 0.2f, 0.5f);
	public float initPositionSpread = 0.2f;
	public float angluarSpread = 0.5f;
	public GameManagement gameManagement;

	public AudioSource audiosource { get { return GetComponent<AudioSource> (); } }
	public AudioClip breathesound;

	// Use this for initialization
	void Start () 
	{
		gameObject.AddComponent<AudioSource> ();
		audiosource.clip = breathesound;
		audiosource.playOnAwake = false;

	}

	// Update is called once per frame
	void Update () {
		if (gameManagement.fireBreating && Input.GetKey(KeyCode.Space)) {
			BreatheFire();
			PlayBreatheSound ();
		}
	}

	public void BreatheFire() {
		float createProbability = Time.smoothDeltaTime * amountPerSecond;
		if (Random.Range(0, 1) < createProbability) {
			var elem = (GameObject) Instantiate(
				fireElement, 
				transform.position + (initRelativeOffset.z*transform.forward + initRelativeOffset.y*transform.up + initRelativeOffset.x*transform.right) 
				+ Random.Range(-initPositionSpread, initPositionSpread)*transform.right,
				transform.rotation);
			elem.GetComponent<Rigidbody>().velocity = strength * transform.forward + 0.3f*strength * transform.up + Random.Range(-angluarSpread, angluarSpread)*strength * transform.right;
		}
	}

	void PlayBreatheSound (){
		audiosource.Play ();
	}
}
