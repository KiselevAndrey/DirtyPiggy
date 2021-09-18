using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KAP.Helper;

public class Unit : MonoBehaviour, IUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float startTimeToRelocate;

    [Header("References")]
    [SerializeField] private List<Cell> startedCellList;

    private Vector2 _startPosition;

    #region Properies
    public Cell Cell { get; set; }

    public float TimeToRelocate { get; private set; }

    public bool IsMoving { get; private set; }
    #endregion

    private void Start()
    {
        TimeToRelocate = startTimeToRelocate;
        _startPosition = transform.position;
        MoveToStartPosition();
    }

    public void MoveTo(Cell cell, float duration)
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendCallback(() => IsMoving = true)
            .Append(transform.DOMove(cell.Position, duration))
            .AppendCallback(() => IsMoving = false)
            .AppendCallback(() => Cell = cell);
    }

    public void MoveTo(Cell cell)
    {
        MoveTo(cell, TimeToRelocate);
    }

    public void MoveToStartPosition()
    {
        MoveTo(startedCellList.Random(), TimeToRelocate * 3);
    }
}
