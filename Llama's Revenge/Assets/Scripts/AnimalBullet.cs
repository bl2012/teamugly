using UnityEngine;
using System.Collections;

public class AnimalBullet : MonoBehaviour {

	public int damage = 1;
	public bool ifEnemyHit = false;
    public bool launched = false;
    public Transform mlionCommander;

	void Start () {

        //Destroy(gameObject, 10);
	}

	void Update () {
        if (ifEnemyHit == true)  
        {
            if (gameObject.tag == "mlion") return;

            Destroy(gameObject);

        }

        //if (gameObject.transform.position.x < mlionCommander.position.x)
        //  Debug.Log("You win the level");
    }

    public void OnTriggerDown2D(Collider2D otherCollider)
    {
        if (gameObject.tag != "mlion") return;
        if (!otherCollider.CompareTag("soldier")) return;
        if (!otherCollider.GetComponent<AnimalBullet>().launched) return;

        //Debug.Log("calling Attack function");

        Destroy(gameObject);

            //new WaitForSeconds(delay);
            //otherCollider.GetComponent<ShootAnimal>().onSpawner = false;
    }
}
