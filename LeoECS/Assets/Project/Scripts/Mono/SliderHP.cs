using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    private Slider sliderHP;

    private Text sliderText;

    public float UnitHPUI
    {
        set => sliderHP.value = value;
    }

    private void Awake()
    {
        sliderHP = GetComponentInChildren<Slider>();
        sliderText = GetComponentInChildren<Text>();
    }

    public void UpdateSliderText(float currnet, float max)
    {
        sliderText.text = currnet + "/" + max;
        sliderHP.value = currnet / max;
    }
}
