using UnityEngine;
using System.Collections;

public class SafeMain : InteractiveObject {

    private AudioSource dropSound;
    private bool fallCheck = false;
    private Rigidbody safeRigidBody;

    void Awake()
    {
        dropSound = this.GetComponent<AudioSource>();
        safeRigidBody = this.GetComponentInParent<Rigidbody>();
    }

	// Use this for initialization
	public override void Start () 
    {
        base.Start();
	}

    public override void Update()
    {
        base.Update();
    }
		
    void PlayDropSound()
    {
        if (!dropSound.isPlaying)
        {
            dropSound.Play();
        }
    }
	

    void OnCollisionEnter(Collision collision)
    {
        if (!dropSound.isPlaying && fallCheck == false)
        {
            dropSound.Play();
            fallCheck = true;
        }
    }

	public override void SetHighlight(bool highlight)
	{

	}

    public override void OnInteraction(Tool tool)
    {
        if ( tool is Hammer)
        {
            tool.Use();
            Debug.Log("Safe Hammer hit");
            safeRigidBody.AddExplosionForce(1000f, transform.position, 10f);
        }
    }
}
