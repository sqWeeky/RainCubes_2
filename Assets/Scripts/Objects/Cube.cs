using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Painter))]
public class Cube : MonoBehaviour
{
    private Pool _pool;
    private Painter _painter;
    private MeshRenderer _meshRenderer;
    private Coroutine _coroutine;

    private bool _isTouched;
    private int _maxDelay = 4;
    private int _minDelay = 2;
    private int _lifetime;

    public event Action<Cube> CubeDisappeared;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _painter = GetComponent<Painter>();
    }

    private void OnEnable()
    {
        GeneratLifeTime();
        _isTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_isTouched == false)
            {
                _painter.SetRandomColor();
                _isTouched = true;
                _coroutine = StartCoroutine(DestroyObject());
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        var waitForSeconds = new WaitForSeconds(_lifetime);

        while (_lifetime > 0)
        {
            _lifetime--;

            if (_lifetime <= 0)
            {
                _isTouched = false;
                CubeDisappeared?.Invoke(this);
                _painter.SetDefaultColor();
                gameObject.SetActive(false);

                yield break;
            }

            yield return waitForSeconds;
        }

        StopCoroutine(_coroutine);
    }

    private void GeneratLifeTime() => _lifetime = UnityEngine.Random.Range(_minDelay, _maxDelay);
}
