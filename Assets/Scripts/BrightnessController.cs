using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;
    // Start is called before the first frame update
    void Start()
    {
        brightness.TryGetSettings(out exposure);
        brightnessSlider.value = exposure.keyValue;
        AdjustBrightness(brightnessSlider.value);
    }

    // Update is called once per frame
    public void AdjustBrightness(float brightness)
    {
        if (brightness != 0)
        {
            exposure.keyValue.value = brightness;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }
    }
}
