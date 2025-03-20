using UnityEngine;
using UnityEngine.UI;

public class SliderColour : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public Gradient colorGradient;

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateFillColor);
        UpdateFillColor(slider.value);
    }

    void UpdateFillColor(float value)
    {
        fillImage.color = colorGradient.Evaluate(value / slider.maxValue);
    }
}
