using UnityEngine;
using System.Collections;

public class C4LedManager : MonoBehaviour {

    public GameObject C4LedPrefab;

    private static float InitialXPos = 0.1380f;
    private static float DeltaXPos = -0.0419f;

    private static float InitialZPos = 0.0043f;

    private static int NumberOfLED = 6;

    GameObject[] leds;

    public void CreateLEDs(string[] ledColor)
    {
        leds = new GameObject[NumberOfLED];

        for (int i = 0; i < NumberOfLED; i++)
        {
            leds[i] = Instantiate(C4LedPrefab);
            leds[i].transform.parent = this.gameObject.transform;

            leds[i].transform.localPosition = new Vector3(InitialXPos + i * DeltaXPos, 0, InitialZPos);
            leds[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
            leds[i].transform.localScale = new Vector3(1, 1, 1);

            Color color = Color.black;

            if (ledColor.Length > i)
                color = ConvertStringToColor(ledColor[i]);

            leds[i].GetComponent<C4Led>().Color = color;
        }
    }

    private Color ConvertStringToColor(string name)
    {
        switch (name.ToLower())
        {
            case "white":
                return Color.white;
            case "black":
                return Color.black;
            case "yellow":
                return Color.yellow;
            case "blue":
                return Color.blue;
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "purple":
                return new Color(180, 0, 255);
        }

        return Color.clear;
    }

    public void TurnOffNthLED(int n)
    {
        if (0 <= n && n < NumberOfLED)
        {
            leds[n].GetComponent<C4Led>().Color = Color.black;
        }
    }
}
