using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    public Buttons[] input;
    private InputState inputState;
    public GameObject[] spawners;
    private GameObject mySpawner;

    private float attackTimeElapsed = 0f;
    private float delay = 0.25f;
    private float timeElapsed = 0f;

    private bool released = true;

    private int xpos = 830;
    private int zpos = 0;

    private Transform player;

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

        player = gameObject.GetComponent<Transform>();
        player.position = setPosition((int)Positions.TOP);
        inputState = GetComponent<InputState>();
    }

    void Update()
    {

        var up = inputState.GetButtonValue(input[0]);
        var down = inputState.GetButtonValue(input[1]);
        var left = inputState.GetButtonValue(input[2]);
        //var right = inputState.GetButtonValue (input [3]);
        var fire = inputState.GetButtonValue(input[4]);

        if (timeElapsed > delay && !Input.anyKey)
            released = true;

        if (down && released)
        {

            released = false;
            //Debug.Log("Down Key Pressed");

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
            //Debug.Log("Up Key Pressed");


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

        //if (newPlayerPos.position.y == mlion.position.y) // change to recognize launched llamas
        //{

        //}

        if (fire && released)
        {

        }


        timeElapsed += Time.deltaTime;
        attackTimeElapsed += Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D otherCollider)
    {
        var left = inputState.GetButtonValue(input[2]);
        var right = inputState.GetButtonValue (input [3]);
        var fire  = inputState.GetButtonValue (input [4]);

        if(fire && attackTimeElapsed > delay)
        {

            if (!otherCollider.CompareTag("spawner")) return;

            otherCollider.GetComponent<ShootAnimal>().Attack(false);

            ShootBoulder bullet = GetComponent<ShootBoulder>();
            if (bullet != null)
            {
                bullet.Attack(true);
            }

            attackTimeElapsed = 0;


        }
        else if(left && released)
        {
            Debug.Log("left key pressed");
        }
        else if(right && released)
        {
            Debug.Log("right key pressed");
        }

    }
}