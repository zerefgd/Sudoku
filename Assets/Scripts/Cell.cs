using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [HideInInspector] public int Value;
    [HideInInspector] public int Row;
    [HideInInspector] public int Col;
    [HideInInspector] public bool IsLocked;
    [HideInInspector] public bool IsIncorrect;

    [SerializeField] private SpriteRenderer _bgSrpite;
    [SerializeField] private TMP_Text _valueText;

    [SerializeField] private Color _startUnlockedCellColor;
    [SerializeField] private Color _startUnlockedTextColor;
    [SerializeField] private Color _startLockedCellColor;
    [SerializeField] private Color _startLockedTextColor;
    [Space]
    [Header("HighLighted")]
    [SerializeField] private Color _highlightLockedCellColor;
    [SerializeField] private Color _highlightLockedTextColor;
    [SerializeField] private Color _highlightUnlockedCellColor;
    [SerializeField] private Color _highlightUnlockedTextColor;
    [SerializeField] private Color _highlightWrongCellColor;
    [SerializeField] private Color _highlightWrongTextColor;
    [Space]
    [Header("Selected")]
    [SerializeField] private Color _selectedCellColor;
    [SerializeField] private Color _selectedTextColor;
    [SerializeField] private Color _selectedWrongCellColor;
    [SerializeField] private Color _selectedWrongTextColor;
    [Space]
    [Header("Reset")]
    [SerializeField] private Color _resetCellColor;
    [SerializeField] private Color _resetTextColor;
    [SerializeField] private Color _resetWrongCellColor;
    [SerializeField] private Color _resetWrongTextColor;

    public void Init(int value)
    {
        IsIncorrect = false;
        Value = value;

        if (value == 0)
        {
            IsLocked = false;
            _bgSrpite.color = _startUnlockedCellColor;
            _valueText.color = _startUnlockedTextColor;
            _valueText.text = "";
        }
        else
        {
            IsLocked = true;
            _bgSrpite.color = _startLockedCellColor;
            _valueText.color = _startLockedTextColor;
            _valueText.text = Value.ToString();
        }
    }

    public void HighLight()
    {
        if (IsLocked)
        {
            _bgSrpite.color = _highlightLockedCellColor;
            _valueText.color = _highlightLockedTextColor;
        }
        else
        {
            if (IsIncorrect)
            {
                _bgSrpite.color = _highlightWrongCellColor;
                _valueText.color = _highlightWrongTextColor;
            }
            else
            {
                _bgSrpite.color = _highlightUnlockedCellColor;
                _valueText.color = _highlightUnlockedTextColor;
            }
        }
    }

    public void Select()
    {
        if (IsIncorrect)
        {
            _bgSrpite.color = _selectedWrongCellColor;
            _valueText.color = _selectedWrongTextColor;
        }
        else
        {
            _bgSrpite.color = _selectedCellColor;
            _valueText.color = _selectedTextColor;
        }
    }


    public void Reset()
    {
        if (IsLocked)
        {
            _bgSrpite.color = _startLockedCellColor;
            _valueText.color = _startLockedTextColor;
        }
        else
        {
            if (IsIncorrect)
            {
                _bgSrpite.color = _resetWrongCellColor;
                _valueText.color = _resetWrongTextColor;
            }
            else
            {
                _bgSrpite.color = _resetCellColor;
                _valueText.color = _resetTextColor;
            }
        }
    }

    public void UpdateValue(int value)
    {
        Value = value;
        _valueText.text = Value == 0 ? "" : Value.ToString();
    }

    public void UpdateWin()
    {
        _bgSrpite.color = _startLockedCellColor;
        _valueText.color = _startLockedTextColor;
    }
}
