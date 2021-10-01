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

    #region Override
    protected override void EndMoving()
    {
        base.EndMoving();
        TryFindPlayer(out Cell playerCell);
    }

    public override void MoveToStartPosition()
    {
        base.MoveToStartPosition();
        _forwardDirection = KAP.Helper.Direction.Directions.Left;
    }
    #endregion

    private bool TryFindPlayer(out Cell findingCell)
    {
        findingCell = null;

        // check pig on Cell
        if (Cell.TryFindUnit(_pigType))
        {
            findingCell = Cell;
            return true;
        }

        // проверить игрока спереди (findForwardRange)
        // проверить игрока по бокам (findForwardRange / 2)
        // сзади не проверять
        // если игрок найден, то бежать в атаку на него

        return false;
    }
}
