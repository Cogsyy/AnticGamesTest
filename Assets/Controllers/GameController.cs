using System.Collections.Generic;
using UnityEngine;
using SpatialPartitionSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private ResultsPanel _resultsPanel;
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private AntFactory _antFactory;
    [SerializeField] private BeetleFactory _beetleFactory;
    [SerializeField] private AphidFactory _aphidFactory;

    private int _enemiesEliminated = 0;
    private bool _gameStarted;

    public Grid<List<IUnit>> grid { get; private set; }

    private void Awake()
    {
        MessagesBroker.Instance.AddListener(MessagingType.GameStarted, OnGameStarted);
        MessagesBroker.Instance.AddListener(MessagingType.EnemyReachedFlag, OnEnemyReachedFlag);
        MessagesBroker.Instance.AddListener<UnitBase>(MessagingType.UnitDied, OnUnitDied);
        MessagesBroker.Instance.AddListener<bool>(MessagingType.AIModeToggled, OnAIModeToggled);
    }

    private void OnGameStarted()
    {
        if (!_gameStarted)
        {
            _gameStarted = true;
            //Spawn some friendly ants
            SpawnRandomUnits(_antFactory, _gameSettings.allyAntCount, 1, 3);
            //Spawn enemy units
            SpawnRandomUnits(_beetleFactory, _gameSettings.enemyCount / 2, 3, 10);
            SpawnRandomUnits(_aphidFactory, _gameSettings.enemyCount / 2, 3, 10);
        }
    }

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new Grid<List<IUnit>>(_gameSettings.gridSettings);

        for (int i = 0; i < _gameSettings.gridSettings.gridWidth; i++)
        {
            for (int j = 0; j < _gameSettings.gridSettings.gridWidth; j++)
            {
                grid.AddCell(i, j, new List<IUnit>());
            }
        }
    }

    private void OnAIModeToggled(bool isAIMode)
    {
        //Update the properties of the ant for the factories if AI mode is toggled before play, or for when the game is reset
        _antFactory.unitProperties.antProperties.isAIMode = isAIMode;
    }

    private void SpawnRandomUnits(UnitFactory unitFactory, int unitAmount, float radiusMin, float radiusMax)
    {
        for (int i = 0; i < unitAmount; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);

            //Correct random radius for uniform distribution
            float radius = Mathf.Sqrt(Random.Range(radiusMin * radiusMin, radiusMax * radiusMax));

            //Convert polar to Cartesian coordinates
            Vector2 randomPosition = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );

            IUnit newUnit = unitFactory.CreateUnit(randomPosition, grid);
            grid.RegisterUnit(newUnit);
        }
    }

    private void OnEnemyReachedFlag()
    {
        _resultsPanel.ShowResults(victory: false, _enemiesEliminated);//parameter specification for clarity
    }

    private void OnUnitDied(UnitBase deadUnit)
    {
        grid.UnregisterUnit(deadUnit);
        if (deadUnit is AntUnit == false)
        {
            _enemiesEliminated++;

            if (_enemiesEliminated >= _gameSettings.enemyCount)
            {
                _resultsPanel.ShowResults(victory: true, _enemiesEliminated);
            }
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
