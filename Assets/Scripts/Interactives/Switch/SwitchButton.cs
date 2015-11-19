using UnityEngine;
using System.Collections;

public class SwitchButton : InteractiveObject
{

    private SwitchInner switchInner;
	private AudioSource successSound;

    public ChooseTool chooseTool;
	public GameObject leverEnding;

    void Awake()
    {
        switchInner = this.GetComponentInParent<SwitchInner>();
		successSound = this.GetComponent<AudioSource> ();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnInteraction(Tool tool)
    {
        if( tool is Hand)
        {
            StartCoroutine(MoveButton());
            
            Debug.Log(switchInner.CheckLeverState());

            bool solved = switchInner.CheckLeverState();

            if (solved)
            {
                chooseTool.SendMessage("OpenToolBoxes", true);  
				successSound.Play ();
            }
			else
			{
				leverEnding.SendMessage("ShowParticles");
			}
            //chooseTool.SendMessage("Cheat");
        }
    }

    private IEnumerator MoveButton()
    {
        int i = 0;
        int count = 50;
        float moveAmount = 0.0025f;
        while ( i < count)
        {
            i++;
            if ( i > count/2)
            {
                transform.Translate(0, moveAmount, 0);
            }
            else
            {
                transform.Translate(0, -moveAmount, 0);
            }
            yield return null;
        }
    }
}
