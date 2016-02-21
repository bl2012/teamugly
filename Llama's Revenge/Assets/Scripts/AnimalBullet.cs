using UnityEngine;
using System.Collections;

public class AnimalBullet : MonoBehaviour {

	public int damage = 1;
	public bool ifEnemyHit = false;

	void Start () {

		//Destroy(gameObject, 6);
	}

	void Update () {
        if (ifEnemyHit == true)           
            Destroy(gameObject);
	}
}
