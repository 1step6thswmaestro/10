using UnityEngine;
using System.Collections;
using System;

public class C4Wire : InteractiveObject {

    private C4Bomb  parent;
    public string   color;
    
    public override void Start ()
    {
        base.Start();
        parent = GetComponentInParent<C4Bomb>();
	}
	
	public override void Update ()
    {
        base.Update();
    }

    public override void OnInteraction(Tool tool)
    {
        parent.OnWireCut(this);
        this.gameObject.SetActive(false);
    }
}
