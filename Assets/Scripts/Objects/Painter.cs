using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Color _color = Color.blue;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor() => _renderer.material.SetColor("_Color", Random.ColorHSV());

    public void SetDefaultColor() => _renderer.material.SetColor("_Color", _color);
}
