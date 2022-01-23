using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Announcement : MonoBehaviour
{
    public static Announcement Instance;

    [SerializeField] private RectTransform rect;
    [SerializeField] private Text title;
    [SerializeField] private Text content;

    private void Awake()
    {
        Instance = this;
        rect.anchoredPosition = new Vector2(0, -1080);
    }

    private Action _action;
    public const string NoBreakingSpace = "\u00A0";

    public void Show(string t, string c, Action action = null)
    {
        _action = action;
        title.text = t;
        content.text = c.Replace(" ", NoBreakingSpace);
        Sound.Instance.Announcement();
        rect.DOAnchorPosY(0, 0.1f);
    }

    public void Hide()
    {
        _action?.Invoke();
        rect.DOAnchorPosY(-1080, 0.1f);
    }
}