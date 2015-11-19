using UnityEngine;
using System.Collections;

public class HintFlow : MonoBehaviour {

	public GameObject[] triggers;

	private Hint hint;
	private int hintNumber = 0;

    void Awake()
    {
        hint = this.GetComponent<Hint>();
    }

	public void NextHint()
	{
		// 3개의 도구를 선택하는 분기
		if (hintNumber == 3) 
		{
			GameBranch(hintNumber);
		}	

		if ( hintNumber < 3) 
		{
            hint.SendMessage("StartShow", this.triggers[hintNumber].transform);
            hintNumber += 1;
		}      
	}

	void GameBranch(int hintNumber)
	{
        Transform[] trans = { triggers[hintNumber].transform, triggers[++hintNumber].transform, triggers[++hintNumber].transform };
        hint.StartShow(trans, trans.Length);
		Debug.Log("Hint number :  " + hintNumber  + ", trans.Length : " + trans.Length);
	}
}
