using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Color _color = Color.blue;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor() => _renderer.material.color = Random.ColorHSV();

    public void SetDefaultColor() => _renderer.material.color = _color;
}
