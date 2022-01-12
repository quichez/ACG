using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour, IGridObject, IHighlightWithinRange
{
    Color _Unselected;
    public Color _Selected;

    public SpriteRenderer Sprite;

    public Color Unselected => _Unselected;

    public Color Selected => _Selected;

    public void Highlight()
    {
        Debug.Log("highlight called");
        Sprite.color = Selected;
    }

    public void UnHighlight()
    {
        Sprite.color = Unselected;
    }

    private void Start()
    {
        _Unselected = Sprite.color;
    }
}
