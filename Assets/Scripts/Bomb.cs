using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolable<Bomb>
{
    [SerializeField] private float _explotionRadius;
    [SerializeField] private float _explotionForse;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _delay;

    private Color _color;
    private SphereCollider _collider;

    private float _maxAlpha = 1f;
    private float _minAlpha = 0f;
    private float _delta = 5f;

    public event Action<Bomb> DisappearedObject;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _renderer.material = new Material(_renderer.material);
    }

    private void OnEnable()
    {
        SetDefaultCharacteristics();
        Activate();
    }

    public void Activate()
    {
        StartCoroutine(StartDetonation());
    }

    private IEnumerator StartDetonation()
    {
        yield return ChangeAlphaColor();

        Explode();
        DisappearedObject?.Invoke(this);
    }

    private IEnumerator ChangeAlphaColor()
    {
        while (Mathf.Approximately(_renderer.material.color.a, _minAlpha) == false)
        {
            Color color = _renderer.material.color;

            color.a = Mathf.MoveTowards(color.a, _minAlpha, Time.deltaTime / _delta);
            _renderer.material.color = color;
            yield return null;
        }
    }

    private void SetDefaultCharacteristics()
    {
        _color.a = _maxAlpha;
        _renderer.material.color = _color;
        _collider.isTrigger = false;
    }

    private void Explode()
    {
        foreach (Rigidbody obj in GetExplodebleObjects())
            obj.AddExplosionForce(_explotionForse, transform.position, _explotionRadius);
    }

    private List<Rigidbody> GetExplodebleObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explotionRadius);
        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null && (hit.gameObject.TryGetComponent(out Cube _) || hit.gameObject.TryGetComponent(out Bomb _)))
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
