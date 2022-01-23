using System;
using System.Collections.Generic;
using UnityEngine;
public class Manager : MonoBehaviour
{
    public static Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static readonly List<string> VictoryContent = new List<string>
    {
        "做的很好！你证明了自己是我们需要的那种人才。\n现在，完成另一篇揭示“真相”的报道，你的前途将一片光明。",
        "做的很好！你完美地证明了自己，欢迎你正式加入我们！"
    };

    public bool CardLock { get; set; }
    public int Victory { get; set; }

    public readonly Dictionary<int, NPC> NPCs = new Dictionary<int, NPC>();
    public readonly Dictionary<int, Card> Cards = new Dictionary<int, Card>();
    public readonly Dictionary<int, News> News = new Dictionary<int, News>();
}