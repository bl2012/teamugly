using UnityEngine;
using System.Collections;
using System;

public class MlionMovement : MonoBehaviour {

	private Rigidbody2D body2D;
    public GameObject player;
    private Transform mlion;
	public GameObject[] spawners;
    private GameObject activeSpawner = null;

    private float delay        		 = 0.75f;
	public float soldierDelay 		 = 4f;
	private float soldierTimeElapsed = 0f;
	private float timeElapsed 		 = 0f;

	private float dTime;

    //private bool released = true;
    private System.Random rand = new System.Random();

	private int xpos = 830;
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
		mlion = gameObject.GetComponent<Transform> ();
		mlion.position = SetPosition ((int)Positions.TOP);
        timeElapsed = delay;
    }

    void Update()
    {
        if (player == null) return;

        var newPlayerPos = player.GetComponent<Transform>();

        if (timeElapsed > delay)
        {

            if (newPlayerPos.position.y < mlion.position.y)
            {
                moveDown(mlion);
                timeElapsed = 0;

            }
            else if (newPlayerPos.position.y > mlion.position.y)
            {
                moveUp(mlion);
                timeElapsed = 0;
            }
            else
            {
                randomMovement(mlion);
            }
        }

        var bulletlion = (activeSpawner != null && activeSpawner.GetComponent<ShootAnimal>().currentAnimal != null) ? 
                          activeSpawner.GetComponent<ShootAnimal>().currentAnimal.GetComponent<AnimalBullet>() : null;
        if (bulletlion != null && !bulletlion.launched)
        {
            randomLlama();
        }

        if (soldierTimeElapsed > soldierDelay)
        {
            ShootAnimal bullet = activeSpawner.GetComponent<ShootAnimal>();
            if (activeSpawner != null && bullet != null)
            {
                StartCoroutine(bullet.Attack(true));
            }

            soldierTimeElapsed = 0;
        }        

        dTime = Time.deltaTime;
		timeElapsed += dTime;
		//boulderTimeElapsed += dTime;
		soldierTimeElapsed += dTime;

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

    private void randomLlama()
    {
        switch (rand.Next(50)) // randomize switching
        {
            case 0:
                StartCoroutine(activeSpawner.GetComponent<SwitchSoldier>().SwitchLeft());
                break;
            case 1:
                StartCoroutine(activeSpawner.GetComponent<SwitchSoldier>().SwitchRight());
                break;
            default:
                // do nothing;
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


public void OnTriggerStay2D(Collider2D otherCollider)
{
	if (otherCollider.tag != "mlion_spawner" || otherCollider == null) return;
	if(!otherCollider.GetComponent<ShootAnimal>().onSpawner) return;
	if (otherCollider.GetComponent<ShootAnimal> ().defeated)
		return;

        //Debug.Log("mlion commander touching " + otherCollider.name);
        activeSpawner = otherCollider.gameObject;

        
	
}
}