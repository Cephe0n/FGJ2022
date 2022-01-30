using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class RealmShift : Usable
{
    public GameObject Realm1, Realm2;

    bool cooldownDone = true;
    bool FirstTimeShift = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Shift Realm";
    }

    public override void Use()
    {
        if (FirstTimeShift)
        {    
        StartCoroutine(FirstShift());
        }
        else if (cooldownDone)
        {
        //base.MoveCamera();
        StartCoroutine(Shift());
        }
        
    }

    IEnumerator FirstShift()
    {
        input.SwitchCurrentActionMap("NoControls");
        UsableText = "";
        GlobalStuff.instance.SubtitleText.text = "What is this thing?";
        MasterAudio.PlaySoundAndForget("whatisthisthing");
        yield return new WaitForSecondsRealtime(2.5f);
        MasterAudio.PlaySoundAndForget("feelslikeitscalling");
        GlobalStuff.instance.SubtitleText.text = "Feels like it's... calling to me...";
        yield return new WaitForSecondsRealtime(2.5f);
        GlobalStuff.instance.SubtitleText.text = "";
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {
        cooldownDone = false;
        UsableText = "";
        GlobalStuff.instance.ShiftCount();
        input.SwitchCurrentActionMap("NoControls");
        GlobalStuff.instance.PlayerCam.gameObject.SetActive(false);
        closeCam.gameObject.SetActive(false);
        GlobalStuff.instance.ShiftCam.gameObject.SetActive(true); 

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

        if (FirstTimeShift)
        {
         FirstTimeShift = false;
         GlobalStuff.instance.SubtitleText.text = "What the hell?";
         MasterAudio.PlaySoundAndForget("whatthehell");
         yield return new WaitForSecondsRealtime (2.5f);
         GlobalStuff.instance.SubtitleText.text = "";
        }
    }

}
