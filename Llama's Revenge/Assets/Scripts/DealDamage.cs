using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

	//public int health = 5;
	//public int damage = 1;
	//public float rateOfAttack = 2;
	private string team = "";
	private string color = "";
	public bool enemy = true;
	private Animator animator;

	private AudioSource source;//-----------------------------------Sound

	void Start()
	{
		if (gameObject.GetComponent<AnimalBullet> () == null) {
			//Debug.Log (gameObject.name + " doesn't have a bullet script");
			return;
		}
		team = gameObject.GetComponent<AnimalBullet>().team;
		color = gameObject.GetComponent<AnimalBullet> ().color;
	}

	public void OnTriggerEnter2D(Collider2D otherCollider){

        GameObject bullet = otherCollider.gameObject;
        AnimalBullet bulletScript = bullet.GetComponent<AnimalBullet>();
		AnimalBullet animalBullet = GetComponent<AnimalBullet>();

		if(bulletScript != null){
            if (!bulletScript.launched)
                return;
            if (GetComponent<AnimalBullet>() != null && !animalBullet.launched)
            {
                //Debug.Log("Killed at the spawner");
                GetComponentInParent<ShootAnimal>().defeated = true;
                StartCoroutine(GetComponent<AnimalBullet>().Death()); // this animal dies
                return;
            }

            if (team == bulletScript.team) return; // teammates can't hurt each other

			if(color == bulletScript.color)		// If colors are the same
			{
				StartCoroutine(GetComponent<AnimalBullet>().Death()); // this animal dies
				StartCoroutine(bulletScript.Death());				  // enemy also dies
				return;
			}

			if(color == "gray")
			{
				switch(bulletScript.color)
				{
					case "orange":
						//Debug.Log("orange beats gray");
						StartCoroutine(GetComponent<AnimalBullet>().Death()); // this animal dies
						break;
					case "brown":
						//Debug.Log("gray beats brown");
						StartCoroutine(bulletScript.Death());				  // enemy dies
						break;
					default:
						//Debug.Log("Error: enemy is " + animalBullet.color);
						break;
				}

				return;
			}

			if(color == "orange")
			{
				switch(bulletScript.color)
				{
				case "gray":
					//Debug.Log("orange beats gray");
					StartCoroutine(bulletScript.Death());				  // enemy dies
					break;
				case "brown":
					//Debug.Log("brown beats orange");
					StartCoroutine(GetComponent<AnimalBullet>().Death()); // this animal dies
					break;
				default:
					//Debug.Log("Error: enemy is " + animalBullet.color);
					break;
				}

				return;
			}

			if(color == "brown")
			{
				switch(bulletScript.color)
				{
				case "orange":
					//Debug.Log ("brown beats orange");
					StartCoroutine(bulletScript.Death());				  // enemy dies
					break;
				case "gray":
					//Debug.Log ("gray beats brown");
					StartCoroutine(GetComponent<AnimalBullet>().Death()); // this animal dies
					break;
				default:
					//Debug.Log("Error: enemy is " + animalBullet.color);
					break;
				}
				return;
			}
		}
	}
}