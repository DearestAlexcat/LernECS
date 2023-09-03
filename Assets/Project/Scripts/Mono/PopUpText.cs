using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PopUpText : MonoBehaviour
{
    private Text textUP;

    public string MessasgeText { set => textUP.text = value; }

    public Color Color { set => textUP.color = value; }

    public float durationUP;
    public float durationFade;

    public float position;

    private void Awake()
    {
        textUP = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        transform.DOMove(transform.position + Vector3.up * position, durationUP).OnComplete(() => Destroy(gameObject));
        textUP.DOFade(0f, durationFade);
    }
}
