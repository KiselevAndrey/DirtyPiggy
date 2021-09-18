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
    #endregion

    private void Start()
    {
        TimeToRelocate = startTimeToRelocate;
        _startPosition = transform.position;
        MoveTo(startedCellList.Random(), (int)(TimeToRelocate * 3));
    }

    public void MoveTo(Cell cell, int duration)
    {
        transform.DOMove(cell.Position, duration);
    }
}
