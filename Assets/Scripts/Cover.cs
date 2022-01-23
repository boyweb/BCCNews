using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Cover : MonoBehaviour
{
    public static Cover Instance;

    [SerializeField] private Image cover;

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        cover.DOFade(1, 0.5f);
    }

    public void Hide()
    {
        cover.DOFade(0, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
    }
}