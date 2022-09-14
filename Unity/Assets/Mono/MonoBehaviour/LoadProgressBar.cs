using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LoadProgressBar: MonoBehaviour
{
    // [NonSerialized]
    [Range(0, 1)]
    public float _progress = 0;

    public Image Bar;
    public Image Bg;

    public float Progress
    {
        get
        {
            return this._progress;
        }
        set
        {
            this._progress = value;
        }
    }

    private void Update()
    {
        this.Bar.GetComponent<RectTransform>()
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._progress * this.Bg.GetComponent<RectTransform>().rect.width);
    }
}