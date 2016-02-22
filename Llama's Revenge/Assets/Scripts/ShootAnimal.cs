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
	
	public void Attack(bool ifEnemy, string type){
		if(CanAttack){
			Debug.Log ("FOSHIZZLE");
			cooldown = ROF;
			var bulletTrans = Instantiate (AnimalPrefab) as Transform;

			if(type == "mlion"){//<<<<<<<<<<<<<<<<<==================If bullet is fired from Mlion
				bulletTrans.position = new Vector3(transform.position.x + 200, transform.position.y, transform.position.z);
			}
			else{//<<<<<<<<<<<<<<<<<<<<====================If bullet is fired from llama
				bulletTrans.position = new Vector3(transform.position.x - 200, transform.position.y, transform.position.z);//<<<<<<<
			}
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
