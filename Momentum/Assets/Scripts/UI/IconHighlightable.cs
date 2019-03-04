using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconHighlightable : MonoBehaviour
{
    private Image image;
    private bool _highlight;

    public Sprite inactive, active;
    public Color inactiveColor, activeColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        highlight = false;
    }

    void SetActive()
    {
        image.sprite = active;
        image.color = activeColor;
    }

    void SetInactive()
    {
        image.sprite = inactive;
        image.color = inactiveColor;
    }

    public bool highlight
    {
        get
        {
            return _highlight;
        }
        set
        {
            _highlight = value;
            if (value)
                SetActive();
            else
                SetInactive();
        }
    }
}
