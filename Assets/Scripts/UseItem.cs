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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            NearUsable = false;
            ItemToUse = null;
            HelpText.text = "";
        }
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
        usableScript.Back();
        HelpText.text = usableScript.UsableText;
        ControlHintText.text = "";
    }

    void OnSubmit()
    {
        usableScript.Submit();
    }
}
