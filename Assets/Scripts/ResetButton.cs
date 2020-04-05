﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetScene();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
}
