using UnityEngine;
using System.Collections;

public class Lever : InteractiveObject {

	private bool leverUp = false;
    private SwitchInner switchInner;

    void Awake()
    {
        switchInner = this.GetComponentInParent<SwitchInner>();
    }

	// Use this for initialization
	public override void Start () {
        base.Start();
        if( switchInner != null)
        {
		    switchInner.SetLeverState(this.gameObject.name, leverUp);   
        }
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}

    public override void OnInteraction(Tool tool)
    {
        if ( tool is Hand)
        {
            this.Defuse();
			StartCoroutine(MoveLever());
            switchInner.SetLeverState(this.gameObject.name, leverUp);
        }
    }

	public override void SetHighlight(bool highlight)
	{
		Renderer renderer = this.GetComponent<Renderer> ();
		Material[] materails = renderer.materials;

		if (materails != null) 
		{
			foreach ( Material material in materails )
			{
				material.shader = this.FindHighlightOrStdShader(highlight);
			}
		}
	}

	private IEnumerator MoveLever()
	{
		int i = 0;
		int count = 60;
		float rotateAmount;

		if (leverUp) {
			rotateAmount = 1.5f;
		}
		else 
		{
			rotateAmount = -1.5f;
		}

		while ( i < count)
		{
			i++;
			transform.Rotate(new Vector3(rotateAmount, 0, 0 )); 
			yield return null;
		}

		leverUp = !leverUp;
	}

}
