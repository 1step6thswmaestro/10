using UnityEngine;
using System.Collections;
using System;

public class ScrewPlate : InteractiveObject {
	
	public Screw[] screws;
	public SwitchInner switchInner;

	private AudioSource plateDropSound;
    private Rigidbody rb;
	
	public override void Start () {
		base.Start();
        plateDropSound = this.GetComponent<AudioSource>();
	}
	
	public override void Update () {
		base.Update();
		
		if (screws != null && CurrentState == State.Activated)
		{
			int defusedScrewCount = 0;
			
			foreach (Screw screw in screws)
			{
				if (screw.CurrentState == State.Defused)
				{
					defusedScrewCount++;
				}
			}
			
			if (defusedScrewCount == screws.Length)
			{
				this.Defuse();
				StartCoroutine(RemoveSwitchBody());
			}
		}	
	
        //TODO : 천장 스크류 제거 치트기
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(RemoveSwitchBody());
        }

	}

    public override void SetHighlight(bool highlight)
    {

    }
	
	public override void OnInteraction(Tool tool)
	{
		// do nothing
	}

	IEnumerator RemoveSwitchBody()
	{
		yield return new WaitForSeconds (1.0f);
		int i = 0;
		int count = 50;

		while (i < count) 
		{
			i++;
			transform.Translate(new Vector3(0, 0, 0.001f));
			yield return null;
		}

		if (rb == null) 
		{
			rb = this.gameObject.AddComponent<Rigidbody> ();
		}

        if ( switchInner != null)
        {
			switchInner.MovePlate();
        }

        StartCoroutine(PlaySound(1.0f));
	}

    IEnumerator PlaySound(float time)
    {
        yield return new WaitForSeconds(time);
        if (plateDropSound != null && rb != null)
        {
            plateDropSound.Play();
        }
    }
}
