using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    public delegate void ShowElements();
    public static event ShowElements onEnter;

    public delegate void HideElements();
    public static event HideElements onExit;

    public delegate void PressButton();
    public static event PressButton pressButton;

    public delegate void ShowText(bool buttonAnimation);
    public static event ShowText showText;

    public delegate void OpenDoor(bool buttonAnimation);
    public static event OpenDoor openDoor;

    public delegate void PlayAnotherSound(bool buttonAnimation);
    public static event PlayAnotherSound playAnotherSound;

    public delegate void AnimateCamera(bool buttonAnimation);
    public static event AnimateCamera animateCamera;

    public delegate void PlaySound(bool buttonAnimation);
    public static event PlaySound playSound;

    private string[] listButtons = {"Text", "Door", "Sound", "AnimateCamera"};
    public static List<string> listButtonPressed = new List<string>();
    public static string objectTag;
    private bool triggerStay = false;
    public static bool doorCheck = false;
    public static bool soundCheck = false;
    public static bool textCheck = false;
    public AudioClip audioClip;
    

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.F) && triggerStay)
        {
            GetComponent<AudioSource>().PlayOneShot(audioClip);
            pressButton();
            if (doorCheck)
            {
                openDoor(true);
                listButtonPressed.Add(objectTag);
            }else if (soundCheck)
            {
                playSound(true);
                listButtonPressed.Add(objectTag);
            }
            else if (textCheck)
            {
                showText(true);
                listButtonPressed.Add(objectTag);
            }
            else
            {
                playAnotherSound(true);
                animateCamera(true);
                listButtonPressed.Add(objectTag);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(listButtons.Contains(other.gameObject.tag))
        {
            onEnter();
            if (other.gameObject.tag == "Door")
            {
                triggerStay = true;
                textCheck = false;
                doorCheck = true;
                soundCheck = false;
                objectTag = other.gameObject.tag;
            }
            if (other.gameObject.tag == "Sound")
            {
                triggerStay = true;
                textCheck = false;
                soundCheck = true;
                doorCheck = false;
                objectTag = other.gameObject.tag;
            }
            if (other.gameObject.tag == "Text")
            {
                triggerStay = true;
                textCheck = true;
                soundCheck = false;
                doorCheck = false;
                objectTag = other.gameObject.tag;
            }
            if (other.gameObject.tag == "AnimateCamera")
            {
                triggerStay = true;
                textCheck = false;
                soundCheck = false;
                doorCheck = false;
                objectTag = other.gameObject.tag;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        triggerStay = false;
        onExit();
    }
}
