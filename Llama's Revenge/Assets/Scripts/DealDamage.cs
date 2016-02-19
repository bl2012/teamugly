using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

	public int health = 1;
	public bool enemy = true;

	public void dealDamage(int amount){

		health -= amount;

		if(health <= 0){
			Destroy (gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D otherCollider){

        GameObject bullet = otherCollider.gameObject;
        AnimalBullet bulletScript = bullet.GetComponent<AnimalBullet>();

		if(bulletScript != null){
			if(bulletScript.ifEnemyHit != enemy){
				dealDamage(bulletScript.damage);
                dealDamage(health);	
			}
        
            if (bulletScript.damage <= 0)
                Destroy(bullet);

            if (health <= 0)
                Destroy(gameObject);
		}
	}
}
