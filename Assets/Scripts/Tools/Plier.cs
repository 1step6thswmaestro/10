using UnityEngine;
using System.Collections;

public class Plier : Tool
{
	public Transform plierBody;
	public Transform plierOtherBody;

    private AudioSource cutSound;

    public override void Start()
    {
        base.Start();
        cutSound = this.GetComponent<AudioSource>();
    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.X))
		{
			CutAction();
		}
	}

    public override void Use()
    {
        CutAction();
    }

	private void CutAction()
	{
		if (plierBody != null) 
		{
			StartCoroutine(RotatePlier (plierBody, 15, 1f));
		}

        if (cutSound != null)
        {
            cutSound.Play();
        }

		if (plierOtherBody != null) 
		{
			StartCoroutine(RotatePlier (plierOtherBody, 15, -1f));
		}        
	}

	IEnumerator RotatePlier(Transform target, int count, float rotationAmount)
	{
		int i = 0;
		while (i < count * 2) 
		{
			i++;
			if( i > count )
			{
				target.Rotate(new Vector3(0, 0, -rotationAmount));
			}
			else
			{
				target.Rotate(new Vector3(0, 0, rotationAmount));
			}
			yield return null;
		}        
	}
}
