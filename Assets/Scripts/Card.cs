using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardSettings
{
    public CardSettings(string titleA, string titleB, string contentA, string contentB, int targetA, int targetB)
    {
        TitleA = titleA;
        TitleB = titleB;
        ContentA = contentA;
        ContentB = contentB;
        TargetA = targetA;
        TargetB = targetB;
    }
    public string TitleA { get; }
    public string TitleB { get; }
    public string ContentA { get; }
    public string ContentB { get; }
    public int TargetA { get; }
    public int TargetB { get; }
}

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private static readonly List<CardSettings> CardSettingsList = new List<CardSettings>
    {
        new CardSettings(
            "蛮横门卫",
            "秘密场所",
            "记者刚刚靠近，这位门卫便蛮横地驱逐记者离开，这让信息的获取变得艰难。",
            "这片建筑区并不容易寻找，门口的保卫不让我们的记者进入，仿佛在保卫着什么惊人的秘密。",
            1,
            3
        ),

        new CardSettings(
            "无法无天",
            "乐善好施",
            "据透露，这位凶名在外的恶霸曾经让自己驯养的大型犬当街扑咬一位可怜的流浪汉。",
            "这是一位善良的爱狗人士，他曾慷慨地支付一位流浪者一大笔钱。",
            2,
            1
        ),

        new CardSettings(
            "穷凶极恶",
            "可怜小狗",
            "即使是对自家忠心耿耿的宠物，他下起手来也毫不手软。",
            "这只可怜的狗曾被无情殴打，它的惨叫声甚至传遍了整条街区。",
            2,
            1
        ),

        new CardSettings(
            "动物虐待",
            "攻击性装备",
            "据可靠报道，这位工厂门卫曾经使用坚硬的木棍，无情的殴打一只狗。",
            "一位在附近的流浪汉告诉记者，这里的门卫配备有攻击性武器，他还曾亲眼看见门卫使用武器。",
            1,
            3
        ),

        new CardSettings(
            "好伙伴",
            "不负责任",
            "据狗狗的主人介绍，他家的狗一直都很听话，对人也很友善。",
            "这位狗的主人身上看不到责任心，他公开宣称自己不会牵绳遛狗，放任自己的大狗在街上恐吓行人。",
            1,
            2
        ),

        new CardSettings(
            "狗仗人势",
            "凶恶野兽",
            "这只狗对记者凶狠地吼叫，一边的主人也丝毫没有管理的意向。",
            "门口有一只凶恶的巨犬，记者还未靠近就表现出了强烈的攻击欲望，让人害怕不已。",
            2,
            3
        ),

        new CardSettings(
            "肮脏泥泞",
            "唉声叹气",
            "记者偷偷进入之后，映入眼帘的是一片倒满垃圾的肮脏土地。",
            "一见到我们的记者，这位环卫工人就开始唉声叹气，他好像在这里过的并不开心。",
            3,
            4
        ),

        new CardSettings(
            "奴隶劳工",
            "强颜欢笑",
            "这位工人用“奴隶”称呼自己，他透露到在这里必须高强度劳作。",
            "一位工人表示这里的工作很辛苦，但他仍然坚强地微笑着，这令记者感到动容。",
            3,
            4
        ),

        new CardSettings(
            "克扣薪水",
            "严苛制度",
            "记者了解到，曾有一位工人因为拿不到完整的薪水，而闯进办公室里大闹。",
            "这里的制度非常严格，一旦有人没有出席应到的活动，就会收到严重的处罚。",
            4,
            3
        ),

        new CardSettings(
            "隐瞒",
            "命令",
            "看见记者，这里的管理者第一反应就是不让记者靠近存放文件的地方，我们无从得知他们想要隐瞒什么。",
            "没等记者透露来意，这位经理就以涉密为理由命令记者离开。",
            3,
            2
        ),

        new CardSettings(
            "身负巨债",
            "新的受害者",
            "记者想办法拿到了一份内部文件，上面写明这家工厂正背负着巨额债务。",
            "背负着巨大的风险，记者拿到了一些机密资料，证明这里还在计划扩张，也许不久的将来，就会出现新的受害者。",
            4,
            3
        ),

        new CardSettings(
            "裙带关系",
            "酒池肉林",
            "据有关人士透露，恶犬主人是经理的亲戚，恶霸的底气来原来源自于裙带关系。",
            "记者注意到，这位管理层办公室里的都是年轻漂亮的女性，这也许能侧面说明一些问题。",
            2,
            4
        ),

        new CardSettings(
            "纵容者",
            "忍气吞声",
            "记者采访到了一位知情者，工厂经理曾经保护那个恶霸。",
            "有工人向记者表示他早有不满，但由于上级领导的关系，只能忍气吞声。",
            2,
            4
        ),

        new CardSettings(
            "不合格产品",
            "销毁证据",
            "在偌大的工厂里，堆放着一些已经生产好的产品，但据记者观察，这些产品的质量都存在一些问题。",
            "在这里，记者发现了一些东西，但还没等记者一探究竟，便有人急匆匆的将东西进行销毁。",
            4,
            3
        ),

        new CardSettings(
            "被悬吊的人",
            "无人关心",
            "记者在一块告示板上发现了一张没来得及撕掉的照片，照片里是一些被绳索悬吊起来的人。",
            "工厂里有一块安全生产相关的告示牌，但是并没有任何人关注。",
            3,
            4
        )
    };


    [SerializeField] private GameObject notFound;

    [SerializeField] private Text textA;
    [SerializeField] private Text textB;

    [SerializeField] private RectMask2D usedA;
    [SerializeField] private RectMask2D usedB;

    [SerializeField] private RectTransform rect;

    [SerializeField] private GameObject content;
    [SerializeField] private Text contentText;

    private string _contentA;
    private string _contentB;
    private bool _found;

    private int _id;

    private bool _isA = true;

    private int _targetA;
    private int _targetB;

    private bool _usedA;
    private bool _usedB;

    private void Start()
    {
        var match = Regex.Match(name, "\\d+");
        _id = int.Parse(match.Value);
        Manager.Instance.Cards[_id] = this;

        var settings = CardSettingsList[_id - 1];
        Init(
            settings.TitleA,
            settings.TitleB,
            settings.ContentA,
            settings.ContentB,
            settings.TargetA,
            settings.TargetB
        );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Manager.Instance.CardLock) return;
        if (!_found) return;

        Sound.Instance.Click();
        
        if (_isA)
        {
            if (!_usedA) Manager.Instance.News[_targetA].Remove(_id);
            if (!_usedB) Manager.Instance.News[_targetB].Add(_id);

            Manager.Instance.News[_targetA].HideHighLight();
            Manager.Instance.News[_targetB].ShowHighLight();
        }
        else
        {
            if (!_usedB) Manager.Instance.News[_targetB].Remove(_id);
            if (!_usedA) Manager.Instance.News[_targetA].Add(_id);

            Manager.Instance.News[_targetA].ShowHighLight();
            Manager.Instance.News[_targetB].HideHighLight();
        }

        _isA = !_isA;
        rect.DOKill();
        rect.DORotate(new Vector3(0, _isA ? 0 : 180), 0.3f);
        contentText.text = _isA ? _contentA : _contentB;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_found) return;

        Manager.Instance.News[_isA ? _targetA : _targetB].ShowHighLight();

        contentText.text = _isA ? _contentA : _contentB;
        content.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_found) return;

        Manager.Instance.News[_isA ? _targetA : _targetB].HideHighLight();

        content.SetActive(false);
    }

    public void Init(
        string a,
        string b,
        string contentA,
        string contentB,
        int targetA,
        int targetB)
    {
        textA.text = a;
        textB.text = b;
        _contentA = contentA;
        _contentB = contentB;

        _targetA = targetA;
        _targetB = targetB;
    }

    public void Found()
    {
        _found = true;
        notFound.SetActive(false);

        Manager.Instance.News[_targetA].Add(_id);
    }

    private void UsedA()
    {
        _usedA = true;
        usedA.DoRight(0, 0.3f);
    }

    private void UsedB()
    {
        _usedB = true;
        usedB.DoRight(0, 0.3f);
    }

    public void Used()
    {
        if (_isA) UsedA();
        else UsedB();
    }
}