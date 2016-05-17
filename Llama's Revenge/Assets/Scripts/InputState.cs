using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState{
	public bool value;
}

public class InputState : MonoBehaviour {

	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState> ();

    void Awake()
    {
        var inManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        if(inManager.inputState == null) inManager.inputState = this;
    }

	public void SetButtonValue(Buttons key, bool value){
		if (!buttonStates.ContainsKey (key))
			buttonStates.Add (key, new ButtonState ());
		
		var state = buttonStates [key];

		if (state.value && !value) {
            ;// Debug.Log ("Button " + key + " released");
		} else if (state.value && value) {
            ;// Debug.Log ("Button " + key + " down");
		}

		state.value = value;
	}

	public bool GetButtonValue(Buttons key){
		if (buttonStates.ContainsKey (key))
			return buttonStates [key].value;
		else
			return false;
	}

    public string GetButtonName(Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return key.ToString();
        else
            return null;
    }
}
