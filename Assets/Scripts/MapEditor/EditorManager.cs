using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour {

	public GameObject Cube,Sphere;
	Vector3 P1,P2,empty;
	GameObject lastPlace,Collection,sphere,selected;
	bool foundObject;
	RaycastHit hit;
	float nextStage;
	float delay=1.0f;
    private bool isTrack;

	// Use this for initialization
	void Start () {
		Collection = GameObject.Find ("Collection");
		nextStage = Time.time + delay;
        isTrack = false;
    }
	
	// Update is called once per frame
	void Update () {
		/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		print (Input.mousePosition);*/
		Ray ray = new Ray ();
		ray.origin = transform.position;
		ray.direction = transform.forward;
		if (Physics.Raycast (ray, out hit, 100, LayerMask.NameToLayer ("Terrain"))) {
			//Debug.Log (hit.GetType ());
			foundObject = true;
		} else {
			foundObject = false;
		}

//		if (Input.GetTouch(0).phase == TouchPhase.Began) {
//			Press (hit.point);
//		}
//		if (Input.GetMouseButtonDown (1)) {
//			if (foundObject) {
//				Select (hit);
//				DestroySelected ();
//			}
//
//		}
		if (Time.time > nextStage && isTrack) {
			nextStage = Time.time + delay;
			GameObject sphere = Instantiate (Sphere, transform.position, Quaternion.identity);
			sphere.transform.parent = GameObject.Find ("Collection").transform;
		}
			
	}

    public void StartTrack()
    {
        isTrack = true;
    }

    public void StopTrack()
    {
        isTrack = false;
    }

    public void Press(Vector3 point){
		print (point);
		if (P1 == empty) {
			//P1 = transform.position + transform.forward*2;
			P1 = point;
			sphere = GameObject.Instantiate (Sphere, P1, Quaternion.identity);
		} else if (P2 == empty) {
			//P2 = transform.position + transform.forward*2;
			P2 = point;
			P2 = new Vector3 (P2.x,P1.y,P2.z);
			Destroy (sphere.gameObject);
			placeCube (P1,P2);
		} else {
			//P1 = transform.position + transform.forward*2;
			P1 = point;
			sphere = GameObject.Instantiate (Sphere, P1, Quaternion.identity);
			P2 = empty;
		}
	}

	public void placeCube(Vector3 point1,Vector3 point2){
		Vector3 Between = point2 - point1;
		float distance = Between.magnitude;
		GameObject cube = GameObject.Instantiate (Cube,Collection.transform);
		cube.transform.localScale = new Vector3(0.1f,5,distance);
		cube.transform.position = point1 + (Between / 2.0f);
		cube.transform.LookAt(point2);
		cube.transform.position = new Vector3 (cube.transform.position.x,2.5f,cube.transform.position.z);
		cube.GetComponent<CubeInfo> ().Init (point1,point2);
		lastPlace = cube;
	}
	public void IncScale(){
		lastPlace.transform.localScale *= 1.05f;
	}
	public void DecScale(){
		lastPlace.transform.localScale *= 0.95f;
	}
	public void Select(RaycastHit hit){
		if (hit.collider.name == "Cube(Clone)") {
			selected = hit.collider.transform.gameObject;
		} else {
			selected = null;
		}
		//selected = GetComponentInChildren<TriggerChecker> ().trigger;
	}
	public void DestroySelected(){
		Destroy (selected);
	}
	public void Place(){
		Press (new Vector3(transform.position.x,0,transform.position.z));
	}
	public void Delete(){
		if (foundObject) {
			Select (hit);
			DestroySelected ();
		}
	}
}
