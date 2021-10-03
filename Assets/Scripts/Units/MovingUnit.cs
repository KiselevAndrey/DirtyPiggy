using UnityEngine;
using DG.Tweening;
using KAP.Helper;

public class MovingUnit : Unit, IMovingUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] protected float timeToRelocate;

    [Header("References")]
    [SerializeField] private Animator animator;

    protected Direction.Directions _forwardDirection;
    private Sequence _moveSequence;
    private Sequence _changeCellSequence;
    private System.Collections.Generic.List<Cell> _startedCellList;
    private bool _isMoving;

    #region Properies
    public bool IsMoving 
    {
        get => _isMoving; 
        private set
        {
            _isMoving = value;
            animator.SetBool("Moving", value);
        }
    }

    public Cell HomeCell { get; set; }
    #endregion

    #region Unity functions
    private void Awake()
    {
        _moveSequence = DOTween.Sequence();
        _changeCellSequence = DOTween.Sequence();
    }

    private void OnDisable()
    {
        transform.position = HomeCell.Position;
    }

    private void OnDestroy()
    {
        HomeCell = null;
    }
    #endregion

    #region Move To
    public void MoveTo(Cell cell, float duration)
    {
        _moveSequence.Kill();
        _changeCellSequence.Kill();

        _moveSequence = DOTween.Sequence();
        _moveSequence.AppendCallback(() => IsMoving = true)
            .Append(transform.DOMove(cell.Position, duration))
            .AppendCallback(() => EndMoving());

        _changeCellSequence = DOTween.Sequence();
        _changeCellSequence.AppendInterval(duration / 2)
            .AppendCallback(() => Cell.RemoveUnit(this))
            .AppendCallback(() => Cell = cell)
            .AppendCallback(() => Cell.AddUnit(this));
    }

    public void MoveTo(Cell cell, float duration, Direction.Directions direction)
    {
        _forwardDirection = direction;
        MoveTo(cell, duration);
    }

    public void MoveTo(Cell cell) => MoveTo(cell, timeToRelocate);

    public void MoveTo(Cell cell, Direction.Directions direction)
    {
        _forwardDirection = direction;
        MoveTo(cell, timeToRelocate);
    }

    public virtual void MoveToStartPosition()
    {
        MoveTo(_startedCellList.Random(), timeToRelocate * 3);
    }

    protected virtual void EndMoving()
    {
        IsMoving = false;
    }
    #endregion

    public void SetStartCellList(System.Collections.Generic.List<Cell> cells)
    {
        _startedCellList = cells;
    }
}
