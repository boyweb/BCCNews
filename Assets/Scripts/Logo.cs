using System;
using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Logo : MonoBehaviour
{
    [SerializeField] private RectTransform b;
    [SerializeField] private RectTransform c1;
    [SerializeField] private RectTransform c2;
    [SerializeField] private RectTransform n;
    [SerializeField] private RectTransform e;
    [SerializeField] private RectTransform w;
    [SerializeField] private RectTransform s;

    [SerializeField] private RectMask2D mask;

    [SerializeField] private CanvasGroup buttons;

    private void Start()
    {
        StartCoroutine(LogoProcess());
    }

    private IEnumerator LogoProcess()
    {
        Cover.Instance.Hide();
        yield return new WaitForSeconds(1);

        b.DOAnchorPosY(-1000, 1);
        yield return new WaitForSeconds(0.2f);
        c1.DOAnchorPosY(-1000, 1);
        yield return new WaitForSeconds(0.2f);
        c2.DOAnchorPosY(-1000, 1);

        yield return new WaitForSeconds(1f);
        mask.padding += new Vector4(0, 350, 0, 300);
        b.localScale = new Vector3(2, 2, 1);
        b.anchoredPosition = new Vector2(-250, 500);
        c1.localScale = new Vector3(2, 2, 1);
        c1.anchoredPosition = new Vector2(0, 500);
        c2.localScale = new Vector3(2, 2, 1);
        c2.anchoredPosition = new Vector2(250, 500);
        yield return new WaitForSeconds(0.5f);

        n.gameObject.SetActive(true);
        e.gameObject.SetActive(true);
        w.gameObject.SetActive(true);
        s.gameObject.SetActive(true);

        n.DOAnchorPosY(-100, 1);
        yield return new WaitForSeconds(0.1f);
        b.DOAnchorPosY(100, 1);
        yield return new WaitForSeconds(0.1f);
        e.DOAnchorPosY(-100, 1);
        yield return new WaitForSeconds(0.1f);
        c1.DOAnchorPosY(100, 1);
        yield return new WaitForSeconds(0.1f);
        w.DOAnchorPosY(-100, 1);
        yield return new WaitForSeconds(0.1f);
        c2.DOAnchorPosY(100, 1);
        yield return new WaitForSeconds(0.1f);
        s.DOAnchorPosY(-100, 1);

        yield return new WaitForSeconds(1f);

        Music.Instance.PlayMusic();
        buttons.gameObject.SetActive(true);
        buttons.DOFade(1, 0.5f);
    }

    private bool _lock;

    private IEnumerator StartProcess()
    {
        Cover.Instance.Show();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }

    public void StartGame()
    {
        if (_lock) return;
        _lock = true;
        StartCoroutine(StartProcess());
    }

    [SerializeField] private CanvasGroup about;

    public void About()
    {
        if (_lock) return;
        _lock = true;
        about.gameObject.SetActive(true);
        about.DOFade(1, 0.1f);
        buttons.DOFade(0, 0.1f);
    }

    public void CloseAbout()
    {
        buttons.DOFade(1, 0.1f);
        about.DOFade(0, 0.1f).OnComplete(() => {
            about.gameObject.SetActive(false);
            _lock = false;
        });
    }

    public void QuitGame()
    {
        if (_lock) return;
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}