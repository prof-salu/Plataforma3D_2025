using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public CanvasGroup canvas;

    public void Pausar()
    {
        //Fade out
        LeanTween.alphaCanvas(canvas, 1, .2f).setIgnoreTimeScale(true);
    }

    public void Resume()
    {
        //Fade in
        LeanTween.alphaCanvas(canvas, 0, .2f).setIgnoreTimeScale(true);
    }
}
