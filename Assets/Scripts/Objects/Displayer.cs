using TMPro;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _allObjectsText;
    [SerializeField] private TextMeshProUGUI _numberOfCreatObjectsText;
    [SerializeField] private TextMeshProUGUI _numberOfActiveCubesText;
    [SerializeField] private TextMeshProUGUI _numberOfActiveBombsText;
    [SerializeField] private Pool _pool;

    public int AllObjects { get; set; } = 0;

    private void Update()
    {
        ShowInfo();
    }

    private void ShowInfo()
    {
        _allObjectsText.text = $"����������� ������������ �������� �� ��� �����: {AllObjects}";
        _numberOfCreatObjectsText.text = $"����������� ��������� ��������: {_pool.GiveNumberOfAllCreatObject()}";
        _numberOfActiveCubesText.text = $"����������� �������� ����� �� �����: {_pool.GiveNumberOfActiveCubes()}";
        _numberOfActiveBombsText.text = $"����������� �������� ���� �� �����: {_pool.GiveNumberOfActiveBomb()}";
    }
}
