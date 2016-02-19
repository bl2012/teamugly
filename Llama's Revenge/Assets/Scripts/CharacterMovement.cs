using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public Buttons[] input;
	//private Rigidbody2D body2D;
	private InputState inputState;

    private float delay       = 0.5f;
    private float timeElapsed = 0f;

    private bool released = true;

	private int xpos = 830;
	private int zpos = 0;

	private Transform player;

	public enum Positions : int
	{
		TOP    = 400,
		TMID   = 150,
		BMID   = -150,
		BOTTOM = -400
	}

	public Vector3 setPosition(int ypos)
	{
		return new Vector3(xpos, ypos, zpos);
	}

	void Start () {

		//body2D = GetComponent<Rigidbody2D> ();
		player = gameObject.GetComponent<Transform> ();
		player.position = setPosition ((int)Positions.TOP);
		inputState = GetComponent<InputState>();
	}

	void Update () {

		var up    = inputState.GetButtonValue (input [0]);
		var down  = inputState.GetButtonValue (input [1]);
		var left  = inputState.GetButtonValue (input [2]);
		//var right = inputState.GetButtonValue (input [3]);
        //var fire  = inputState.GetButtonValue (input [4]); -- in Launch Soldier

        if (timeElapsed > delay && !Input.anyKey)
            released = true;

        if (down && released){

            released = false;
            Debug.Log("Down Key Pressed");

			switch((int)player.position.y)
			{

			case (int)Positions.TOP:
				player.position = setPosition ((int)Positions.TMID);
				break;
			case (int)Positions.TMID:
				player.position = setPosition ((int)Positions.BMID);
				break;
			case (int)Positions.BMID:
				player.position = setPosition ((int)Positions.BOTTOM);
				break;
			case (int)Positions.BOTTOM:
				player.position = setPosition ((int)Positions.TOP);
				break;
			}

            timeElapsed = 0;
		}
		if(up && released )
        {
            released = false;
            Debug.Log("Up Key Pressed");


            switch ((int)player.position.y)
			{
			case (int)Positions.TOP:
				player.position = setPosition ((int)Positions.BOTTOM);
				break;
			case (int)Positions.TMID:
				player.position = setPosition ((int)Positions.TOP);
				break;
			case (int)Positions.BMID:
				player.position = setPosition ((int)Positions.TMID);
				break;
			case (int)Positions.BOTTOM:
				player.position = setPosition ((int)Positions.BMID);
				break;
			}

            timeElapsed = 0;
		}

        if (left)
        {
            ShootAnimal bullet = GetComponent<ShootAnimal>();
            if (bullet != null)
            {
                bullet.Attack(false);
            }
        }



        /*   if (left && timeElapsed > delay && released)
           {

               released = false;
               Debug.Log("Left Key Pressed");

               if (gameObject.transform.position.y == player.transform.position.y )
               {
                   //gameObject.transform.Translate(new Vector3(-1, 0, 0));
               }

               timeElapsed = 0;
           }
           if (right && timeElapsed > delay && released)
           {

               released = false;
               Debug.Log("Right Key Pressed");

               if (gameObject.transform.position.y == player.transform.position.y)
               {
                   //gameObject.transform.Translate(new Vector3(-1, 0, 0));
               }

               timeElapsed = 0;
           }*/


        timeElapsed += Time.deltaTime;
	}
}