using UnityEngine;
using System.Collections;

public class AnimalBullet : MonoBehaviour {

	//public int damage = 1;
	public string team = "";
	public string color = "";
	public bool ifEnemyHit = false;
    public bool launched = false;
	public bool fighting;
	public bool completed; // animal has landed on an enemy spawner
    public Transform mlionCommander;
	private Animator animator;

	public AudioSource source;

	void Start () {
		fighting = false;
		launched = false;
		completed = false;
	}

	public IEnumerator Death()
	{
		if (gameObject == null)
			StopCoroutine (Death ());

		//Debug.Log (gameObject.tag + " is slain");
		
		AudioSource[] bothAudio = GetComponents<AudioSource> ();//---------------------------------Sound
		source = bothAudio[1];//---------------------------------Sound
		source.Play();
		
		animator = gameObject.GetComponent<Animator> ();
		animator.SetBool ("Dead", true);
		yield return new WaitForSeconds (.01f);
		
		Destroy (gameObject);
		//StopCoroutine ("Fight");
	}

	public void OnTriggerEnter2D(Collider2D otherCollider)
	{
		var infiltrated1 = (otherCollider.tag == "llama_spawner" && team == "mlion");
		var infiltrated2 = (otherCollider.tag == "mlion_spawner" && team == "llama");

		if (otherCollider == null)
			return;

		if(infiltrated1 || infiltrated2)
		{
			completed = true;
			otherCollider.GetComponent<ShootAnimal>().defeated = true;
            completed = true;
			GetComponent<Animator>().SetBool("Walk", false);
            otherCollider.GetComponentInChildren<SpriteRenderer>().enabled = true; // raise the flag of victory
			GetComponent<Transform>().localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            otherCollider.GetComponentInParent<EndGame>().updateList(otherCollider.GetComponent<GameObject>());
            return;
		}

		if (GetComponent<DealDamage> () == null || otherCollider.GetComponent<DealDamage> () == null)
			return;

		if(!launched)
		{
			return;
		}
		else if (otherCollider.GetComponent<AnimalBullet>() != null && !otherCollider.GetComponent<AnimalBullet> ().launched) {
			return;
		}

		//if (team != otherCollider.GetComponent<AnimalBullet> ().team) {
			//StartCoroutine (GetComponent<DealDamage> ().Fight (otherCollider.GetComponent<Transform> ()));
			//StartCoroutine (otherCollider.GetComponent<DealDamage>().Fight(GetComponent<Transform>()));

		//}
	}

}
