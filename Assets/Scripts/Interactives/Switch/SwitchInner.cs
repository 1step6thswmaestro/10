using UnityEngine;
using System.Collections;

public class SwitchInner : MonoBehaviour {

	public GameObject[] Lever;
	public GameObject button;
	public GameObject leverEnding;

    // 레버 1 : up, down 
    // 레버 2 : up, down
    // 버튼 누르기

    private bool[] leverAnswer;
    private bool[] leverState;

	void Awake()
	{
        if ( Lever != null)
        {
            leverAnswer = new bool[Lever.Length];
            leverState = new bool[Lever.Length];
        }

        if (leverEnding)
        {
		    leverEnding.SetActive (true);
        }
		
		MakeLeverAnswer();
	}

    void Start()
    {
        
        
    }

    private void MakeLeverAnswer()
    {
        for (int i = 0; i < leverAnswer.Length; i++)
        {
            leverAnswer[i] = passFail(0.5f);
            Debug.Log("leverAnswer : " + (i + 1) +" :" + leverAnswer[i]);
        }
    }

    private bool passFail(float fChanceOfSuccess)
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        float fRand = Random.Range(0.0f, 1.0f);        
        if (fRand <= fChanceOfSuccess)
            return true;
        else
            return false;
    }

    public bool CheckLeverState()
    {
        bool isCorrect = true;
        for (int i = 0; i < leverAnswer.Length; i++)
        {
            if ( leverAnswer[i] != leverState[i] )
            {
                isCorrect = false;
            }
        }
        return isCorrect;
    }

    public void SetLeverState(string leverName, bool leverUp)
    {
        if ( leverName == "Lever1")
        {
            leverState[0] = leverUp;
        }
        else if ( leverName == "Lever2" )
        {
            leverState[1] = leverUp;
        }

        Debug.Log(leverName + ":" + leverUp);
        //Debug.Log(CheckLeverState());
    }

	public void MovePlate()
	{
		StartCoroutine (MovePlateForward());
	}

	private IEnumerator MovePlateForward()
	{
		yield return new WaitForSeconds (0.5f);
		int i = 0;
		int count = 50;
		while (i < count) 
		{
			i++;
			this.transform.Translate(new Vector3(0, 0.01f, 0));
			yield return null;
		}
	}
}
