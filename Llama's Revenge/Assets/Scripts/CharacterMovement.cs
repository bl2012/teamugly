using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    private GameManager manager;

    public Buttons[] input;
    private InputState inputState;
    public GameObject[] spawners;
    private GameObject activeSpawner;

    private float attackTimeElapsed = 0f;
    private float delay = 0.25f;
    private float timeElapsed = 0f;

    private bool released = true;

    private int xpos = -815;
    private int zpos = 0;

    private Transform player;

    //private bool menuVisible = false;
    private GameObject ColorM;

    public int numNeeded;

    public enum Positions : int
    {
        TOP = 400,
        TMID = 150,
        BMID = -150,
        BOTTOM = -400
    }

    public Vector3 setPosition(int ypos)
    {
        return new Vector3(xpos, ypos, zpos);
    }

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.farthestReached = Application.loadedLevelName;

        manager.setNumNeededToWin(numNeeded);

        player = gameObject.GetComponent<Transform>();
        player.position = setPosition((int)Positions.TOP);
        activeSpawner = spawners[0];
        inputState = GetComponent<InputState>();
    }

    void Update()
    {

        var up = inputState.GetButtonValue(input[0]);
        var down = inputState.GetButtonValue(input[1]);
        var left = inputState.GetButtonValue(input[2]);
        var right = inputState.GetButtonValue(input[3]);
        var fire = inputState.GetButtonValue(input[4]);
        var pause = inputState.GetButtonValue(input[5]);
        var exit = inputState.GetButtonValue(input[6]);
        var menu = inputState.GetButtonValue(input[7]);

        if (timeElapsed > delay && !Input.anyKey)
            released = true;

        if (pause && released)
        {
            Debug.Log("Pause pressed");
            released = false;

            manager.PauseGame();

            timeElapsed = 0;

        }

        if (menu && released)
        {
            Debug.Log("Menu pressed");
            released = false;

            if (ColorM == null)
                ColorM = GameObject.Instantiate(Resources.Load("ColorWheel")) as GameObject;
            else
                Destroy(ColorM);

            timeElapsed = 0;

        }

        if (exit)
        {
            Debug.Log("Exit pressed");

            manager.QuitGame();
        }

        if (manager.paused)
        {
            if (fire && released)
                manager.loadScene("MainMenu");

            timeElapsed += Time.deltaTime;

            return;
        }

        if (down && released)
        {
            released = false;
            Debug.Log("Down Key Pressed");

            switch ((int)player.position.y)
            {

                case (int)Positions.TOP:
                    player.position = setPosition((int)Positions.TMID);
                    break;
                case (int)Positions.TMID:
                    player.position = setPosition((int)Positions.BMID);
                    break;
                case (int)Positions.BMID:
                    player.position = setPosition((int)Positions.BOTTOM);
                    break;
                case (int)Positions.BOTTOM:
                    player.position = setPosition((int)Positions.TOP);
                    break;
            }

            timeElapsed = 0;
        }
        if (up && released)
        {
            released = false;
            Debug.Log("Up Key Pressed");


            switch ((int)player.position.y)
            {
                case (int)Positions.TOP:
                    player.position = setPosition((int)Positions.BOTTOM);
                    break;
                case (int)Positions.TMID:
                    player.position = setPosition((int)Positions.TOP);
                    break;
                case (int)Positions.BMID:
                    player.position = setPosition((int)Positions.TMID);
                    break;
                case (int)Positions.BOTTOM:
                    player.position = setPosition((int)Positions.BMID);
                    break;
            }

            timeElapsed = 0;

        }

        if (fire && attackTimeElapsed > delay)
        {
            Debug.Log("Fire pressed");

            var bulletllama = activeSpawner.GetComponent<ShootAnimal>().currentAnimal.GetComponent<AnimalBullet>();
            if (bulletllama.launched) return;

            StartCoroutine(activeSpawner.GetComponent<ShootAnimal>().Attack(false));
            attackTimeElapsed = 0;

        }
        else if (left && released)
        {
            Debug.Log("Left pressed");

            if (activeSpawner.GetComponent<ShootAnimal>().currentAnimal == null) return;
            var bulletllama = activeSpawner.GetComponent<ShootAnimal>().currentAnimal.GetComponent<AnimalBullet>();
            if (bulletllama.launched) return;

            if (activeSpawner.GetComponent<ShootAnimal>().onSpawner)
            {
                released = false;
                StartCoroutine(activeSpawner.GetComponent<SwitchSoldier>().SwitchLeft());
            }
            timeElapsed = 0;

        }
        else if (right && released)
        {
            Debug.Log("Right pressed");

            if (activeSpawner.GetComponent<ShootAnimal>().currentAnimal == null) return;
            if (activeSpawner.GetComponent<ShootAnimal>().defeated) return;
            var bulletllama = activeSpawner.GetComponent<ShootAnimal>().currentAnimal.GetComponent<AnimalBullet>();
            if (bulletllama.launched) return;

            var llama = activeSpawner.GetComponent<ShootAnimal>().currentAnimal.GetComponent<Transform>();

            if (activeSpawner.GetComponent<ShootAnimal>().onSpawner && Mathf.Abs(llama.position.x - activeSpawner.GetComponent<Transform>().position.x) < llama.GetComponent<BoxCollider2D>().size.x)
            {
                released = false;
                StartCoroutine(activeSpawner.GetComponent<SwitchSoldier>().SwitchRight());

            }
            timeElapsed = 0;

        }

        var dtime = Time.deltaTime;
        timeElapsed += dtime;
        attackTimeElapsed += dtime;

    }

    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (!otherCollider.CompareTag("llama_spawner")) return;

        //Debug.Log ("Commander touching " + otherCollider.name);
        activeSpawner = otherCollider.gameObject;

    }
}