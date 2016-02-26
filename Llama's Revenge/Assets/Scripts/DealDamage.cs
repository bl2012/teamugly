using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

	public int health = 1;
	public bool enemy = true;

	public void dealDamage(int amount){

		health -= amount;

		if(health <= 0){
			Destroy (gameObject);
            Debug.Log("object destroyed");
		}
	}

	public void OnTriggerEnter2D(Collider2D otherCollider){

        GameObject bullet = otherCollider.gameObject;
        AnimalBullet bulletScript = bullet.GetComponent<AnimalBullet>();

		if(bulletScript != null){
            if (!bulletScript.launched)
                return;
			if(bulletScript.ifEnemyHit != enemy){
				bulletScript.damage -= 1;
                dealDamage(health);
                Debug.Log("damage dealt");
			}

           if (bulletScript.damage <= 0)
            {
                Destroy(bullet);
                Debug.Log("target destroyed");
            }
		}
        else if(otherCollider.CompareTag("commander") && gameObject.CompareTag("mlion"))
        {
            Debug.Log("You sank my battleship!");
            Destroy(bullet);
        }

	}
}
