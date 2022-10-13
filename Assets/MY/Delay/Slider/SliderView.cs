using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderView : MonoBehaviour
{
    [SerializeField] Image handle;
    private static readonly Color red = Color.red;
    private static readonly Color white = Color.white;

    public void SetColor(bool on){
        if(on)  handle.color = red;
        else handle.color = white;
    }
}
