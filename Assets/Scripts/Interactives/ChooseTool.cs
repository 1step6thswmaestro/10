using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseTool : MonoBehaviour {

    // cabinet에 드라이버, locker에 해머, 책상 서랍에 니퍼(piler)가 있음.
	public GameObject cabinetDriver;
	public GameObject lockerHammer;
    public GameObject drawerPiler;

	public PlayerTools playerTools;
    // Plier 선택시 wire 트리거가 발동되도록 함.
    public GameObject wireTrigger;

    // driver, hammer, piler순으로 관리.
    bool[] tools = new bool[3];
    enum ToolName
    {
		Screwdriver,
        Hammer,
		Plier
    }
	string[] toolType = { "driverOpen", "hammerOpen", "plierOpen" };

	// 보관 상자를 열고 닫는 애니메이션들.
    IList<Animator> toolAnimList = new List<Animator>();

    private AudioSource toolBoxSound;

    public GameObject hint;
    public GameObject ceiling;
    public GameObject c4Bomb;

    private int triggerCheck = 0;

	// Use this for initialization
	void Start () {
		toolAnimList.Add(cabinetDriver.GetComponent<Animator>());
		toolAnimList.Add(lockerHammer.GetComponent<Animator>());
		toolAnimList.Add(drawerPiler.GetComponent<Animator>());

        toolBoxSound = this.GetComponent<AudioSource>();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenToolBoxes(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Cheat();
        }
    }


	void CheckTool(string toolName)
	{
		if (ToolName.Plier.ToString () == toolName) 
		{
			ChooseOneTool (ToolName.Plier);
            wireTrigger.SetActive(true);
		} 
		else if (ToolName.Hammer.ToString () == toolName) 
		{
			ChooseOneTool(ToolName.Hammer);            
		} 
		else if (ToolName.Screwdriver.ToString () == toolName) 
		{
			ChooseOneTool(ToolName.Screwdriver);
		}
        // 도구들을 가리키는 힌트 화살표가 사라지도록 함.
        hint.SendMessage("SolveBranch");        
	}

	// 도구가 선택된 곳은 다시 닫히지 않도록 함.
    void ChooseOneTool(ToolName selectedTool)
    {
		int i = (int)selectedTool;
        tools[i] = true;
		OpenToolBoxes (false);
    } 
	// 각 도구가 담긴 곳을 열었다 닫았다 함. 
    void OpenToolBoxes(bool isOpen)
    {
        // 모든 도구 관련 트리거가 마무리되었다면
        if ( isOpen && tools[0] && tools[1] && tools[2])
        {
            Debug.Log("All Tool Trigger OK");
            ceiling.SendMessage("CeilingDown");
            return;
        }

		int i = 0;
		
		foreach(Animator anim in toolAnimList) 
		{            
			if(tools[i] == false)
			{
				// 도구 상자들이 열려 있을때
				if(isOpen)
				{
					anim.SetBool(toolType[i], true );
				}
				else
				{
					anim.SetBool(toolType[i], false);
				}
			}
			i++;
		}

        if (i > 0)
        {
            toolBoxSound.Play();
        }            
    }

    // TODO 디버그용
    private void Cheat()
    {
        tools[0] = true;
        tools[1] = true;
        tools[2] = true;
        OpenToolBoxes(true);
    }
}
