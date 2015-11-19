using UnityEngine;
using System.Collections;
using System;

public class DeskTop : InteractiveObject {

    public static string TurnOffTextureName = "Models/PC/Textures/PC_Dif";
    public static string TurnOnTextureName = "Models/PC/Textures/PC_Dif_1";

    private int hitCount = 0;
	private Rigidbody rbSide;

	public GameObject deskTopSide;
    public GameObject monitor;

    public GameObject badEnding;
    public GameObject chooseTool;

	public override void Start () 
	{
		base.Start ();
	}

    // called from 'DeskTopSide'
	public void HitWithHammer()
	{
		Rigidbody rb = this.GetComponent<Rigidbody> ();
		rb.AddTorque (new Vector3 (10f, 10f, 10f));
		hitCount += 1;

		if (hitCount == 3) 
        {
			OpenDeskTop ();
            chooseTool.SendMessage("OpenToolBoxes", true);
		} 
		else if (hitCount == 6) 
		{
			StartBadEnding();
		}
	}

    public void TurnOn()
    {
        if (monitor != null)
        {
            Renderer monitorRenderer = monitor.GetComponent<Renderer>();
            if (monitorRenderer != null)
            {
                Texture turnOnTexture = (Texture)Resources.Load(TurnOnTextureName);

                if (turnOnTexture != null)
                    monitorRenderer.material.mainTexture = turnOnTexture;
                else
                    Debug.Log("Turn on texture is null");
            }
        }
    }

    private void OpenDeskTop()
	{
		rbSide = deskTopSide.AddComponent<Rigidbody> ();
	}

	private void StartBadEnding()
	{
        badEnding.SetActive(true);
	}

	public override void OnInteraction(Tool tool)
	{
        // do nothing
	}
}
