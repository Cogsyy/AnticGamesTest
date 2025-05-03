using System.Collections.Generic;
using UnityEngine;
using SpatialPartitionSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private ResultsPanel _resultsPanel;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Transform _flag;
    [SerializeField] private AntFactory _antFactory;
    [SerializeField] private BeetleFactory _beetleFactory;

    private int _enemiesEliminated = 0;

    private Grid<List<IUnit>> _grid;

    private void Awake()
    {
        MessagesBroker.Instance.AddListener(MessagingType.GameStarted, OnGameStarted);
        MessagesBroker.Instance.AddListener(MessagingType.EnemyReachedFlag, OnEnemyReachedFlag);
        MessagesBroker.Instance.AddListener<UnitBase>(MessagingType.UnitDied, OnUnitDied);
    }

    private void OnGameStarted()
    {
        //Spawn some enemies
        IUnit ant = _antFactory.CreateUnit(new Vector3(5, 3, 0), _flag.position, _grid);
        _grid.RegisterUnit(ant);

        for (int i = 0; i < _gameSettings.enemyCount; i++)
        {
            Vector2 randomPosition = Random.insideUnitCircle;
            randomPosition.x *= 10f;
            randomPosition.y *= 10f;

            IUnit beetle = _beetleFactory.CreateUnit(randomPosition, _flag.position, _grid);
            _grid.RegisterUnit(beetle);

            if (i == 0)//Temporary
            {
                ((AntUnit)ant).GetComponent<MovementBase>().SetTargetTransform(((BeetleUnit)beetle).transform);
            }
        }
    }

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _grid = new Grid<List<IUnit>>(_gameSettings.gridSettings);

        for (int i = 0; i < _gameSettings.gridSettings.gridWidth; i++)
        {
            for (int j = 0; j < _gameSettings.gridSettings.gridWidth; j++)
            {
                _grid.AddCell(i, j, new List<IUnit>());
            }
        }
    }

    private void OnEnemyReachedFlag()
    {
        _resultsPanel.ShowResults(victory: false, _enemiesEliminated);//parameter specification for clarity
    }

    private void OnUnitDied(UnitBase deadUnit)
    {
        _grid.UnregisterUnit(deadUnit);
        if (deadUnit is AntUnit)
        {
            
        }
        else 
        {
            _enemiesEliminated++;
        }

        Destroy(deadUnit.gameObject);
    }

    private void Update()
    {
        DrawDebugGrid();
    }

    public void DrawDebugGrid()
    {
        
        Vector3 originPosition = new Vector3(_gameSettings.gridSettings.MIN_WORLD_X, _gameSettings.gridSettings.MIN_WORLD_Y);
        int cellSize = _gameSettings.gridSettings.cellSize;
        int width = _gameSettings.gridSettings.gridWidth;

        for (int x = 0; x <= width; x++)
        {
            Debug.DrawLine(
                originPosition + new Vector3(x * cellSize, 0, 0),
                originPosition + new Vector3(x * cellSize, width * cellSize, 0),
                Color.black
            );
        }

        for (int y = 0; y <= width; y++)
        {
            Debug.DrawLine(
                originPosition + new Vector3(0, y * cellSize, 0),
                originPosition + new Vector3(width * cellSize, y * cellSize, 0),
                Color.black
            );
        }
    }
}
