using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Button _debugButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _antThoughts;
    [SerializeField] private LineDrawer _lineDrawer;

    private List<LineDrawer> _lines = new List<LineDrawer>();
    private List<AntUnit> _antList = new List<AntUnit>();
    private bool panelActive;

    private void Awake()
    {
        _debugButton.onClick.AddListener(TogglePanel);
        _panel.SetActive(false);
    }

    public void RegisterAnt(AntUnit ant)
    {
        _antList.Add(ant);
        //Not working. LineRenderer's aren't drawn correctly
        LineDrawer lineDrawer = Instantiate(_lineDrawer, Vector3.zero, Quaternion.identity, transform);
        lineDrawer.SetAnt(ant);
        _lines.Add(lineDrawer);
    }

    public void TogglePanel()
    {
        panelActive = !panelActive;

        _panel.SetActive(panelActive);

        for (int i = 0; i < _lines.Count; i++)
        {
            _lines[i].enabled = panelActive;
        }
    }

    private void Update()
    {
        if (_antList.Count > 0)
        {
            _antThoughts.text = _antList[0].debugReasoning;
        }
    }
}
