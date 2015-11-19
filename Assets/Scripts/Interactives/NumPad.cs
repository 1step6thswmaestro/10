using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class NumPad : InteractiveObject {

    private IList<char>         combination;
    public string               answer;

    public Text                 display;

	// Use this for initialization
	public override void Start () {
        base.Start();
        combination = new List<char>();
	}

    public void OnNumPadButtonPressed (char button)
    {
        combination.Add(button);

        string combinationString = CombinationToString();

        if (display)
        {
            display.text = combinationString;
        }

        if (combinationString.CompareTo(answer) == 0)
        {
            this.Defuse();
        }

        // PublishEvent(combinationString);
    }

    private string CombinationToString ()
    {
        string combinationString = "";
        foreach (char c in combination)
        {
            combinationString += c;
        }

        return combinationString;
    }

    private void PublishEvent (string combination)
    {
        CombinationChangedEvent e = new CombinationChangedEvent();
        e.combination = combination;
        EventBus.Instance.Post(e);
    }

    public override void OnInteraction (Tool tool)
    {
        // do nothing
    }


    public class CombinationChangedEvent
    {
        public string combination;
    }
}
