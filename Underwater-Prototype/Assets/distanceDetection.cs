using UnityEngine;
using System.Collections;

public class distanceDetection : MonoBehaviour {
	private Transform other; 
	public float distance = 1f;
	private Color color; 
	public Material MaterialChange; 

	private GameManagement gameManagement;


	void Start () {
		other = GameObject.FindGameObjectWithTag ("DragonHead").transform; 
		gameObject.GetComponent<MeshRenderer> ().material.SetColor("_Color", new Color(238/255f, 28/255f, 36/255f));
		gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0));
		gameManagement = GameObject.Find("GameManager").GetComponent<GameManagement>();
	}
	
	// Update is called once per frame
	void Update () {		
		if (gameManagement.fireBreathing && (gameObject.transform.position - other.transform.position).magnitude < distance) {
			gameObject.GetComponent<MeshRenderer> ().material = MaterialChange; 
		}
	}
}
