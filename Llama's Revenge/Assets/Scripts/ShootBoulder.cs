using UnityEngine;
using System.Collections;

public class ShootBoulder : MonoBehaviour {

	public Transform BoulderPrefab;
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
			var bulletTrans = Instantiate (BoulderPrefab) as Transform;
			bulletTrans.position = new Vector3(transform.position.x + 200, transform.position.y, transform.position.z);
			AnimalBullet bullet = bulletTrans.gameObject.GetComponent<AnimalBullet>();
			
			if(bullet != null){
				bullet.ifEnemyHit = ifEnemy;
			}

		}
	}
	
	public bool CanAttack{
		get{
			return cooldown <= 0f;
		}
	}
}
