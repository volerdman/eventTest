using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private int count = 0;
    public Text counter;
    public Text text;
    public Text eventText;
    public Animator animator;
    public bool buttonAnimation;
    public bool doorAnimation;
    public Animator doorAnimator;
    public bool isOpen = false;
    public AudioClip anotherSound;
    public Animator cameraAnimator;
    public AudioClip soundButton;

    private void OnEnable()
    {
        ButtonManager.onEnter += ShowText;
        ButtonManager.onExit += HideText;
        ButtonManager.pressButton += PressButton;
        ButtonManager.openDoor += OpenDoorAnimation;
        ButtonManager.animateCamera += CameraAnimation;
        ButtonManager.playAnotherSound += PlayAnotherSound;
        ButtonManager.showText += ShowEventText;
        ButtonManager.playSound += PlaySound;
    }

    private void OnDisable()
    {
        ButtonManager.onEnter -= ShowText;
        ButtonManager.onExit -= HideText;
        ButtonManager.pressButton -= PressButton;
        ButtonManager.openDoor -= OpenDoorAnimation;
        ButtonManager.animateCamera -= CameraAnimation;
        ButtonManager.playAnotherSound -= PlayAnotherSound;
        ButtonManager.showText -= ShowEventText;
        ButtonManager.playSound -= PlaySound;
    }

    void ShowText()
    {
        text.gameObject.SetActive(true);
    }

    void HideText()
    {
        text.gameObject.SetActive(false);
    }

    void PressButton()
    {
        isOpen = !isOpen;
        if (!(ButtonManager.listButtonPressed.Contains(ButtonManager.objectTag)))
        {
            count++;
            counter.text = count.ToString();
        }
    }

    void OpenDoorAnimation(bool buttonAnimation)
    {
        if (isOpen)
        {
            doorAnimator.Play("DoorController");
            doorAnimator.SetBool("isOpen", isOpen);
        }
        else
        {
            doorAnimator.Play("CloseDoor");
            doorAnimator.SetBool("isOpen", isOpen);
        }
    }

    void CameraAnimation(bool buttonAnimation)
    {
        animator.Play("AnimateCameraButton");
        cameraAnimator.Play("CameraAnimation");

    }

    void PlayAnotherSound(bool buttonAnimation)
    {
        GetComponent<AudioSource>().PlayOneShot(anotherSound);
    }

    void ShowEventText(bool buttonAnimation)
    {
        if (buttonAnimation)
            animator.Play("ButtonAnimation");
        eventText.gameObject.SetActive(true);
        StartCoroutine(HideEventText());
    }

    IEnumerator HideEventText()
    {
        yield return new WaitForSeconds(1.5f);
        eventText.gameObject.SetActive(false);
        buttonAnimation = false;
    }

    void PlaySound(bool buttonAnimation)
    {
        if (buttonAnimation)
            animator.Play("PlaySound");
        if (ButtonManager.soundCheck)
            StartCoroutine(PlayEventSound());
        buttonAnimation = false;
    }

    IEnumerator PlayEventSound()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<AudioSource>().PlayOneShot(soundButton);
    }
}
