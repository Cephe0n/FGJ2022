using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class RealmShift : Usable
{
    public GameObject Realm1, Realm2;

    bool cooldownDone = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Shift Realm";
    }

    public override void Use()
    {
        if (cooldownDone)
        {
        //base.MoveCamera();
        input.SwitchCurrentActionMap("NoControls");
        GlobalStuff.instance.PlayerCam.gameObject.SetActive(false);
        GlobalStuff.instance.ShiftCam.gameObject.SetActive(true); 
        StartCoroutine(Shift());
        }
    }

    IEnumerator Shift()
    {
        cooldownDone = false;
        UsableText = "";

        if (!GlobalStuff.instance.InHell)
        {
            RenderSettings.fogColor = Color.red;
            GlobalStuff.instance.InHell = true;
        }
        else
        {
            RenderSettings.fogColor = Color.gray;
            GlobalStuff.instance.InHell = false;
        }

        GlobalStuff.instance.PlayHellSounds();

        yield return new WaitForSecondsRealtime(1f);
        
        Realm1.SetActive(!Realm1.activeInHierarchy);
        Realm2.SetActive(!Realm2.activeInHierarchy);

        MasterAudio.PlaySoundAndForget("dimensionFlipWhoosh");
        
        GlobalStuff.instance.TurnPlayer();
        base.Back();

        yield return new WaitForSecondsRealtime (2f);
        GlobalStuff.instance.PlayerCam.gameObject.SetActive(true);
        GlobalStuff.instance.ShiftCam.gameObject.SetActive(false);
        cooldownDone = true;
        UsableText = "(LMB) Shift Realm";
    }

}
