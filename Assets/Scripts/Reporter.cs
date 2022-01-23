using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reporter : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;

    private Vector2 _direction = Vector2.zero;

    public static Reporter Instance;
    private static readonly int Speed = Animator.StringToHash("Speed");

    public bool CanMove { get; set; } = true;

    private void Awake()
    {
        Instance = this;
        Physics.queriesHitTriggers = true;
    }

    private IEnumerator StartProcess()
    {
        Cover.Instance.Hide();
        yield return new WaitForSeconds(1);
        Announcement.Instance.Show(
            "主编的信息",
            "我们收到了你的入职申请，但是BCC News是全世界新闻人都想来的地方。\n想要加入BCC，你必须证明你自己。\n现在有2个任务交给你：发表一篇有关强制劳动的新闻报道，以及一篇打击当地民心的新闻报道。\n我们讲究真实，所以你的所有素材来源必须来自于实地采访，这样才有可信度。\n至于如何用真实的素材写出我们需要的新闻，你需要好好思考。\n如果你能完成任务，那么你就掌握了BCC记者的基本技能。\n祝你成功！",
            () => { CanMove = true; });
    }

    private void Start()
    {
        CanMove = false;
        StartCoroutine(StartProcess());
    }

    private void CheckKey(KeyCode keyCode, Vector2 direction)
    {
        if (Input.GetKey(keyCode))
        {
            _direction += direction;
        }
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            _direction = Vector2.zero;
            CheckKey(KeyCode.W, Vector2.up);
            CheckKey(KeyCode.A, Vector2.left);
            CheckKey(KeyCode.S, Vector2.down);
            CheckKey(KeyCode.D, Vector2.right);

            body.MovePosition(body.position + _direction.normalized * speed * Time.fixedDeltaTime);

            animator.SetFloat(Speed, _direction.magnitude);
        }
    }

    [SerializeField] private GameObject policeCar1;
    [SerializeField] private GameObject policeCar2;

    private IEnumerator PoliceCar1Process()
    {
        Report.Instance.Hide();
        CanMove = false;
        FollowingCamera.Instance.SetTarget(policeCar1.transform);
        yield return new WaitForSeconds(0.5f);
        Sound.Instance.PoliceCar();
        yield return policeCar1.transform.DOMoveX(0, 2f).SetUpdate(UpdateType.Fixed).WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        Manager.Instance.NPCs[1].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        yield return policeCar1.transform.DOMoveX(-30, 2f).SetUpdate(UpdateType.Fixed).SetEase(Ease.InQuad).WaitForCompletion();
        FollowingCamera.Instance.SetTarget(transform);
        CanMove = true;
    }

    public void PoliceCar1()
    {
        StartCoroutine(PoliceCar1Process());
    }

    private IEnumerator PoliceCar2Process()
    {
        Report.Instance.Hide();
        CanMove = false;
        FollowingCamera.Instance.SetTarget(policeCar2.transform);
        yield return new WaitForSeconds(0.5f);
        Sound.Instance.PoliceCar();
        yield return policeCar2.transform.DOMoveX(4.5f, 2f).SetUpdate(UpdateType.Fixed).WaitForCompletion();
        yield return new WaitForSeconds(0.5f);
        Manager.Instance.NPCs[11].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        yield return policeCar2.transform.DOMoveX(-30, 2f).SetUpdate(UpdateType.Fixed).SetEase(Ease.InQuad).WaitForCompletion();
        FollowingCamera.Instance.SetTarget(transform);
        CanMove = true;
    }

    public void PoliceCar2()
    {
        StartCoroutine(PoliceCar2Process());
    }

    [SerializeField] private Text win;

    private IEnumerator WinProcess()
    {
        Report.Instance.Hide();
        CanMove = false;
        Cover.Instance.Show();
        yield return new WaitForSeconds(1);
        Sound.Instance.Victory();
        yield return win.DOFade(1, 1).WaitForCompletion();
        yield return new WaitForSeconds(3);
        yield return win.DOFade(0, 1).WaitForCompletion();
        SceneManager.LoadScene("Logo");
    }

    public void Win()
    {
        StartCoroutine(WinProcess());
    }
}