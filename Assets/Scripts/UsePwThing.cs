using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsePwThing : Usable
{
    public TMP_InputField field;

    [SerializeField]
    string correctPw;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Use";
    }

    public override void Use()
    {
        if (!inUse)
        {
            inUse = true;
            UsableText = "";
            ControlsText = "ESC/RMB - Back\nEnter - Submit";
            input.SwitchCurrentActionMap("ViewItem");
            closeCam.Follow = camPos;
            closeCam.LookAt = camLookDir;
            closeCam.gameObject.SetActive(true);
            cam.gameObject.SetActive(false);
            field.ActivateInputField();
        }
    }

    public override void Submit()
    {
        string givenPw = field.text;

        if (givenPw == correctPw)
        field.text = "Access Granted";
        else
        field.text = "Access Denied";
    }

    public override void Back()
    {
        if (inUse)
        {
            UsableText = "(LMB) Use";
            base.Back();
        }
    }
}
