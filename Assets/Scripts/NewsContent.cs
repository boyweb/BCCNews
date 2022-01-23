using System;
using UnityEngine;
using UnityEngine.UI;
public class NewsContent : MonoBehaviour
{
    public static NewsContent Instance;

    [SerializeField] private Text title;
    [SerializeField] private Text content;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(string t, string c)
    {
        title.text = t;
        content.text = c;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}