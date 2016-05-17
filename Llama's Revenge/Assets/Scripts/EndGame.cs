using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	public GameObject[] spawners;
	private GameManager manager;

	void Start()
	{
		manager = GameObject.Find ("GameManager").GetComponent<GameManager>();
        if (gameObject.name == "llama_spawners")
        {
            manager.setSpawnHub(0, gameObject);
            //Debug.Log("llama spawn hub set");
        }
        else if (gameObject.name == "mlion spawners")
        {
            //Debug.Log("mlion spawn hub set");
            manager.setSpawnHub(1, gameObject);
        }
    }

	public void updateList(GameObject spwnr)
	{
        //Debug.Log("Check for level end");
        manager.EndLevel();
	}
    
}
