using UnityEngine;
using System.Collections;

public class Button : InteractiveObject
{
    public GameObject drawer;
    public GameObject badEnding;
    
    public GameObject buttonBroken;
    public GameObject PC;

    private Animator animatorDrawer;
    private Animator animatorButton;
    
    int pressedNumber = 0;
    private AudioSource buttonPushSound;
    private AudioSource drawerOpenSound;

	public GameObject hint;

    public override void Start()
    {
        base.Start();
        animatorDrawer = drawer.GetComponent<Animator>();
        animatorButton = this.GetComponent<Animator>();

        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.priority == 127)
            {
                buttonPushSound = source;
            }
            else
            {
                drawerOpenSound = source;
            }
        }
    }
   
    public override void OnInteraction(Tool tool)
    {
        // 버튼을 1번 클릭하고 1.5초간 기다리면 성공! 버튼을 3번 클릭하면 배드엔딩으로~~        
        if (tool is Hand)
        {                   
            pressedNumber += 1;
            ButtonPressed();
            StartCoroutine(StartTrigger());           


            if (pressedNumber == 3)
            {
                StartBadEnding();
            }            
        }
        else if( tool is Hammer)
        {
            Debug.Log("Hammer ok");
            StartCoroutine(DropPC());
            this.gameObject.SetActive(false);
            buttonBroken.SetActive(true);            
        }
        tool.Use();
    }    


    IEnumerator StartTrigger()
    {        
        yield return new WaitForSeconds(0.5f);
        if (pressedNumber == 1)
        {
            OpenDrawer(); 
			hint.SendMessage("Solve");
        }
    }
	
    private void ButtonPressed()
    {
        animatorButton.SetTrigger("Press");
        buttonPushSound.Play();
    }

    private void OpenDrawer()
    {        
        animatorDrawer.SetBool("drawerMoved", true);
        drawerOpenSound.Play();
    }

    private void StartBadEnding()
    {        
        badEnding.SetActive(true);
    } 
    // 해머로 버튼을 치면은 PC가 추락함.
    IEnumerator DropPC()
    {
        PC.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Rigidbody rb = PC.GetComponent<Rigidbody>();
        rb.mass = 1;
    }

}
