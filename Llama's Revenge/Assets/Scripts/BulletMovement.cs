using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class BulletMovement : MonoBehaviour {

	public Vector2 speed = new Vector2(100, 100);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;
	private string nextScene;

	void Update () {
	
		if (GetComponent<DealDamage> ().IsInvoking ("Death"))
			speed = new Vector2 (25, 25);

		if (GetComponent<AnimalBullet> () != null)
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);

		if(GetComponent<AnimalBullet> ().fighting || GetComponent<AnimalBullet>().completed)
			movement = new Vector2 (0,0);
	}

	void FixedUpdate(){
		
		GetComponent<Rigidbody2D>().velocity = movement;

        if (gameObject.transform.position.x > 1000 || gameObject.transform.position.x < -1000)
        {
                Destroy(gameObject);
        }
	}
}
