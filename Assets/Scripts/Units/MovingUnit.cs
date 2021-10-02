using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KAP.Helper;

public class MovingUnit : Unit, IMovingUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] protected float startTimeToRelocate;

    [Header("References")]
    [SerializeField] private List<Cell> startedCellList;

    protected Vector2 _homePosition;
    protected Direction.Directions _forwardDirection;

    #region Properies

    public float TimeToRelocate { get; private set; }

    public bool IsMoving { get; private set; }
    #endregion

    private void Start()
    {
        TimeToRelocate = startTimeToRelocate;
        _homePosition = transform.position;
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
            .AppendCallback(() => EndMoving());
    }

    public void MoveTo(Cell cell)
    {
        MoveTo(cell, TimeToRelocate);
    }

    public virtual void MoveToStartPosition()
    {
        MoveTo(startedCellList.Random(), TimeToRelocate * 3);
    }

    protected virtual void EndMoving()
    {
        IsMoving = false;
    }
    #endregion
}
