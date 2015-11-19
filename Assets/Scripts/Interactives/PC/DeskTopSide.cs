using UnityEngine;
using System.Collections;

public class DeskTopSide : InteractiveObject {

	private DeskTop deskTop;

	public override void Start () 
	{
		base.Start ();
		deskTop = this.transform.parent.GetComponent<DeskTop> ();
	}
	
	public override void OnInteraction(Tool tool)
	{
		if (tool is Hammer) 
		{
			tool.Use();
			deskTop.HitWithHammer();
		}
        else if (tool is Hand)
        {
            tool.Use();
            deskTop.TurnOn();
        }
	}	
}
