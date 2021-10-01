using UnityEngine;

public class AIMoving : MovingUnit, IMovingUnit
{
    [Header("AI Parameters")]
    [Tooltip("The number of cells, after how long the player will be detected")]
    [SerializeField, Min(1)] private int findForwardRange = 2;
    [Tooltip("The percentage of time from a normal transition that is spent on an attack")]
    [SerializeField, Range(0.2f, 1f)] private float reducingTransitionTime = 0.5f;
    [SerializeField, Min(0)] private float maxIdleTime = 5f;

    [SerializeField] Pig pig;
    
    private System.Type _pigType;

    private void Awake()
    {
        _pigType = new Pig().GetType();
    }

    protected override void EndMoving()
    {
        base.EndMoving();
        TryFindPlayer(out Cell playerCell);
    }

    private bool TryFindPlayer(out Cell findingCell)
    {
        findingCell = null;
        // проверить на своей ячейке игрока
        // проверить игрока спереди (findForwardRange)
        // проверить игрока по бокам (findForwardRange / 2)
        // сзади не проверять
        // если игрок найден, то бежать в атаку на него

        return false;
    }
}
