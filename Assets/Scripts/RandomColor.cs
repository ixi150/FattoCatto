using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomColor : MonoBehaviour
{
    public Gradient gradient;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().color = gradient.Evaluate(Random.Range(0f, 1f));
        Destroy(this);
    }
}
