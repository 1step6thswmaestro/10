using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class Calendar : MonoBehaviour {

    public Text month;
    public Text days;

	// Use this for initialization
	void Start ()
    {
        // SetMonth(1);
        // SetHighlightOnDay(1);
    }

    private void SetMonth (string m)
    {
        month.text = "<b><color=#bbbbbb>" + m + "</color></b>";
    }

    public void SetMonth (int m)
    {
        if (1 <= m && m <= 12)
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(m);
            SetMonth(monthName);
        }
        else
        {
            SetMonth(1);
        }
    }

    public void SetHighlightOnDay (uint day)
    {
        days.text = "";

        for (int i = 1; i <= 31; i ++)
        {
            if (i == day)
            {
                days.text += "<color=#bb0000>" + i.ToString("D2") + "</color> ";
            }
            else
            {
                days.text += i.ToString("D2") + " ";
            }
        }
    }
}
