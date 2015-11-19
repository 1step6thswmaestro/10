using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class WireMachine : InteractiveObject
{
    public Image display;

    public Wire[] wires;

    private IList<Color> colorList;
    private int colorListIndex = 0;

    private GameObject wireShelf;
    private AudioSource wireUpSound;

	public GameObject badEnding;
	public GameObject chooseeTool;

	// Use this for initialization
	public override void Start () {
        base.Start();

        this.colorList = new List<Color>();

        foreach (Wire wire in wires)
        {
            Color color = wire.wireColor;
            color.a = 1;

            this.colorList.Add(color);
        }

        Util.ShuffleList(this.colorList);

        display.color = Util.ComplementaryColor(colorList[colorListIndex]);

        StartCoroutine(ShowWireMachine());
    }
    
    public void OnWireCut(Color wireColor)
    {

        if (Util.ColorCompareRGB(wireColor, colorList[colorListIndex]))
        {
            // Color Matched
            colorListIndex++;

            if (colorListIndex >= colorList.Count)
            {
                // DONE
                this.Defuse();
				chooseeTool.SendMessage("OpenToolBoxes", true);
            }
            else
            {
                display.color = Util.ComplementaryColor(colorList[colorListIndex]);
            }
        }
        else {
            // Bad Ending
            Debug.Log("Bad Ending");
			badEnding.SetActive(true);
        }
    }

    private IEnumerator ShowWireMachine()
    {
        wireShelf = this.transform.parent.gameObject;
        wireUpSound = this.GetComponent<AudioSource>();

        wireUpSound.Play();

        int i = 0;
        int count = 250;

        while( i < count)
        {
            i++;
            wireShelf.transform.Translate(new Vector3(0, 0.01f, 0));
            yield return null;
        }        
    }

    public override void OnInteraction(Tool tool)
    {
        // do nothing
    }
}
