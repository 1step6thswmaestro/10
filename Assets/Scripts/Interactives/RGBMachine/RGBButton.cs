using UnityEngine;
using System.Collections;

public class RGBButton : InteractiveObject {

    private RGBMachine  rgbMachine;
    public char         rgbValue;

    // Use this for initialization
    public override void Start () {
        base.Start();
        rgbMachine = this.transform.parent.GetComponent<RGBMachine>();
    }

    public override void OnInteraction(Tool tool)
    {
        if (tool is Hand)
        {
            rgbMachine.OnRGBButtonPressed(rgbValue);
        }
    }
}
