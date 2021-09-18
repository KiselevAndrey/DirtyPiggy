using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        
    }

    public void MoveTo(Cell cell)
    {
        transform.DOMove(cell.Position, TimeToRelocate);
    }
}
