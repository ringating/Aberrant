using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    public static InteractText instance;

    [HideInInspector] public TextMeshProUGUI tmp;

    private void Awake()
    {
        // get component
        tmp = GetComponent<TextMeshProUGUI>();

        // singleton stuff
        if (instance)
        {
            Debug.LogError("An instance of InteractText already exists! Destroying this instance!");
            Destroy(this);
            return;
        }
        instance = this;

        // set text to nothing
        tmp.text = "";
    }
}
