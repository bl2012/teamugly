using UnityEngine;
using System.Collections;

public class ShootAnimal : MonoBehaviour {
	
	private GameObject AnimalPrefab;
    public GameObject currentAnimal;

	private AudioSource source;//------------------------------------Sound

	public float ROF = 2.0f;
	private float cooldown;

	private int index;
	private int last_index;
	private float spawnDelay = 0f;
    private float timeElapsed = 0f;

    public bool onSpawner = false;
	public bool defeated = false;
	public string team;
	
	void Start () {
		cooldown = 0f;
		
		last_index = index = gameObject.GetComponent<SwitchSoldier> ().index;
	}
	
	void Update () {
		if (defeated) {
			if(currentAnimal != null)
				Destroy(currentAnimal);
			return;
		}

		if(cooldown > 0){
			cooldown -= Time.deltaTime;
		}

		if (gameObject.GetComponent<SwitchSoldier> () != null)
			index = gameObject.GetComponent<SwitchSoldier> ().index;
		else
			Debug.Log ("SwitchSoldier not found");

        if (!onSpawner && spawnDelay <= 0f)
        {
			StartCoroutine(SpawnAnimal());
			spawnDelay = 0;

			return;
        }
		
		if (onSpawner && last_index != index) {
			Destroy(currentAnimal);
			onSpawner = false;
		}
	
		if (currentAnimal == null || currentAnimal.GetComponent<Transform>() == null) {
			onSpawner = false;
			spawnDelay -= Time.deltaTime;
		}

		timeElapsed += Time.deltaTime;

    }

	private IEnumerator SpawnAnimal()
	{

		onSpawner = true;

		AnimalPrefab = gameObject.GetComponent<SwitchSoldier> ().SoldierTypes [index];

		currentAnimal = Instantiate(AnimalPrefab) as GameObject;
		currentAnimal.GetComponent<Transform> ().parent = gameObject.GetComponent<Transform> ();

		currentAnimal.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		currentAnimal.GetComponent<BulletMovement>().enabled = false;
		currentAnimal.GetComponent<AnimalBullet>().enabled = true;
		currentAnimal.GetComponent<BoxCollider2D>().enabled = true;
		currentAnimal.GetComponent<Animator> ().SetBool("Walk", false);

		source = GetComponent<AudioSource> ();//---------------------------------Sound

		last_index = index;

		yield return new WaitForSeconds (.1f);

	}

	public IEnumerator Attack(bool ifEnemy){
		if (currentAnimal == null)
			StopCoroutine ("Attack");

		if(CanAttack)
        {
			cooldown = ROF;

            currentAnimal.GetComponent<BulletMovement>().enabled = true;
            currentAnimal.GetComponent<AnimalBullet>().enabled = true;
            currentAnimal.GetComponent<BoxCollider2D>().enabled = true;
            currentAnimal.GetComponent<AnimalBullet>().launched = true;
            currentAnimal.GetComponent<Animator>().SetBool("Walk", true);
            Debug.Log(currentAnimal.name + "is walking");

			source.Play ();//-------------------------------Sound

            AnimalBullet bullet = currentAnimal.gameObject.GetComponent<AnimalBullet>();

            if (bullet != null){
            	bullet.ifEnemyHit = ifEnemy;
            }

			yield return new WaitForSeconds(.5f);
			//onSpawner = false;

        }
    }

	public bool CanAttack{

        get {
            if (currentAnimal.GetComponent<AnimalBullet>().completed) return false;

            return cooldown <= 0f;
		}
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.GetComponent<AnimalBullet> ().team == null)
			return;

		var colTeam = col.collider.GetComponent<AnimalBullet> ().team;

		if (colTeam.Equals (team))
			return;

        // check if all attack lanes have been claimed and end game if so
		gameObject.GetComponentInParent<EndGame> ().updateList (gameObject);
	}
}
