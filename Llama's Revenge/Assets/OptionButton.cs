using UnityEngine;
using System.Collections;

public class OptionButton : MonoBehaviour
{
    public Sprite upGraphic;
    public Sprite downGraphic;
    public string nextScene;
    private bool active;
    private Vector2 inactiveSize = new Vector2(93, 93);
    private Vector2 activeSize = new Vector2(103, 103);

    void OnStart()
    {
        this.GetComponent<SpriteRenderer>().sprite = upGraphic;
        active = true;
        press();
    }

    public void press()
    {
        active = !active;

        if (isActive())
        {
            GetComponent<SpriteRenderer>().sprite = downGraphic;
            GetComponent<Transform>().localScale = activeSize;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = upGraphic;
            GetComponent<Transform>().localScale = inactiveSize;
        }

    }

    public bool isActive()
    {
        return active;
    }

    public string next()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (nextScene == "next")
        {
            nextScene = manager.scenes[manager.currentSceneIndex + 1];
        }

        if(nextScene == "back")
        {
            nextScene = manager.scenes[manager.currentSceneIndex - 1];
        }

        if(nextScene == "farthest")
        {
            nextScene = manager.farthestReached;
        }

        return nextScene;
    }


}
