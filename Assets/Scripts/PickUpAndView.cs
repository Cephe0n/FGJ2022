using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndView : Usable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Look";
    }

    // Update is called once per frame
    public override void Use()
    {
        if (!inUse)
        {
            inUse = true;
            UsableText = "";
            ControlsText = "ESC/RMB - Back";
            input.SwitchCurrentActionMap("ViewItem");
            closeCam.Follow = camPos;
            closeCam.LookAt = camLookDir;
            closeCam.gameObject.SetActive(true);
            cam.gameObject.SetActive(false);
        }
    }

    public override void Back()
    {
        if (inUse)
        {
            UsableText = "(LMB) Look";
            ControlsText = "";
            base.Back();     
        }
    }
}
