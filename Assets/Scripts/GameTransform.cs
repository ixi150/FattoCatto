using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameTransform : MonoBehaviour
{
    public bool facingRight = true;
    public float x;

    public void MoveUp()
    {
        GameLines.MoveGameTransformUp(this);
        RefreshTransform();
    }

    public void MoveDown()
    {
        GameLines.MoveGameTransformDown(this);
        RefreshTransform();
    }

    public int GetLineIndex()
    {
        return GameLines.GetLineIndex(this);
    }

    public void RefreshTransform()
    {
        //var newPosition = Vector3.zero;
        //newPosition.x = transform.localPosition.x;
        transform.localPosition = new Vector3(facingRight ? -x : x, 0, 0);
        transform.localRotation = facingRight ? rightRotation : leftRotation;
        transform.localScale = Vector3.one;
    }

    readonly static Quaternion leftRotation = Quaternion.Euler(0, 180, 0);
    readonly static Quaternion rightRotation = Quaternion.Euler(0, 0, 0);

    void LateUpdate()
    {
        RefreshTransform();
    }


}
