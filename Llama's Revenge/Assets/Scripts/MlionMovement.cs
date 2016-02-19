using UnityEngine;
using System.Collections;
using System;

public class MlionMovement : MonoBehaviour {
	/*----------------------------------------------------------------------------------------------------
	 * 
	 * Dont forget to set up mlion commander
	 * 
	 * ---------------------------------------------------------------------------------------------------
	 */

	private Rigidbody2D body2D;
    public GameObject player;
    private Transform playerPos;
    private Transform mlion;

    private float delay       = 0.75f;
	private float timeElapsed = 0f;

    //private bool released = true;
    private System.Random rand = new System.Random();

	private int xpos = -830;
	private int zpos = 0;

	public enum Positions : int
	{
		TOP    = 400,
		TMID   = 150,
		BMID   = -150,
		BOTTOM = -400
	}

	public Vector3 SetPosition(int ypos)
	{
		return new Vector3(xpos, ypos, zpos);
	}


	void Start () {
		//body2D = GetComponent<Rigidbody2D> ();
		mlion = gameObject.GetComponent<Transform> ();
		mlion.position = SetPosition ((int)Positions.TOP);
        playerPos = player.GetComponent<Transform>();
		//inputState = GetComponent<InputState>();
	}

    void Update()
    {
        var newPlayerPos = player.GetComponent<Transform>();

        if (timeElapsed > delay)
        {

            if (newPlayerPos.position.y < mlion.position.y /*&& timeElapsed > delay*/)
            {
                //Debug.Log("mlion is above lion");
                moveDown(mlion);
                timeElapsed = 0;

            }
            else if (newPlayerPos.position.y > mlion.position.y /*&& timeElapsed > delay*/)
            {
                //Debug.Log("mlion is below lion");
                moveUp(mlion);
                timeElapsed = 0;
            }
            else
            {
                //Debug.Log("mlion in same lane as lion");
                randomMovement(mlion);
            }

			if (newPlayerPos.position.y == mlion.position.y)
			{
				ShootBoulder bullet = GetComponent<ShootBoulder>();
				if (bullet != null)
				{
					bullet.Attack(false);
				}
			}
        }

            timeElapsed += Time.deltaTime;
            //playerPos = newPlayerPos;
    }

    private void moveDown(Transform obj)
    {
        switch ((int)obj.position.y) // move down
        {
            case (int)Positions.TOP:
                obj.position = SetPosition((int)Positions.TMID);
                break;
            case (int)Positions.TMID:
                obj.position = SetPosition((int)Positions.BMID);
                break;
            case (int)Positions.BMID:
                obj.position = SetPosition((int)Positions.BOTTOM);
                break;
            case (int)Positions.BOTTOM:
                obj.position = SetPosition((int)Positions.TOP);
                break;
        }

    }

    private void moveUp(Transform obj)
    {
        switch ((int)obj.position.y) // move up
        {
            case (int)Positions.TOP:
                obj.position = SetPosition((int)Positions.BOTTOM);
                break;
            case (int)Positions.TMID:
                obj.position = SetPosition((int)Positions.TOP);
                break;
            case (int)Positions.BMID:
                obj.position = SetPosition((int)Positions.TMID);
                break;
            case (int)Positions.BOTTOM:
                obj.position = SetPosition((int)Positions.BMID);
                break;
        }

    }

    private void randomMovement(Transform obj)
    {
        switch (rand.Next(50)) // randomize AI movement
        {
            case 0:
                moveUp(obj);
                timeElapsed = 0;
                break;
            case 1:
                moveDown(obj);
                timeElapsed = 0;
                break;
            default:
                // do nothing;
                break;

        }
    }
}