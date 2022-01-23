using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCSettings
{
    public string Name { get; }
    public string Dialog { get; }
    public int Card { get; }

    public NPCSettings(string name, string dialog, int card)
    {
        Name = name;
        Dialog = dialog;
        Card = card;
    }
}

public class NPC : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private static readonly List<NPCSettings> NPCSettingsList = new List<NPCSettings>
    {
        new NPCSettings(
            "门卫小哥",
            "外来人员未经许可不得进入！",
            1
        ),

        new NPCSettings(
            "商店老板",
            "什么都别问我，问就是比你们国家强多了~",
            -1
        ),

        new NPCSettings(
            "路人甲",
            "你看到外面遛狗的人了吗？他的狗脾气可不好，曾经咬伤了一个流浪汉，让他赔了一大笔钱。",
            2
        ),

        new NPCSettings(
            "路人乙",
            "外面那只大狗看起来真吓人，听说那狗的主人曾经在家打狗，街上都能听到狗的惨叫，你说他养狗图啥呢？",
            3
        ),

        new NPCSettings(
            "流浪汉",
            "那只恶狗又来了！上次我差点被它咬死，幸好当时门卫小哥用棍棒打走了它。",
            4
        ),

        new NPCSettings(
            "狗主人",
            "牵什么绳？我家的狗不咬人的！上次那是意外！而且我已经狠狠教训过它了。",
            5
        ),

        new NPCSettings(
            "恶犬",
            "汪！（一副择人而噬的样子）",
            6
        ),

        new NPCSettings(
            "清洁工",
            "唉，刚刚不小心撞倒了垃圾桶，十分抱歉，我马上打扫干净。",
            7
        ),

        new NPCSettings(
            "扳手工人",
            "这里的工作很辛苦，但是工资非常高，谁还不是金钱的奴隶呢（笑），所以我还是很满意的。",
            8
        ),

        new NPCSettings(
            "财务人员",
            "遵守公司制度是很重要的，上个月有个无故旷工的工人，被扣了工资还来吵闹，真是不知所谓！",
            9
        ),

        new NPCSettings(
            "工厂经理",
            "柜子里是保密资料，请离开！",
            10
        ),

        new NPCSettings(
            "资料柜",
            "一份被批准的贷款申请，表明工厂业绩良好，已向银行贷款以扩大生产。",
            11
        ),

        new NPCSettings(
            "人事小妹",
            "门口那个带狗的无赖又来找经理要钱了，经理正头疼怎么处理这个小舅子。",
            12
        ),

        new NPCSettings(
            "榔头工人",
            "我早就看不惯那泼皮欺负经理好说话了！要不是经理拦着我非得揍他一顿！",
            13
        ),

        new NPCSettings(
            "技术员",
            "这些是劣质产品，待会将会被销毁。",
            14
        ),

        new NPCSettings(
            "告示牌",
            "一块告示牌，画着高空安全绳索的使用照片。",
            15
        ),
    };

    [SerializeField] private Texture2D normal;
    [SerializeField] private Texture2D dialog;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private bool needAnimation;

    private string _name;
    private string _dialog;
    private int _card;

    private bool _gave;

    private int _id;

    private void Start()
    {
        var match = Regex.Match(name, "\\d+");
        _id = int.Parse(match.Value);

        Manager.Instance.NPCs[_id] = this;

        var settings = NPCSettingsList[_id - 1];
        _name = settings.Name;
        _dialog = settings.Dialog;
        _card = settings.Card;

        spriteRenderer.sprite = sprite1;
    }

    private const float AnimationSeparate = 0.5f;
    private bool _spriteFlag;
    private float _spriteCounter;
    private void Update()
    {
        if (!needAnimation) return;
        _spriteCounter += Time.deltaTime;
        if (_spriteCounter >= AnimationSeparate)
        {
            _spriteCounter -= AnimationSeparate;
            _spriteFlag = !_spriteFlag;
            spriteRenderer.sprite = _spriteFlag ? sprite2 : sprite1;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Reporter.Instance.CanMove) return;
        Cursor.SetCursor(dialog, new Vector2(0, 32), CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Reporter.Instance.CanMove) return;
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Reporter.Instance.CanMove) return;
        Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);

        if (Vector3.Distance(Reporter.Instance.transform.position, transform.position) <= 2)
        {
            if (_id == 12)
            {
                if (Manager.Instance.NPCs[11].gameObject.activeSelf)
                {
                    Dialog.Instance.StartDialog(new List<string>
                    {
                        $"<b>{NPCSettingsList[10].Name}</b>:{NPCSettingsList[10].Dialog}"
                    }, () => { Reporter.Instance.CanMove = true; });
                    return;
                }
            }

            var list = new List<string>
            {
                $"<b>{_name}</b>：{_dialog}",
            };
            if (_card > 0)
            {
                list.Add(!_gave ? "【得到了一条线索！】" : "【这里似乎没什么重要的信息了……】");
            }
            Dialog.Instance.StartDialog(list, () => {
                if (_card > 0 && !_gave)
                {
                    _gave = true;
                    Sound.Instance.Found();
                    Report.Instance.Shake();
                    Manager.Instance.Cards[_card].Found();
                }
                Reporter.Instance.CanMove = true;
            });
        }
        else
        {
            Dialog.Instance.StartDialog(new List<string>
            {
                "【太远了……】"
            }, () => { Reporter.Instance.CanMove = true; });
        }
    }
}