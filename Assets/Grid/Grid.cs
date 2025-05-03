using System.Collections.Generic;
using UnityEngine;

namespace SpatialPartitionSystem
{
    public class Grid<TCell> where TCell : ICollection<IUnit>
    {
        private GridSettings _settings;
        private TCell[,] _cellArray;
        private HashSet<IUnit> _registeredUnits = new HashSet<IUnit>();

        public Grid(GridSettings settings)
        {
            _settings = settings;
            _cellArray = new TCell[settings.gridWidth, settings.gridWidth];
        }

        public void AddCell(int i, int j, TCell cell)
        {
            if (IsValidCellPosition(i, j))
            {
                _cellArray[i, j] = cell;
            }
        }

        public bool IsValidCellPosition(int i, int j)
        {
            return i >= 0 && j >= 0 && i < _settings.gridWidth && j < _settings.gridWidth;
        }

        public TCell GetCell(int i, int j)
        {
            if (IsValidCellPosition(i, j))
            {
                return _cellArray[i, j];
            }
            else
            {
                Debug.LogError("Error: Invalid cell position at: " + i + ", " + j);
            }

            return default;
        }

        public void DeleteCell(int i, int j)
        {
            if (IsValidCellPosition(i, j))
            {
                _cellArray[i, j] = default;
            }
        }

        public void DeleteCell(TCell cell)
        {

        }

        public void RegisterUnit(IUnit unitToRegister)
        {
            Vector2Int position = ConvertFromWorldToCell(unitToRegister.GetPosition());
            if (!IsValidCellPosition(position.x, position.y))
            {
                Debug.LogError("Error: Not a valid grid position: " + position.x + ", " + position.y);
                return;
            }

            _cellArray[position.x, position.y].Add(unitToRegister);
            _registeredUnits.Add(unitToRegister);
        }

        public void UnregisterUnit(IUnit unitToUnregister)
        {
            if (_registeredUnits.Contains(unitToUnregister))
            {
                Vector2Int position = ConvertFromWorldToCell(unitToUnregister.GetPosition());
                _cellArray[position.x, position.y].Remove(unitToUnregister);
                _registeredUnits.Remove(unitToUnregister);
            }
        }

        public void Move(IUnit unit, Vector3 oldPosition)
        {
            //Find the previous cell
            Vector2Int cellPositionOld = ConvertFromWorldToCell(oldPosition);

            //Check which cell it's in now
            Vector2Int cellPositionCurrent = ConvertFromWorldToCell(unit.GetPosition());

            bool unitChangedCells = cellPositionOld.x != cellPositionCurrent.x || cellPositionOld.y != cellPositionCurrent.y;
            if (unitChangedCells)
            {
                TCell oldCell = GetCell(cellPositionOld.x, cellPositionOld.y);
                if (oldCell != null)
                {
                    oldCell.Remove(unit);
                }

                TCell newCell = GetCell(cellPositionCurrent.x, cellPositionCurrent.y);
                if (newCell != null)
                {
                    newCell.Add(unit);
                }

                //Debug.Log("Current cell: " + cellPositionCurrent.x + ", " + cellPositionCurrent.y);
            }
        }

        public Vector2Int ConvertFromWorldToCell(Vector3 position)
        {
            int cellX = Mathf.FloorToInt((position.x - _settings.MIN_WORLD_X) / _settings.cellSize);
            int cellY = Mathf.FloorToInt((position.y - _settings.MIN_WORLD_Y) / _settings.cellSize);

            return new Vector2Int(cellX, cellY);
        }
    }
}
