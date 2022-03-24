using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingNPC : MonoBehaviour
{
    public bool speaking = true;
    public bool offeringAccepted = false;

    private void Awake()
    {
        GetComponentInChildren<Canvas>().worldCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey("e") && !speaking && !offeringAccepted)
        {
            offeringAccepted = true;
            // Put other offering functionality here or in the Farewell() method in the OfferingDialogueManager.cs script.
            GetComponentInParent<OfferingDialogueManager>().Farewell();
        }
    }
}
