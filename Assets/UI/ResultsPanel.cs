using TMPro;
using UnityEngine;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_Text _gameResultLabel;
    [SerializeField] private TMP_Text _enemiesEliminatedValueLabel;

    private void Awake()
    {
        //Defaults off
        _content.SetActive(false);
    }

    public void ShowResults(bool victory, int enemiesEliminated = 0)
    {
        _content.SetActive(true);
        _gameResultLabel.text = victory ? "Victory!" : "Defeat!";
        _enemiesEliminatedValueLabel.text = enemiesEliminated.ToString();
    }

    public void HideResults()
    {
        _content.SetActive(false);
    }
}
