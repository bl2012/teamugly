using UnityEngine;
using System.Collections;

public class SwitchSoldier : MonoBehaviour {

	public GameObject[] SoldierTypes;
	public int index;

	void Start () {
		//index = 0;
    }

    public IEnumerator SwitchLeft()
	{
		//Debug.Log ("Switching left");
		index = (index - 1 < 0) ? SoldierTypes.Length - 1 : index-1;
		yield return new WaitForSeconds(1f);

	}

	public IEnumerator SwitchRight()
	{
		//Debug.Log ("Switching right");
		index = (index + 1 > SoldierTypes.Length - 1) ? 0 : index+1;
		yield return new WaitForSeconds(1f);

	}
}
