using UnityEngine;
using System.Collections;

public class Hint : MonoBehaviour {

	private Transform target;
	public GameObject arrow;

	private float timeCheck = 0f;
	private float hintTime = 20.0f;
	private bool isBegin = false;
	private bool isSolve = false;

	private HintFlow hintFlow;

    private ArrayList arrowList;

	// Use this for initialization
	void Start () {
        hintFlow = this.GetComponent<HintFlow>();
        hintFlow.SendMessage("NextHint");		
	}	

	// 몇초 뒤에 힌트를 보여줌
	public void StartShow(Transform targetTrans)
	{        
		this.target = targetTrans;
		this.transform.position = this.target.position + new Vector3 (0f, 2f, 0f); 
        this.arrow = Instantiate (arrow, this.transform.position, this.transform.rotation) as GameObject;
        arrow.SetActive(false);

		StartCoroutine (ShowArrow (hintTime));
	}

    public void StartShow(Transform[] targetTrans, int count)
    {
        arrowList = new ArrayList();

        for (int i = 0; i < count; i++ )
        {
            this.target = targetTrans[i];
            this.transform.position = this.target.position + new Vector3(0f, 2f, 0f);
            this.arrow = Instantiate(arrow, this.transform.position, this.transform.rotation) as GameObject;
            arrow.SetActive(false);
            arrowList.Add(arrow);            
        }

        StartCoroutine(ShowArrows(hintTime));
    }

	// 퍼즐을 풀었을 때
	public void Solve()
	{
		this.timeCheck = 60.0f;
		isBegin = false;
		Destroy (this.arrow);
		hintFlow.SendMessage ("NextHint");
	}
        
    public void SolveBranch()
    {
        this.timeCheck = 60.0f;
        isBegin = false;
        if ( arrowList != null)
        {
            foreach (GameObject arr in arrowList)
            {

                Destroy(arr);
            }
        }        
    } 

	IEnumerator ShowArrow(float time)
	{
		yield return new WaitForSeconds (time);
		arrow.SetActive (true);        
	}

    IEnumerator ShowArrows(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (GameObject arr in arrowList)
        {
            arr.SetActive(true);
        }
    }
}
