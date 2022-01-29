using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealmShift : Usable
{
    public GameObject Realm1, Realm2;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UsableText = "(LMB) Shift Realm";
    }

    public override void Use()
    {
        Realm1.SetActive(!Realm1.activeInHierarchy);
        Realm2.SetActive(!Realm2.activeInHierarchy);
    }
}
