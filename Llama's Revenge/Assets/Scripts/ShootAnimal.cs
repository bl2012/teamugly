using UnityEngine;
using System.Collections;

public class ShootAnimal : MonoBehaviour {
	
	public Transform AnimalPrefab;
	public float ROF = 2.0f;
	private float cooldown;
	
	void Start () {
		cooldown = 0f;
	}
	
	void Update () {
		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}
	}
	
	public void Attack(bool ifEnemy){
		if(CanAttack){
			cooldown = ROF;
			var bulletTrans = Instantiate (AnimalPrefab) as Transform;
			bulletTrans.position = new Vector3(transform.position.x - 200, transform.position.y, transform.position.z);
			AnimalBullet bullet = bulletTrans.gameObject.GetComponent<AnimalBullet>();
			
			if(bullet != null){
				bullet.ifEnemyHit = ifEnemy;
			}
			
			//BulletMovement move = bulletTrans.gameObject.GetComponent<BulletMovement>();
			
			/*if(move != null){
				move.direction = this.transform.right;
			}*/
		}
	}
	
	public bool CanAttack{
		get{
			return cooldown <= 0f;
		}
	}
}
