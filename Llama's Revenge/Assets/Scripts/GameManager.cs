using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public string[] scenes;
	private static GameManager firstInstance = null;
	public int currentSceneIndex;
	public string farthestReached;
    private GameObject[] spawnHub;
    private int countNeededToWin = 4;
    public bool paused = false;
    private GameObject PauseM;


    //-----------------Level Setup------------------------
    void Awake() // implement singleton
	{
		if (firstInstance == null)
			firstInstance = this;
		else if (firstInstance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		//Debug.Log (gameObject.name + "Shouldn't be destroyed on load");
		currentSceneIndex = Application.loadedLevel;

        spawnHub = new GameObject[2];
        paused = false;
	}

    public void setSpawnHub(int index, GameObject hub)
    {
        spawnHub[index] = hub;
    }

    public void setNumNeededToWin(int num)
    {
        countNeededToWin = num;
    }

	void OnLevelWasLoaded()
	{
        // keep scene index accurate
        /*if (!Application.loadedLevelName.Equals (scenes [currentSceneIndex])) {
			for(int i = 0; i < scenes.Length; i++)
			{
				var Str = scenes[i];
				if(Application.loadedLevelName.Equals (Str)){
					currentSceneIndex = i;
					break;
				}
			}
		}*/

        currentSceneIndex = Application.loadedLevel;

    }

    //-----------------Manage the Level-------------------

    public void PauseGame()
    {
        paused = !paused;

        if(paused)
        {
            GameObject.Find("mlion_commander").GetComponent<MlionMovement>().enabled = false;
            var mlions = GameObject.FindGameObjectsWithTag("mlion");
            var llamas = GameObject.FindGameObjectsWithTag("soldier");

            foreach(GameObject lion in mlions)
            {
                if (lion.GetComponent<BulletMovement>() != null)
                    lion.GetComponent<BulletMovement>().speed = new Vector2(0, 0);
            }
            foreach(GameObject llama in llamas)
            {
                if (llama.GetComponent<BulletMovement>() != null)
                    llama.GetComponent<BulletMovement>().speed = new Vector2(0, 0);
            }

            PauseM = GameObject.Instantiate(Resources.Load("llamasrevenge_Pause(Arcade)")) as GameObject;
        }
        else
        {
            GameObject.Find("mlion_commander").GetComponent<MlionMovement>().enabled = true;
            var mlions = GameObject.FindGameObjectsWithTag("mlion");
            var llamas = GameObject.FindGameObjectsWithTag("soldier");

            foreach (GameObject lion in mlions)
            {
                if (lion.GetComponent<BulletMovement>() != null)
                    lion.GetComponent<BulletMovement>().speed = new Vector2(100, 100);
            }
            foreach (GameObject llama in llamas)
            {
                if (llama.GetComponent<BulletMovement>() != null)
                    llama.GetComponent<BulletMovement>().speed = new Vector2(100, 100);
            }

            if (PauseM != null)
                Destroy(PauseM);
        }
    }

    //------------------End the Level---------------------

    private bool isLevelOver()
    {
        var llama_spawner = spawnHub[0].GetComponent<EndGame>().spawners;
        var mlion_spawner = spawnHub[1].GetComponent<EndGame>().spawners;

        for (int i = 0; i < llama_spawner.Length; i++)
        {
            if ((!llama_spawner[i].GetComponent<ShootAnimal>().defeated && !mlion_spawner[i].GetComponent<ShootAnimal>().defeated))
            {
              //  Debug.Log("Level is not over");
                return false;
            }
        }
        //Debug.Log("Level is over");
        return true;
    }

    public void EndLevel()
    {
        if (!isLevelOver()) return;

        var llama_spawner = spawnHub[0].GetComponent<EndGame>().spawners;
        var llama_count = 0;
        var mlion_count = 0;

        GameObject.Find("mlion_commander").GetComponent<MlionMovement>().enabled = false;
        GameObject.Find("llama_commander").GetComponent<CharacterMovement>().enabled = false;


        for (int i = 0; i < llama_spawner.Length; i++)
        {
            if (llama_spawner[i].GetComponent<ShootAnimal>().defeated)
                mlion_count++;
            else llama_count++;
        }

        Debug.Log("Llama count " + llama_count + " | mlion count " + mlion_count);

        if(llama_count >= countNeededToWin)
        {
            //Debug.Log("llama's win");
            StartCoroutine(WinGame());
        }
        else
        {
            //Debug.Log("You dead. Thanks for playing");
            StartCoroutine(LoseGame());
        }
    }

    public float Death(string commander_name)
    {
        var commander = GameObject.FindGameObjectWithTag(commander_name);
        var animator = commander.GetComponent<Animator>();

        if (commander == null) return 0;

       // Debug.Log(gameObject.tag + " is slain");

        //AudioSource[] bothAudio = GetComponents<AudioSource>();//---------------------------------Sound
        //source = bothAudio[1];//---------------------------------Sound
        //source.Play();

        animator.SetBool("Dead", true);
        return (animator.speed);
    }


    //-------------------Win  Level-----------------------
    public IEnumerator WinGame()
	{
		//Debug.Log ("Winning");
		yield return new WaitForSeconds(Death("level_boss"));
		nextScene ();
	}

	//-------------------Lose Level-----------------------
	public IEnumerator LoseGame()
	{
		//Debug.Log ("Losing");
		yield return new WaitForSeconds(Death("commander"));
		loadScene ("DeathMenu");
	}

	//---------------Scene Navigation---------------------
	public void nextScene()
	{
		currentSceneIndex++;

		if (currentSceneIndex >= scenes.Length)
			currentSceneIndex = 0;

		loadScene (currentSceneIndex);
	}

/*	public void lastScene()
	{
		currentSceneIndex--;

		if (currentSceneIndex < 0)
			currentSceneIndex = 0;

		loadScene (currentSceneIndex);
	}*/

	public void loadScene(string sceneName)
	{
		var found = false;

		foreach (string Str in scenes) {
			if(Str.Equals(sceneName))
			{
				found = true;
				break;
			}
		}

		if (found)
			Application.LoadLevel (sceneName);
		else if(sceneName == "quit")
        {
            QuitGame();
        }
        else
			Debug.Log ("Level " + sceneName + " not found");
	}

	public void loadScene(int sceneIndex)
	{
		if (sceneIndex >= scenes.Length || sceneIndex < 0)
			sceneIndex = 0;

		Application.LoadLevel (sceneIndex);

	}

    public void QuitGame()
    {
        Application.Quit();
    }
}