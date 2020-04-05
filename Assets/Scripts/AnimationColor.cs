using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimationColor : MonoBehaviour
{
    public float duration;
    public Gradient gradient;

    private void Awake()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float time = Time.time;
        while (true)
        {
            float timer = Time.time - time;
            renderer.color = gradient.Evaluate(timer / duration);
            if (timer >= duration) break;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this);
    }
}
