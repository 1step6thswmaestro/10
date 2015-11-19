using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RGBPaper : MonoBehaviour {

    private Text rgbText;
 
	// Use this for initialization
	void Awake () {
        rgbText = GetComponentInChildren<Text>();
	}

    public void SetRGBText (string answer)
    {
        if (answer.Length < 3)
            return;

        answer = answer.ToUpper();

        rgbText.text = 
            "<color=" + GetColorString(answer[0]) + ">Red</color>\n"
            + "<color=" + GetColorString(answer[1]) + ">Green</color>\n"
            + "<color=" + GetColorString(answer[2]) + ">Blue</color>";

    }

    private string GetColorString (char order)
    {
        if (order == 'R')
        {
            return "red";
        }
        else if (order == 'G')
        {
            return "green";
        }
        else if (order == 'B')
        {
            return "blue";
        }
        else
        {
            return "black";
        }
    }
}
