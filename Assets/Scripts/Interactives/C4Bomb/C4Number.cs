public class C4Number : InteractiveObject
{
    private C4Bomb  parent;
    public char     number;
    
    public override void Start () {
        base.Start();
        parent = GetComponentInParent<C4Bomb>();
    }
	
	public override void Update ()
    {
        base.Update();
    }

    public override void OnInteraction(Tool tool)
    {
        parent.OnNumberPressed(this);
    }

    public override void SetHighlight(bool highlight)
    {
        base.SetHighlight(highlight);
    }

}
