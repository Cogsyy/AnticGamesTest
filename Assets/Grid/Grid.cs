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
            }
        }

        /// <summary>
        /// Finds the closest unit as compared to the passed unit
        /// </summary>
        /// <param name="comparingUnit">The unit to start the search from</param>
        /// <param name="friendly">Whether or not to search for friendly or enemy units</param>
        /// <returns></returns>
        public IUnit FindClosestUnitTo(IUnit comparingUnit, bool friendly)
        {
            Vector2Int gridPosition = ConvertFromWorldToCell(comparingUnit.GetPosition());

            TCell currentCell = GetCell(gridPosition.x, gridPosition.y);

            IUnit closestUnit = null;
            float closestDistance = float.MaxValue;
            const int MAX_CELL_RANGE = 15;

            for (int radius = 0; radius < MAX_CELL_RANGE; radius++)
            {
                //Search cells in a square around the center tile and expand the search out as it goes
                List<TCell> cells = GetCellsInRadius(gridPosition, radius + 1, radius + 1);

                if (radius == 0)
                {
                    //If it's the first radius check, also check the current cell
                    cells.Add(currentCell);
                }

                for (int i = 0; i < cells.Count; i++)
                {
                    //Check the list of units at this cell. Is there an AntUnit here?
                    foreach (IUnit unit in cells[i])
                    {
                        //Make sure the unit is not yourself, and if looking for friendly units, it must be an AntUnit, otherwise if enemy, it must not be an AntUnit
                        if (unit != comparingUnit && (friendly && unit is AntUnit) || !friendly && (unit is AntUnit == false))
                        {
                            float distance = Vector3.Distance(unit.GetPosition(), comparingUnit.GetPosition());
                            if (distance < closestDistance)
                            {
                                closestDistance = distance;
                                closestUnit = unit;
                            }
                        }
                    }
                }

                if (closestUnit != null)
                {
                    //Since the search is from the center of this unit, out, if any unit is found within the current radius check (the outer for loop),
                    //it will be the closest after assessing all their distances from this current radius.
                    break;
                }
            }

            return closestUnit;
        }

        private List<TCell> GetCellsInRadius(Vector2Int startingPoint, int minRadius, int maxRadius)
        {
            List<TCell> cellsToCheck = new List<TCell>();
            //Check all cells in a radius of 1
            for (int x = -maxRadius; x <= maxRadius; x++)
            {
                for (int y = -maxRadius; y <= maxRadius; y++)
                {
                    //Skip minimum cells
                    int distanceSquared = x * x + y * y;
                    if (distanceSquared < minRadius * minRadius)
                        continue;
                    //Skip the center cell
                    if (x == 0 && y == 0)
                        continue;

                    TCell toCheck = GetCell(startingPoint.x + x, startingPoint.y + y);
                    if (toCheck != null)
                    {
                        cellsToCheck.Add(toCheck);
                    }
                }
            }

            return cellsToCheck;
        }

        public Vector2Int ConvertFromWorldToCell(Vector3 position)
        {
            int cellX = Mathf.FloorToInt((position.x - _settings.MIN_WORLD_X) / _settings.cellSize);
            int cellY = Mathf.FloorToInt((position.y - _settings.MIN_WORLD_Y) / _settings.cellSize);

            return new Vector2Int(cellX, cellY);
        }
    }
}
