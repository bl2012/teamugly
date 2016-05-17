using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{

    public Buttons[] input;
    public OptionButton[] optButtons;
    public int activeButton;
    public InputState inputState;

    private bool released = true;

    private float timeElapsed = 0;
    private float delay = .5f;

    private Transform selector;

    private GameManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        inputState = this.GetComponent<InputState>();

        activeButton = 0;
        optButtons[activeButton].press();

    }

    // Update is called once per frame
    void Update()
    {

        if (inputState == null)
        {
            Debug.Log("input state is null");
            return;
        }

        var up = inputState.GetButtonValue(input[0]);
        var down = inputState.GetButtonValue(input[1]);
        var left = inputState.GetButtonValue(input[2]);
        var right = inputState.GetButtonValue(input[3]);
        var fire = inputState.GetButtonValue(input[4]);
        var exit = inputState.GetButtonValue(input[5]);


        if (exit)
        {
            Debug.Log("Exit pressed");

            manager.QuitGame();
        }

        if (timeElapsed > delay && !Input.anyKey)
            released = true;


        if ((down || right) && released)
        {

            released = false;

            activeButton = increment(activeButton);

            timeElapsed = 0;
        }
        if ((up || left) && released)
        {
            released = false;

            activeButton = decrement(activeButton);

            timeElapsed = 0;
        }
        if (fire && released)
        {
            if (optButtons[activeButton].next() == "quit")
                manager.QuitGame();

            manager.loadScene(optButtons[activeButton].next());
        }

        timeElapsed += Time.deltaTime;
    }

    public void adjustButtons(int newActive)
    {
        for (int i = 0; i < optButtons.Length; i++)
        {
            if (optButtons[i].isActive())
            {
                optButtons[i].press();
                break;
            }
        }

        optButtons[newActive].press();
    }

    public int increment(int b)
    {
        b++;

        if (b > optButtons.Length - 1)
            b = 0;

        adjustButtons(b);

        return b;
    }

    public int decrement(int b)
    {
        b--;

        if (b < 0)
            b = optButtons.Length - 1;

        adjustButtons(b);

        return b;
    }

}