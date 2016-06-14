using UnityEngine;
using System.Collections;

public class eggMove : MonoBehaviour {
    public float turnSpeed = 10f;
    public float pushForce = 100f;
    public float backwardAcc = 1;

    private float angularVel = 0;
    private float angle = 0; 
    public GameObject eggLight; 
	public ParticleSystem explosion;
	public GameObject egg;
    private float lightMinInten = 0.5f;     
	 
	private bool exploded = false; 

    // Use this for initialization
    void Start () {
		egg.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {       
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            
            angularVel = pushForce;
            eggLight.GetComponent<Light>().intensity += 0.5f; 
            //eggLight.enabled = !eggLight.enabled;
           

           // transform.Rotate(Vector3.forward,turnSpeed*Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            angularVel = -pushForce;
            eggLight.GetComponent<Light>().intensity += 0.5f;
           // transform.Rotate(Vector3.forward,turnSpeed*Time.deltaTime);
        }
        
		if(eggLight.GetComponent<Light>().intensity >= 8f && !exploded ){
			
			GetComponentInChildren<MeshRenderer>().enabled = false;
			eggLight.SetActive (false);
			explosion.Play ();
			exploded = true; 
        }

        angle += Time.smoothDeltaTime * angularVel;
        angularVel -= Time.smoothDeltaTime * backwardAcc * angle;
        angularVel *= 0.92f;
        transform.eulerAngles = angle * Vector3.forward; 

	}
}
