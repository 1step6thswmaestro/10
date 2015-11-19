using UnityEngine;
using System.Collections;

public class Hammer : Tool
{
    private AudioSource hammerSound;

    void Awake()
    {
        hammerSound = this.GetComponent<AudioSource>();
    }

    public override void Use()
    {
        if( hammerSound != null)
        {
            hammerSound.Play();            
        }       

		SwingHammer ();
    }

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.X)) 
		{
			SwingHammer();
		}
	}

	private void SwingHammer()
	{
		StartCoroutine (RotateHammer(10, 9f));
		StartCoroutine (RotateHammer(10, -18f));
		StartCoroutine (RotateHammer(30, 3f));
	}

	private IEnumerator RotateHammer(int count, float rotationAmount)
	{
		int i = 0;
		while (i < count) 
		{
			i++;
			transform.Rotate(new Vector3(0, rotationAmount, 0) );
			yield return null;
		}
		yield return new WaitForSeconds(0.1f);
	}
}
