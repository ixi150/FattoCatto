using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLines : MonoBehaviour
{
    static GameLines gameLines;

    private void Awake()
    {
        gameLines = this;
    }

    public static void SetGameTransformLine(GameTransform gameTransform, int lineIndex)
    {
        if (gameLines.transform.childCount > lineIndex && lineIndex >= 0)
            gameTransform.transform.SetParent(gameLines.transform.GetChild(lineIndex));
    }

    public static void MoveGameTransformUp(GameTransform gameTransform)
    {
        AddGameTransformLineIndex(gameTransform, -1);
    }

    public static void MoveGameTransformDown(GameTransform gameTransform)
    {
        AddGameTransformLineIndex(gameTransform, +1);
    }

    public static int GetLineIndex(GameTransform gameTransform)
    {
        return gameTransform.transform.parent.GetSiblingIndex();
    }

    static void AddGameTransformLineIndex(GameTransform gameTransform, int lineIndexChange)
    {
        var lineIndex = GetLineIndex(gameTransform);
        SetGameTransformLine(gameTransform, lineIndex + lineIndexChange);
    }

}
