﻿using UnityEngine;
using System.Collections;
using System;

public class AndGate : InteractiveObject
{
    public InteractiveObject[] inputs;

    public override void OnInteraction(Tool tool) { }

    public override void Update ()
    {
        base.Update();

        if (CheckAllInputDefused())
        {
            Defuse();
        }
    }

    private bool CheckAllInputDefused ()
    {
        foreach (InteractiveObject interObj in inputs)
        {
            if (interObj.CurrentState != State.Defused)
            {
                return false;
            }
        }
        return true;
    }

    public override void Activate()
    {
        base.Activate();

        foreach (InteractiveObject intObj in inputs)
        {
            intObj.Activate();
        }
    }
}