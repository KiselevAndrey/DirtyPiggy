using UnityEngine;
using KAP.Helper;
using DG.Tweening;

public class AIMoving : MovingUnit, IMovingUnit
{
    [Header("AI Parameters")]
    [Tooltip("The number of cells, after how long the player will be detected")]
    [SerializeField, Min(1)] private int findForwardRange = 2;
    [Tooltip("The percentage of time from a normal transition that is spent on an attack")]
    [SerializeField, Range(0.2f, 1f)] private float reducingTransitionTime = 0.5f;
    [SerializeField, Min(0)] private float maxIdleTime = 5f;
    [SerializeField] private bool showFindingArea;

    [Header("AI References")]
    [SerializeField] private AIAnimations animations;
    [SerializeField] private GameObject targetDisplayPrefab;
        
    private System.Type _pigType;
    private Sequence _waitSequence;
    private bool _isRunToHome;

    #region Awake OnDestroy
    private void Awake()
    {
        _pigType = new Pig().GetType();

        Pig.MovingAction += FindPigAroundAction;
    }

    private void OnDestroy()
    {
        Pig.MovingAction -= FindPigAroundAction;
    }
    #endregion

    #region Override
    protected override void EndMoving()
    {
        base.EndMoving();

        if (_isRunToHome)
        {
            RunToHome();
        }
        else
        {
            // Check pig on this Cell
            if (Cell.TryFindUnit(_pigType, out IUnit pig))
            {
                pig.Die();
                return;
            }

            animations.ChangeSprite(_forwardDirection);

            FindPigAround();

            WaitToNextMove(Random.Range(maxIdleTime / 2, maxIdleTime));
        }
    }

    public override void MoveToStartPosition()
    {
        base.MoveToStartPosition();
        _forwardDirection = Direction.Directions.Left;
        animations.ChangeSprite(Direction.Left);
        _isRunToHome = false;
    }

    public override void BecomeDirty()
    {
        _isRunToHome = true;
        _waitSequence.Kill();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => animations.ChangeDirtySprite(_forwardDirection))
            .AppendInterval(1f)
            .AppendCallback(() => animations.ChangeDirtySprite(Direction.Right))
            .AppendCallback(() => RunToHome());       
    }
    #endregion

    #region FindPlayer
    private void FindPigAroundAction()
    {
        FindPigAround();
    }

    private bool FindPigAround()
    {
        if (!IsMoving && TryFindPlayer(out Cell playerCell))
        {
            // if the player is found, then run to attack him
            MoveTo(playerCell, TimeToRelocate * reducingTransitionTime * Field.Distance(playerCell, Cell));
            _waitSequence.Kill();
            return true;
        }

        return false;
    }

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

        for (int i = 1; i <= range; i++)
        {
            findingCell = Field.singleton.GiveCell(Cell, direction, i);
            if (findingCell != null)
            {
                if (showFindingArea) findingCell.SpawningUnit(targetDisplayPrefab, false);
                if (findingCell.TryFindUnit(_pigType))
                {
                    animations.ChangeAngrySprite(direction);
                    _forwardDirection = direction;
                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    #region NextMove
    private void WaitToNextMove(float endTime)
    {
        _waitSequence.Kill();
        _waitSequence = DOTween.Sequence();
        _waitSequence.AppendInterval(endTime)
            .AppendCallback(() =>
            {
                FindNewMoveDirection();
            });
    }

    private void FindNewMoveDirection()
    {
        Direction.Directions direction = Direction.Random();
        Cell nextCell = Field.singleton.GiveCell(Cell, direction, 1);

        if (nextCell != null)
        {
            animations.ChangeSprite(direction);
            MoveTo(nextCell, direction);
        }
        else
        {
            FindNewMoveDirection();
        }
    }
    #endregion

    #region BecomeDirty
    private void RunToHome()
    {
        Cell nextCell = Field.singleton.GiveCell(Cell, Direction.Right, 1);
        if (nextCell != null) MoveTo(nextCell, Direction.Right);
        else MoveToHomePosition();
    }

    private void MoveToHomePosition()
    {
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.AppendCallback(() => Cell.RemoveUnit(this))
            .Append(transform.DOMove(HomeCell.Position, startTimeToRelocate))
            .AppendCallback(() => Cell = null)
            .AppendInterval(maxIdleTime)
            .AppendCallback(() => MoveToStartPosition());
    }
    #endregion
}
