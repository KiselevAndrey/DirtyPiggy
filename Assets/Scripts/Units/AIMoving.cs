using UnityEngine;
using KAP.Helper;

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

        // Check pig on this Cell
        if (Cell.TryFindUnit(_pigType, out IUnit unit))
        {
            unit.Die();
            return;
        }

        // Find Pig around
        if (TryFindPlayer(out Cell playerCell))
        {
            // if the player is found, then run to attack him
            MoveTo(playerCell, TimeToRelocate * reducingTransitionTime);
        }
    }

    public override void MoveToStartPosition()
    {
        base.MoveToStartPosition();
        _forwardDirection = Direction.Directions.Left;
    }
    #endregion

    #region TryFindPlayer
    /// <summary> Search for a player across the entire front and half of the sides. Don't check the back </summary>
    private bool TryFindPlayer(out Cell findingCell)
    {
        // check the player from the front (findForwardRange)
        if (CheckPlayer(_forwardDirection, findForwardRange, out findingCell))
            return true;

        // check the player on the sides (findForwardRange / 2)
        if (CheckPlayer(Direction.GiveRightDirection(_forwardDirection), findForwardRange / 2, out findingCell))
            return true;

        if (CheckPlayer(Direction.GiveLeftDirection(_forwardDirection), findForwardRange / 2, out findingCell))
            return true;

        return false;
    }

    private bool CheckPlayer(Direction.Directions direction, int range, out Cell findingCell)
    {
        findingCell = null;

        for (int i = 1; i < range; i++)
        {
            findingCell = Field.singleton.GiveCell(Cell, direction, i);
            if (findingCell.TryFindUnit(_pigType)) 
                return true;
        }

        return false;
    }
    #endregion
}
