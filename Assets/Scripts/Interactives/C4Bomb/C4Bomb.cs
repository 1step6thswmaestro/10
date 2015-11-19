using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class C4Bomb : InteractiveObject {

    enum C4State {
        Wire,
        Numpad
    }
    
    public Text displayText;

    public float expTime = 45.0f;

    private string numpadAnswer;
    public string[] wireAnswer;
    public bool shuffleWireAnswer;

    public Calendar calendar;
    
    private string numpadInput;
    private ArrayList wireInput;

    private C4State c4State;
    private C4LedManager c4LedManager;

	public GameObject escapeEnding;
	public GameObject boomExplosionEnding;

    public override void Start ()
    {
        base.Start();

        if (shuffleWireAnswer)
        {
            Util.ShuffleList(wireAnswer);
        }

        numpadInput = "";
        wireInput = new ArrayList(wireAnswer.Length);

        c4LedManager = GetComponentInChildren<C4LedManager>();

        if (c4LedManager != null)
        {
            c4LedManager.CreateLEDs(wireAnswer);
        }

        if (calendar != null)
        {
            int month = UnityEngine.Random.Range(1, 12);
            uint day = (uint)UnityEngine.Random.Range(1, 31);
            calendar.SetMonth(month);
            calendar.SetHighlightOnDay(day);

            numpadAnswer = "" + month.ToString("D2") + day.ToString("D2");
        }

        Debug.Log(numpadAnswer);
        StateTransition(C4State.Wire);

        if (boomExplosionEnding)
        {
            boomExplosionEnding.SetActive(true);
        }
    }

	public override void Update ()
    {
        base.Update();

        expTime -= Time.deltaTime;
    }

    public override void OnInteraction(Tool tool)
    {
        // do nothing
    }

    public void OnNumberPressed(C4Number number)
    {
        // Debug.Log(number.number);

        if (c4State == C4State.Numpad)
        {
            numpadInput += number.number;
            displayText.text = (numpadInput + " ");

            // it's time to check the answer
            if (numpadInput.Length >= numpadAnswer.Length)
            {
                if (numpadInput.CompareTo(numpadAnswer) == 0)
                {
                    // fine
                    base.Defuse();
                    this.Defused();
                }
                else
                {
                    // not good
                    Explode();
                }
            }
        }
        else
        {
            Explode();
        }
    }

     public void OnWireCut(C4Wire wire)
    {
        // Debug.Log(wire.color);

        if (c4State == C4State.Wire)
        {
            if (wire.color.CompareTo(wireAnswer[wireInput.Count]) != 0)
            {
                Explode();
                return;
            }
            else
            {
                wireInput.Add(wire.color);
                displayText.text = "";
                c4LedManager.TurnOffNthLED(wireInput.Count - 1);

                foreach (string wireColor in wireInput)
                {
                    displayText.text += wireColor[0];
                }
            }

            // judge answer
            if (wireInput.Count >= wireAnswer.Length)
            {
                int index = 0;
                foreach (string wireColor in wireInput)
                {
                    if (wireColor.CompareTo(wireAnswer[index]) != 0)
                    {
                        // not good
                        Explode();
                        return;
                    }
                    index++;
                }

                // fine
                StateTransition(C4State.Numpad);
            }
        }
        else
        {
            Explode();
        }
    }

    private void StateTransition(C4State state)
    {
        switch (state)
        {
            case C4State.Wire:
                displayText.text = "cut wires";
                break;
            case C4State.Numpad:
                displayText.text = "? ? ? ?";
                break;
        }

        this.c4State = state;
    }
        
    private void Explode()
    {
        Debug.Log("Bomb exploded");
		boomExplosionEnding.SendMessage ("Explosion");
    }

    private void Defused()
    {
        Debug.Log("Bomb defused");
        escapeEnding.SetActive(true);
    }
}
