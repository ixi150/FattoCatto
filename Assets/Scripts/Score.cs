using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static Score score;
    int cans = 0;
    Text text;

    private void Awake()
    {
        score = this;
        text = GetComponent<Text>();
        Refresh();
    }

    public static void CanKilled()
    {
        score.CanKilledLocal();
    }

    void CanKilledLocal()
    {
        cans++;
        Refresh();
    }

    void Refresh()
    {
        text.text = cans.ToString();
    }
}
