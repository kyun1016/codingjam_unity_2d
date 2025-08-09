using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackGround_InGame1 : MonoBehaviour
{
    public Animator anim;

    public void ResetPosition()
    {
    }
    public void Reset()
    {
        ResetPosition();
    }

    public void TriggerLeft()
    {
        anim.SetTrigger("Left");
    }
    public void TriggerRight()
    {
        anim.SetTrigger("Right");
    }

    public void TriggerStart()
    {
        anim.SetTrigger("Start");
    }
    public void TriggerTimeout()
    {
        anim.SetTrigger("Timeout");
    }
}