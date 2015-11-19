using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {

    private AudioSource dropSound;
    private AudioSource desktopShakingSound;

	// Use this for initialization
	void Start () {
        dropSound = this.GetComponent<AudioSource>();
	}	

    void OnCollisionEnter(Collision collider)
    {
        dropSound.Play();
		Debug.Log (gameObject.name);
    }
}
