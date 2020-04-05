using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    
	void Update ()
    {
		if (!FattoCatto.IsCattoAlive())
        {
            GetComponent<AudioSource>().Stop();
            enabled = false;
        }
	}
}
