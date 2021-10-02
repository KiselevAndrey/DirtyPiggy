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
    [SerializeField] private Animator animator;

    protected Vector2 _homePosition;
    protected Direction.Directions _forwardDirection;
    private bool _isMoving;

    #region Properies
    public float TimeToRelocate { get; private set; }

    public bool IsMoving 
    {
        get => _isMoving; 
        private set
        {
            _isMoving = value;
            animator.SetBool("Moving", value);
        }
    }
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
            .Append(transform.DOMove(cell.Position, duration))
            .AppendCallback(() => EndMoving());

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(duration / 2)
            .AppendCallback(() => Cell.RemoveUnit(this))
            .AppendCallback(() => Cell = cell)
            .AppendCallback(() => Cell.AddUnit(this));
    }

    public void MoveTo(Cell cell, float duration, Direction.Directions direction)
    {
        _forwardDirection = direction;
        MoveTo(cell, duration);
    }

    public void MoveTo(Cell cell) => MoveTo(cell, TimeToRelocate);

    public void MoveTo(Cell cell, Direction.Directions direction)
    {
        _forwardDirection = direction;
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
