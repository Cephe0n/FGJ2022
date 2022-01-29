using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseItem : MonoBehaviour
{
    public bool NearUsable;
    public GameObject ItemToUse;
    public TMP_Text HelpText, ControlHintText;
    Usable usableScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            NearUsable = true;
            ItemToUse = other.gameObject;
            usableScript = ItemToUse.GetComponent<Usable>();
            HelpText.text = usableScript.UsableText;
        }
        else if (other.gameObject.layer == 7)
        GlobalStuff.instance.RollForAmbient();

        if (other.gameObject.CompareTag("Exit"))
        GlobalStuff.instance.Escape();
    }

    private void OnTriggerExit(Collider other)
    {
        
            NearUsable = false;
            ItemToUse = null;
            HelpText.text = "";
            ControlHintText.text = "";
        
    }

    void OnUse()
    {
        if (NearUsable)
        {
            usableScript.Use();
            HelpText.text = usableScript.UsableText;
            ControlHintText.text = usableScript.ControlsText;
        }

    }

    void OnBack()
    {
        if (ItemToUse != null)
        {
        ControlHintText.text = "";
        usableScript.Back();
        HelpText.text = usableScript.UsableText;
        }
    }

    void OnSubmit()
    {
        usableScript.Submit();
    }
}
