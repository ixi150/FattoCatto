using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMessage : MonoBehaviour
{

    static EndMessage endMessage;
    Text text;

    private void Awake()
    {
        endMessage = this;
        text = GetComponent<Text>();
        text.text = "";
    }

    public static void ShowMessage(string message)
    {
        endMessage.enabled = true;
        endMessage.text.text = message;
    }

}
