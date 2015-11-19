using UnityEngine;
using System.Collections;
using System;

public class NumPadButton : InteractiveObject {

    private NumPad   numpad;
    public  char     number;

    public override void Start ()
    {
        base.Start();
        numpad = this.transform.parent.GetComponent<NumPad>();
    }

    public override void OnInteraction (Tool tool)
    {
        if (tool is Hand)
        {
            numpad.OnNumPadButtonPressed(number);
        }
    }
}
