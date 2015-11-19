using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RGBMachine : InteractiveObject {

    public RGBPaper paper;

    private IList<char> combination;
    private string       answer;

    public Text         display;

	private int trial = 0;

	public ChooseTool chooseTools;
    public GameObject badEnding;
        
    private AudioSource rgbButtonSound;
    private AudioSource rgbFailSound;

	public GameObject hint;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        combination = new List<char>();
        answer = GenerateAnswer();

        paper.SetRGBText(answer);

        SoundSourceInit();
    }

    private string GenerateAnswer ()
    {
        List<char> list = new List<char>();
        list.Add('R');
        list.Add('B');
        list.Add('G');

        Util.ShuffleList(list);

        string ans = "";
        foreach (char c in list)
        {
            ans += c;
        }

        return ans;
    }

    public void OnRGBButtonPressed (char button)
    {
        combination.Add(button);
        if( rgbButtonSound != null)
        {
            rgbButtonSound.Play();
        }        

        string combinationString = this.CombinationToString();

        if (display)
        {
            display.text = combinationString;
        }
        // 정답을 맞추었을때, 정답을 맞추지 못했을 때로 나눔.
		// 맞추면 3가지 도구를 고를 수 있게 됨.
        if (combinationString.CompareTo (answer) == 0) 
        {						
			chooseTools.SendMessage("OpenToolBoxes", true);
			hint.SendMessage("Solve");
		} 
		else if (combinationString.Length > 2) 
		{          
            StartCoroutine(FailOnce());
		}

		if (trial == 2) 
		{			
            badEnding.SetActive(true);
		}
    }

    private string CombinationToString()
    {
        string combinationString = "";
        foreach (char c in combination)
        {
            combinationString += c;
        }

        return combinationString;
    }

    public override void OnInteraction(Tool tool)
    {
        // do nothing
    }

    private void SoundSourceInit()
    {
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.priority == 127)
            {
                rgbFailSound = source;
            }
            else
            {
                rgbButtonSound = source;
            }
        }
    }

    IEnumerator FailOnce()
    {
        combination.Clear();
        rgbFailSound.Play();
        trial += 1;
        yield return new WaitForSeconds(0.5f);
        display.text = "";
    }

}
