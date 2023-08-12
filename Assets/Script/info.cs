using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info : MonoBehaviour
{
    public static info instance;

    public Text infoText;

    void Awake()
    {
        instance = this;
        infoText.text = "";
    }

    public void showMessage(string text)
    {
        infoText.text = text;
    }

}
