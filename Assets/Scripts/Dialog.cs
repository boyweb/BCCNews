using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Dialog : MonoBehaviour
{
    public static Dialog Instance;

    [SerializeField] private RectTransform rect;

    private void Awake()
    {
        Instance = this;
        rect.anchoredPosition = new Vector2(0, -400);
    }

    [SerializeField] private Text content;

    private IEnumerator DialogProgress(List<string> contents, Action over)
    {
        foreach (var text in contents)
        {
            content.text = text;
            while (true)
            {
                yield return null;
                if (Input.GetMouseButtonDown(0))
                {
                    break;
                }
            }
        }

        rect.DOAnchorPosY(-400, 0.1f);
        over();
    }

    public void StartDialog(List<string> contents, Action over)
    {
        content.text = "";
        Reporter.Instance.CanMove = false;
        rect.DOAnchorPosY(40, 0.1f);
        StartCoroutine(DialogProgress(contents, over));
    }
}