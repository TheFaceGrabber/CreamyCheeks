using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreamyCheaks.Input;
using UnityEngine.UI;

public class GamepadImageSwapper : MonoBehaviour
{
    public Sprite KeyboardImage;
    public Sprite GamepadImage;

    Image img;

    private void Start()
    {
        img = gameObject.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update () {
       // img.sprite = InputManager.GetLastInputType() == InputType.Gamepad ? GamepadImage : KeyboardImage;

        //if (InputManager.GetButtonDown("left"))
        //{
        //    print("Left");
        //}
	}
}
