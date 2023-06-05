using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class FigurePositionValidator
{
    public event Action CorrectMoveCompleted; 
    private readonly Dictionary<Cell, List<PlayerFigure>> _figuresByCell;
    private readonly Cell[] _cells;

    private const float CellRadius = 0.2f;
    private const float YOffset = 0.286f;

    public FigurePositionValidator(Cell[] cells)
    {
        _figuresByCell = new Dictionary<Cell, List<PlayerFigure>>();
        _cells = cells;
    }

    public void InitializeStartPosition(PlayerData[] players)
    {
        foreach (var player in players)
        {
            UpdatePosition(_cells[0], player.Figure);
            player.Figure.CellChanged += ValidatePosition;
        }
    }

    private void ValidatePosition(Cell newCell, PlayerFigure playerFigure)
    {
        if (!IsCorrectMove(newCell, playerFigure))
        {
            newCell = _figuresByCell.First(pair => pair.Value.Contains(playerFigure)).Key;
            UpdatePosition(newCell, playerFigure);
        }
        else
        {
            UpdatePosition(newCell, playerFigure);
            CorrectMoveCompleted?.Invoke();
        }
    }

    private void UpdatePosition(Cell newCell, PlayerFigure playerFigure)
    {
        foreach (var (cell, figures) in _figuresByCell)
        {
            if (!figures.Contains(playerFigure)) continue;
            figures.Remove(playerFigure);
            CorrectFigurePositions(cell);
        }

        if (!_figuresByCell.ContainsKey(newCell))
            _figuresByCell[newCell] = new List<PlayerFigure>();
        _figuresByCell[newCell].Add(playerFigure);
        CorrectFigurePositions(newCell);
    }

    private bool IsCorrectMove(Cell newCell, PlayerFigure playerFigure) =>
        newCell == _cells[playerFigure.Owner.CellIndex];

    private void CorrectFigurePositions(Cell cell)
    {
        var cellCenter = cell.transform.position;
        if (_figuresByCell[cell].Count == 1)
        {
            _figuresByCell[cell][0].transform.position = cellCenter + Vector3.up * YOffset;
            return;
        }

        for (var i = 0; i < _figuresByCell[cell].Count; i++)
        {
            var angleInRadians = (float) i / _figuresByCell[cell].Count * 2 * Mathf.PI;
            _figuresByCell[cell][i].transform.position = new Vector3(
                cellCenter.x + CellRadius * Mathf.Cos(angleInRadians), cellCenter.y + YOffset,
                cellCenter.z + CellRadius * Mathf.Sin(angleInRadians));
        }
    }
}