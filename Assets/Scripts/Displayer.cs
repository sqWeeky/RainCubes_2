using TMPro;
using UnityEngine;

public class Displayer<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _counterTMPro;

    private string _activeObjects = $"�������:";
    private string _createObjects = $"�������:";
    private string _spawnsObjects = $"����������:";

    private void OnEnable()
    {
        _spawner.CounterChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.CounterChanged -= ShowInfo;
    }

    private void ShowInfo(int creatCount, int activeCount, int spawnCount)
    {
        _counterTMPro.text = $"{_activeObjects} {activeCount}\n" +
                             $"{_createObjects} {creatCount}\n" +
                             $"{_spawnsObjects} {spawnCount}";
    }
}