using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KAP.Helper;

public class MovingUnit : Unit, IMovingUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float startTimeToRelocate;

    [Header("References")]
    [SerializeField] private List<Cell> startedCellList;

    private Vector2 _startPosition;

    #region Properies

    public float TimeToRelocate { get; private set; }

    public bool IsMoving { get; private set; }
    #endregion

    private void Start()
    {
        TimeToRelocate = startTimeToRelocate;
        _startPosition = transform.position;
        MoveToStartPosition();
    }

    #region Move To
    public void MoveTo(Cell cell, float duration)
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendCallback(() => IsMoving = true)
            .AppendCallback(() => Cell.RemoveUnit(this))
            .Append(transform.DOMove(cell.Position, duration))
            .AppendCallback(() => Cell = cell)
            .AppendCallback(() => Cell.AddUnit(this))
            .AppendCallback(() => IsMoving = false);
    }

    public void MoveTo(Cell cell)
    {
        MoveTo(cell, TimeToRelocate);
    }

    public void MoveToStartPosition()
    {
        MoveTo(startedCellList.Random(), TimeToRelocate * 3);
    }
    #endregion
}
