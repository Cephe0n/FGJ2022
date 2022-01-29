using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DarkTonic.MasterAudio;

public class UsePwThing : Usable
{
    public TMP_InputField field;
    public TMP_Text EnterPwText;
    public GameObject WrongLight, Correctlight;
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
            base.MoveCamera();
            field.ActivateInputField();
        }
    }

    public override void Submit()
    {
        string givenPw = field.text;

        if (givenPw.ToLower() == correctPw)
        Success();
        else
        StartCoroutine(ShowWrongLight());
    }

    void Success()
    {
        StopCoroutine(ShowWrongLight());
        MasterAudio.PlaySound3DAtTransformAndForget("passwordCorrect2", this.transform);
        EnterPwText.text = "ACCESS GRANTED";
        field.text = "";
        ControlsText = "";
        WrongLight.SetActive(false);
        Correctlight.SetActive(true);
        this.gameObject.layer = 0;
        field.gameObject.SetActive(false);
        base.Back();
        GlobalStuff.instance.PuzzleSolved();
    }

    public override void Back()
    {
        if (inUse)
        {
            UsableText = "(LMB) Use";
            base.Back();
        }
    }

    IEnumerator ShowWrongLight()
    {
        WrongLight.SetActive(true);
        MasterAudio.PlaySound3DAtTransformAndForget("passwordWrong", this.transform);
        EnterPwText.text = "ACCESS DENIED";
        field.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        WrongLight.SetActive(false);
        EnterPwText.text = "ENTER PASSWORD";
    }
}
