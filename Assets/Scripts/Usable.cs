using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Usable : MonoBehaviour
{

    [HideInInspector]
    public string UsableText, ControlsText;
    public Transform camPos, camLookDir;
    public CinemachineVirtualCamera cam, closeCam;
    protected PlayerInput input;
    protected bool inUse;

    protected virtual void Start()
    {
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    public virtual void Use()
    {

    }

    public virtual void Back()
    {
        inUse = false;
        input.SwitchCurrentActionMap("Player");
        closeCam.Follow = null;
        closeCam.LookAt = null;
        closeCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
    }

    public virtual void Submit()
    {

    } 
}
