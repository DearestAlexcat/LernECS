using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{

    public Button attackButton;     // not active
    public Button clearButton;
    public RectTransform HolderUnits;
    public ScrollRect MessageScrollRect;

    public SliderHP PlayerSlider;
    public Text PlayerNameText;

    public WinWindow WinWindow;
    public DieWindow DieWindow;
}
