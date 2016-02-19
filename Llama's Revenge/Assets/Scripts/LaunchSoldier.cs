using UnityEngine;
using System.Collections;

public class LaunchSoldier : MonoBehaviour {

    public GameObject player;

    public Buttons[] input;
    private InputState inputState;

    private float delay = 0.5f;
    private float timeElapsed = 0f;
    private bool released = true;
    private bool launched = false;

	// Use this for initialization
	void Start () {

        inputState = GetComponent<InputState>();
	}
	
	// Update is called once per frame
	void Update () {
        var left = inputState.GetButtonValue(input[0]);
        var right = inputState.GetButtonValue(input[1]);
        var x  = inputState.GetButtonValue (input[2]);

        /* The problem we seem to be having is that the buttons aren't registering as pressed
           Once we figure out why that is, getting the llamas to walk across the screen should be pretty simple.
        */

        if (left) 
            Debug.Log("left is called");
        if (right)
            Debug.Log("right is called");
        if (x)
            Debug.Log("x is called");

        if (timeElapsed > delay && !Input.anyKey)
        {
            released = true;
            //Debug.Log("button released");
        }

        if (x && released)
        {
            
            released = false;
            //Debug.Log("Fire Key Pressed");

            if (gameObject.transform.position.y == player.transform.position.y && gameObject.transform.position.x > -100)
            {
                gameObject.transform.Translate(new Vector3(-1, 0, 0));
            }

            launched = true;
            timeElapsed = 0;
        }
        if (left && released )
        {

            released = false;
            //Debug.Log("Left Key Pressed");

            if (gameObject.transform.position.y == player.transform.position.y && !launched)
            {
                //gameObject.transform.Translate(new Vector3(-1, 0, 0));
            }

            timeElapsed = 0;
        }
        if (right && released )
        {

            released = false;
            //Debug.Log("Right Key Pressed");

            if (gameObject.transform.position.y == player.transform.position.y && !launched)
            {
                //gameObject.transform.Translate(new Vector3(-1, 0, 0));
            }

            timeElapsed = 0;
        }

        timeElapsed += Time.deltaTime;

    }

    private void launch()
    {


    }
}


