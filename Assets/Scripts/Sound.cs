using System;
using UnityEngine;
public class Sound : MonoBehaviour
{
    public static Sound Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private AudioSource found;
    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource announcement;
    [SerializeField] private AudioSource speak;
    [SerializeField] private AudioSource policeCar;
    [SerializeField] private AudioSource victory;

    public void Found()
    {
        found.Play();
    }

    public void Click()
    {
        click.Play();
    }

    public void Announcement()
    {
        announcement.Play();
    }

    public void Speak()
    {
        speak.Play();
    }

    public void PoliceCar()
    {
        policeCar.Play();
    }

    public void Victory()
    {
        victory.Play();
    }
}