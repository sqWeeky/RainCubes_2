using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Painter))]
public class Cube : MonoBehaviour, IPoolable<Cube>
{
    private Painter _painter;

    private bool _isTouched;
    private int _maxDelay = 4;
    private int _minDelay = 2;
    private int _lifetime;

    public event Action<Cube> DisappearedObject;

    private void Awake()
    {
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
                StartCoroutine(DestroyObject());
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
                DisappearedObject?.Invoke(this);
                _painter.SetDefaultColor();

                yield break;
            }

            yield return waitForSeconds;
        }
    }

    private void GeneratLifeTime() => _lifetime = UnityEngine.Random.Range(_minDelay, _maxDelay);
}
