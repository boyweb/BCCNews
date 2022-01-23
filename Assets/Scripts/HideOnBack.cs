using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class HideOnBack : MonoBehaviour
{
    private Graphic _renderer;
    private Camera _camera;

    private void Start()
    {
        _renderer = GetComponent<Graphic>();
        _camera = Camera.main;
    }

    private void Update()
    {
        var rect = transform;
        var angle = Vector3.Angle(rect.position - _camera.transform.position, rect.forward);
        _renderer.enabled = angle < 90;
    }
}