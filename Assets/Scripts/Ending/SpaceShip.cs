using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {

    public float speed = 0.5f;

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
        RotateAround();
	}

    void RotateAround()
    {        
        transform.Rotate(Vector3.up * speed);
        transform.Translate(new Vector3(0, 0, speed));
    }
}
