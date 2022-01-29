using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using DarkTonic.MasterAudio;
using DG.Tweening;

public class GlobalStuff : MonoBehaviour
{
    public static GlobalStuff instance;
    int solvedPuzzles;
    PlayerInput input;
    public GameObject FrontDoor, OpenDoor, Laptop, Player;
    public GameObject[] ExitLights;
    public Material GreenLight;
    public CinemachineVirtualCamera FadeCam, PlayerCam, ShiftCam;

    public bool InHell;
    // Start is called before the first frame update

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
        yield return new WaitForSecondsRealtime(2f);
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

        if (solvedPuzzles >= 4)
            OpenExit();
    }

    public void PlayHellSounds()
    {
        if (InHell)
        {

            if (!MasterAudio.IsSoundGroupPlaying("morsecode"))
                MasterAudio.PlaySound3DAtTransformAndForget("morsecode", Laptop.transform);

            MasterAudio.TriggerPlaylistClip("Nightmare_World");
            MasterAudio.PlaySoundAndForget("dimensionFlipReverse");
        }
        else
        {
            MasterAudio.TriggerPlaylistClip("Real_World");
            MasterAudio.PlaySoundAndForget("dimensionFlipNormal");
            MasterAudio.StopAllOfSound("morsecode");
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
        MasterAudio.StopAllPlaylists();
        FadeCam.gameObject.SetActive(true);
        PlayerCam.gameObject.SetActive(false);
        input.SwitchCurrentActionMap("NoControls");
    }

}
