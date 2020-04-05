using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class SettingSortingLayerToChildren : MonoBehaviour
{
    SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.sortingLayerID = spriteRenderer.sortingLayerID;
        }
    }
}
