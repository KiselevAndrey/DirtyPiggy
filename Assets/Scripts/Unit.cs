using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float startTimeToRelocate;

    [Header("References")]
    [SerializeField] private List<Cell> startedCellList;

    public Cell Cell { get; set; }

    public float TimeToRelocate { get; private set; }

    private void Start()
    {
        TimeToRelocate = startTimeToRelocate;
    }

    public void MoveTo(Cell cell)
    {
        throw new System.NotImplementedException();
    }
}
