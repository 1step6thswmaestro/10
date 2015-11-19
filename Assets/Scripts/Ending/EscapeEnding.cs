using UnityEngine;
using System.Collections;

public class EscapeEnding : MonoBehaviour {

    public GameObject door;

	// Use this for initialization
	void Start () {
        StartCoroutine(OpenDoor());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator OpenDoor()
    {
        int i = 0;
        int count = 90;
        while( i < count)
        {
            i++;
            door.transform.Rotate(new Vector3(0, 1f, 0));
            yield return null;
        }
        door.GetComponent<AudioSource>().Play();
    }
}
