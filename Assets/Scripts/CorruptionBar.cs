using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionBar : MonoBehaviour
{
    public static CorruptionBar Instance;
    public Slider slider;

    private void Awake()
    {
        Instance = this;
    }
    public void setSliderMaxValue(int value)
    {
        slider.maxValue = value;
    }

    public void setSliderValue(int value)
    {
        slider.value = value;
    }
}
