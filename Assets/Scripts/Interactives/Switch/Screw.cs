using UnityEngine;
using System.Collections;
using System;

public class Screw : InteractiveObject {

	private AudioSource dropSound;
    private Rigidbody rb;
	
	public override void Start () {
		base.Start();
		dropSound = this.GetComponent<AudioSource> ();
	}
	
	public override void Update () {
		base.Update();
	}
	
	public override void OnInteraction(Tool tool)
	{
		if (tool is ScrewDriver)
		{
			this.Defuse();
			StartCoroutine(RemoveScrew());
		}
	}

	IEnumerator RemoveScrew()
	{
		int i = 0;
		int count = 50;
		while (i < count) 
		{
			i++;
			transform.Rotate(new Vector3(0, 0, 12f));
			transform.Translate(new Vector3(0, 0, -0.02f));
			yield return null;
		}
		rb = this.gameObject.AddComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (!dropSound.isPlaying && rb != null) 
		{
			dropSound.Play ();
		}
	}
}

