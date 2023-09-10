using UnityEngine;
using TMPro;

public class LossCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _playerLoss;
    private int _enemyLoss;

    public void SetEnemyLoss(int value)
    {
        _enemyLoss = value;
        UpdateText();
    }

    public void SetPlayerLoss(int value)
    {
        _playerLoss = value;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{_playerLoss} : {_enemyLoss}";
    }
}
