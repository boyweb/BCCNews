using System;
using DG.Tweening;
using UnityEngine;
public class Report : MonoBehaviour
{
    public static Report Instance;

    private void Awake()
    {
        Instance = this;
        rect.anchoredPosition = new Vector2(0, -1080);
    }

    [SerializeField] private RectTransform button;

    public void Shake()
    {
        button.DOShakeScale(0.3f, new Vector3(0.25f, 0.25f));
    }

    [SerializeField] private RectTransform rect;

    public void Show()
    {
        if (!Reporter.Instance.CanMove) return;
        Reporter.Instance.CanMove = false;
        rect.DOAnchorPosY(0, 0.1f);
    }

    public void Hide()
    {
        Reporter.Instance.CanMove = true;
        rect.DOAnchorPosY(-1080, 0.1f);
    }
}