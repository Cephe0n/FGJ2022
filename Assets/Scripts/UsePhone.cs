using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class UsePhone : Usable
{

    public GameObject spook1, spook2, spook3;

    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Answer?";
    }

    // Update is called once per frame
    public override void Use()
    {

        StartCoroutine(PhoneEvent());
    }

    IEnumerator PhoneEvent()
    {
        UsableText = "";
        ControlsText = "";
        input.SwitchCurrentActionMap("NoControls");
        base.MoveCamera();
        MasterAudio.StopAllOfSound("phone_ring");
        MasterAudio.PlaySound3DAtTransformAndForget("phone_pickup", this.transform);
        yield return new WaitForSecondsRealtime(0.5f);
        MasterAudio.PlaySound3DAtTransformAndForget("phone_off_hook", this.transform);
        yield return new WaitForSecondsRealtime(4.5f);
        MasterAudio.PlaySound3DAtTransformAndForget("phone_putdown", this.transform);
        spook1.SetActive(true);
        spook2.SetActive(true);
        spook3.SetActive(true);
        base.Back();
        GlobalStuff.instance.PhoneEventDone = true;
        yield return new WaitForSecondsRealtime(4f);
        spook1.SetActive(false);
        spook2.SetActive(false);
        spook3.SetActive(false);
    }

}
