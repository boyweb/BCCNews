using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewsSettings
{
    public NewsSettings(int all, string title, List<Tuple<int, string>> content, Action action)
    {
        All = all;
        Title = title;
        Content = content;
        Action = action;
    }

    public int All { get; }
    public string Title { get; }
    public List<Tuple<int, string>> Content { get; }
    public Action Action { get; }
}

public class News : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static readonly List<NewsSettings> NewsSettingsList = new List<NewsSettings>
    {
        new NewsSettings(
            5,
            "虐狗者",
            new List<Tuple<int, string>>
            {
                new Tuple<int, string>(2, "他，是一位善良的爱狗人士。"),
                new Tuple<int, string>(5, "它，是一只听话的宠物伙伴。"),
                new Tuple<int, string>(4, "然而厄运降临，它被工厂门卫狠狠殴打。"),
                new Tuple<int, string>(3, "附近的居民都能听到狗狗的惨叫。"),
                new Tuple<int, string>(1, "而试图寻找真相的记者则遭到了门卫的无情驱逐。"),
                new Tuple<int, string>(-1, "我们必须发声！虐待动物的人必须被抵制！"),
            },
            () => {
                Sound.Instance.Speak();
                Factory.Instance.Show(
                    "工厂通报",
                    "我们有注意到最近的言论，并进行了彻底筛查。\n对于已经发生的事情，我们表示痛心，涉事者是一位临时担任门卫的编外人员。\n目前，公司已经与其解除了劳动合同。\n我们将严格自查，防止类似事情的发生。\n谢谢大家的监督！",
                    () => { Reporter.Instance.PoliceCar1(); }
                );
            }
        ),
        new NewsSettings(
            7,
            "恶霸的靠山",
            new List<Tuple<int, string>>
            {
                new Tuple<int, string>(2, "一位可怜的流浪汉被当街咬伤，狗主人为何如此胆大妄为？"),
                new Tuple<int, string>(5, "不仅如此，这位主人遛狗从不牵绳，视公共安全如无物。"),
                new Tuple<int, string>(6, "那只咬人的恶狗，如今依然悠然游荡在街上"),
                new Tuple<int, string>(3, "然而即便是这样的恶犬，依然逃不过主人的毒打。"),
                new Tuple<int, string>(12, "记者调查发现，这位著名的恶霸，原来竟是工厂管理人员的小舅子。"),
                new Tuple<int, string>(13, "这位管理人员还曾经保护过那名恶霸。"),
                new Tuple<int, string>(10, "当记者进一步调查时，该管理人员却以涉密为由强行命令记者离开。"),
                new Tuple<int, string>(-1, "毫无疑问，这种黑恶势力的靠山，是人民的敌人，我们绝不容忍！"),
            },
            () => {
                Sound.Instance.Speak();
                Factory.Instance.Show(
                    "工厂通报",
                    "对于近期传言本厂一名管理人员涉黑事宜，我们在此号召大家不信谣、不传谣！\n秉着公平公正的原则，我们已经向公安机关报案，请大家耐心等待官方的调查结果。\n对于涉事的管理人员，我们也已经暂时解除其职务，并在调查完毕之前不再参与工厂管理。\n对于有关谣言的制造者与传播者，我们将保留起诉的权利。",
                    () => { Reporter.Instance.PoliceCar2(); }
                );
            }
        ),
        new NewsSettings(
            10,
            "集中营",
            new List<Tuple<int, string>>
            {
                new Tuple<int, string>(1, "是什么样的神秘场所，让保卫人员积极地驱赶寻求真相的记者？"),
                new Tuple<int, string>(4, "附近的流浪汉证实，那些守卫都武装到了牙齿，配备着攻击性武器。"),
                new Tuple<int, string>(6, "记者在门口还碰到了一条恶犬，不让任何人靠近。"),
                new Tuple<int, string>(7, "辛苦潜入此地后，本报记者首先看到的是脏乱的环境。"),
                new Tuple<int, string>(8, "而一位在此劳动的人员，在记者面前用“奴隶”字眼称呼自己。"),
                new Tuple<int, string>(9, "这里有着严苛的制度，不准时出席活动的人会受到制裁。"),
                new Tuple<int, string>(15, "记者甚至找到了一张照片，上面是被绳索悬吊着的人影。"),
                new Tuple<int, string>(10, "记者想要采访这里的管理人员，他却只顾着保护机密文件，没有透露任何信息。"),
                new Tuple<int, string>(14, "还有一些不寻常的东西，但在记者进一步调查前有人赶来销毁了它们。"),
                new Tuple<int, string>(11, "一份可靠的内部资料显示，这处设施马上就要进行扩建。"),
                new Tuple<int, string>(-1, "综合以上证据，一个令人震惊的事实摆在世人眼前："),
                new Tuple<int, string>(-1, "在现代民主社会里，居然仍然存在着将人视作奴隶强迫其劳作的集中营！"),
                new Tuple<int, string>(-1, "这无疑是对现代人权的践踏，本报记者将持续关注报道。"),
            },
            () => {
                Sound.Instance.Speak();
                Manager.Instance.Victory++;
                Announcement.Instance.Show(
                    "主编的信息",
                    Manager.VictoryContent[Manager.Instance.Victory - 1]
                );
            }
        ),
        new NewsSettings(
            8,
            "黑心工厂",
            new List<Tuple<int, string>>
            {
                new Tuple<int, string>(7, "今天，记者来到一座驰名工厂，然而迎接他的却是清洁工人的满面愁容。"),
                new Tuple<int, string>(8, "这里的工人很坚强，尽管工作强度很大，但他们依然坚持微笑面对工作。"),
                new Tuple<int, string>(9, "然而还是有人无法领到完整的薪水，他前去办公室抗议，不出意外地没有效果。"),
                new Tuple<int, string>(13, "曾经有工人感到不满，然而在管理人员的压力面前他也只能忍气吞声。"),
                new Tuple<int, string>(12, "记者无奈离去，无意间发现管理者身边全是年轻靓丽的女性。"),
                new Tuple<int, string>(15, "这里的安全生产告示牌前空空荡荡，无人驻足观看。"),
                new Tuple<int, string>(14, "在工厂里面，记者发现了大量的残次产品。"),
                new Tuple<int, string>(11, "进过深入调查，记者发现这座名声在外的工厂实际上早已身负巨债。"),
                new Tuple<int, string>(-1, "这样金玉其外、败絮其中的工厂真的是我们需要的吗？"),
                new Tuple<int, string>(-1, "与其持续兴建这样的工厂，也许当地政府更应该停下角度，等一等自己的人民。"),
            },
            () => {
                Sound.Instance.Speak();
                Manager.Instance.Victory++;
                Announcement.Instance.Show(
                    "主编的信息",
                    Manager.VictoryContent[Manager.Instance.Victory - 1],
                    () => { Reporter.Instance.Win(); }
                );
            }
        ),
    };

    [SerializeField] private Text bottomText;
    [SerializeField] private Text topText;
    [SerializeField] private RectMask2D rectMask2D;
    [SerializeField] private Image highLight;
    [SerializeField] private Image published;

    private int _all;
    private readonly List<int> _current = new List<int>();

    private int _id;
    private string _title;
    private List<Tuple<int, string>> _content;
    private Action _publishAction;

    private bool _published;

    private void Start()
    {
        var match = Regex.Match(name, "\\d+");
        _id = int.Parse(match.Value);
        Manager.Instance.News[_id] = this;

        var settings = NewsSettingsList[_id - 1];
        Init(settings.Title, settings.Content, settings.All, settings.Action);
    }

    public void Init(string title, List<Tuple<int, string>> content, int all, Action publishAction)
    {
        _title = title;
        _content = content;
        _publishAction = publishAction;

        bottomText.text = title;
        topText.text = title;

        _all = all;
    }

    private void CheckDone()
    {
        if (_current.Count != _all) return;

        _published = true;
        prePublish.gameObject.SetActive(false);
        Manager.Instance.CardLock = true;

        NewsContent.Instance.Hide();
        published.DOFade(1, 0.3f);
        published.rectTransform.DOScale(Vector3.one, 0.3f);
        published.rectTransform.DOLocalRotate(Vector3.zero, 0.3f).OnComplete(() => {
            foreach (var i in _current)
            {
                Manager.Instance.Cards[i].Used();
            }
            Manager.Instance.CardLock = false;
            _publishAction();
        });
    }

    [SerializeField] private GameObject prePublish;

    public void Add(int id)
    {
        _current.Add(id);
        rectMask2D.DOKill();
        rectMask2D.DoBottom(300 * (1 - _current.Count / (float)_all), 0.2f);
        if (_current.Count == _all)
        {
            prePublish.SetActive(true);
        }
    }

    public void Remove(int id)
    {
        _current.Remove(id);
        rectMask2D.DOKill();
        rectMask2D.DoBottom(300 * (1 - _current.Count / (float)_all), 0.2f);
        prePublish.SetActive(false);
    }

    public void ShowHighLight()
    {
        highLight.DOKill();
        highLight.DOFade(1, 0.1f);
    }

    public void HideHighLight()
    {
        highLight.DOKill();
        highLight.DOFade(0, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_published) return;
        CheckDone();
    }

    private static char GetRandomCharacter(string text)
    {
        var index = UnityEngine.Random.Range(0, text.Length);
        return text[index];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_current.Count == _all) ShowHighLight();

        var contents = new List<string>();
        foreach (var (item1, item2) in _content)
        {
            if (item1 == -1 || _current.Contains(item1))
            {
                contents.Add(item2);
            }
            else
            {
                var content = "";
                for (var i = 0; i < item2.Length; i++)
                {
                    content += GetRandomCharacter("！＠＃￥％＆＊");
                }
                contents.Add(content);
            }
        }

        NewsContent.Instance.Show(_title, string.Join("\n", contents));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideHighLight();
        NewsContent.Instance.Hide();
    }
}