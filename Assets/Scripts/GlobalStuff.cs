using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using DarkTonic.MasterAudio;
using DG.Tweening;
using TMPro;

public class GlobalStuff : MonoBehaviour
{
    public static GlobalStuff instance;
    int solvedPuzzles, timesShifted, hellArraycount, realArrayCount;
    PlayerInput input;
    public GameObject FrontDoor, OpenDoor, Laptop, Player, Phone;
    public GameObject[] ExitLights, HellEntities, RealWorldEntities;
    public Material GreenLight;
    public CinemachineVirtualCamera FadeCam, PlayerCam, ShiftCam;
    public TMP_Text SubtitleText;
    public bool InHell;
    [HideInInspector]
    public bool PhoneEventStarted, PhoneEventDone;
    

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
    }

    IEnumerator FadeIn()
    {
        SubtitleText.text = "Agh... my head...";
        MasterAudio.PlaySoundAndForget("myhead");
        yield return new WaitForSecondsRealtime(2.5f);
        SubtitleText.text = "What happened?";
        MasterAudio.PlaySoundAndForget("whathappened");
        yield return new WaitForSecondsRealtime(2f);
        MasterAudio.PlaySoundAndForget("whereami");
        SubtitleText.text = "Where am I?";
        yield return new WaitForSecondsRealtime(2f);
        SubtitleText.text = "";
        FadeCam.gameObject.SetActive(false);
        PlayerCam.gameObject.SetActive(true);
        input.SwitchCurrentActionMap("Player");
    }

    public void TurnPlayer()
    {
        Vector3 rot = new Vector3(0, 180, 0);

        Player.transform.Rotate(rot);
    }

    public void PuzzleSolved()
    {
        solvedPuzzles++;
        ExitLights[solvedPuzzles - 1].GetComponent<MeshRenderer>().material = GreenLight;

        if (solvedPuzzles == 1)
        PhoneEventStarted = true;

        if (solvedPuzzles >= 4)
            OpenExit();
    }

    public void TogglePhoneEvent()
    {
        Collider col = Phone.GetComponent<BoxCollider>();
        col.enabled = !col.enabled;
    }

    public void PlayHellSounds()
    {
        if (InHell)
        {

            if (!MasterAudio.IsSoundGroupPlaying("morsecode"))
                MasterAudio.PlaySound3DAtTransformAndForget("morsecode", Laptop.transform);

            MasterAudio.TriggerPlaylistClip("Nightmare_World");
            MasterAudio.PlaySoundAndForget("dimensionFlipReverse");

            if (PhoneEventStarted && !PhoneEventDone)
            {
            TogglePhoneEvent();
            MasterAudio.PlaySound3DAtTransformAndForget("phone_ring", Phone.transform);
            }       
        }
        else
        {
            MasterAudio.TriggerPlaylistClip("Real_World");
            MasterAudio.PlaySoundAndForget("dimensionFlipNormal");
            MasterAudio.StopAllOfSound("morsecode");
            MasterAudio.StopAllOfSound("phone_ring");

            if (PhoneEventDone)
            TogglePhoneEvent();
        }


    }

    public void ShiftCount()
    {
        timesShifted++;

        if (hellArraycount < HellEntities.Length)
        {
            HellEntities[hellArraycount].SetActive(true);
            hellArraycount++;
        }


        if(timesShifted % 4 == 0 && realArrayCount < RealWorldEntities.Length)
        {
            RealWorldEntities[realArrayCount].SetActive(true);
            realArrayCount++;
        }


    }

    public void RollForAmbient()
    {

        int r = InHell ? Random.Range(1, 7) : Random.Range(1, 20);

        if (r <= 2)
            MasterAudio.PlaySoundAndForget("ambientVoices2");
    }

    void OpenExit()
    {
        OpenDoor.SetActive(true);
        FrontDoor.SetActive(false);
        MasterAudio.PlaySound3DAtTransformAndForget("doorOpen", OpenDoor.transform);
    }

    public void Escape()
    {
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
    
        PlayerCam.gameObject.SetActive(false);
        FadeCam.gameObject.SetActive(true);
        input.SwitchCurrentActionMap("NoControls");
        SubtitleText.text = "I'm finally free!";
        MasterAudio.PlaySoundAndForget("imfree");
        yield return new WaitForSecondsRealtime(4f);
        MasterAudio.PlaySoundAndForget("phone_ring");
        yield return new WaitForSecondsRealtime(4f);
        MasterAudio.StopAllPlaylists();
        Application.Quit();
    }


}
