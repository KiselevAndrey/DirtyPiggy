using UnityEngine;
using DG.Tweening;
using KAP.Helper;

public class Bomb : Unit, IUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float timeLife = 2f;
    [SerializeField, Min(1)] private int range;
    [SerializeField, Min(0)] private float blastWaveDelay = 0.2f;

    [Header("References")]
    [SerializeField] private GameObject dirtySplashPrefab;

    private bool _isLive;
    private Sequence _lifeSequence;

    private void OnEnable()
    {
        _isLive = true;

        _lifeSequence = DOTween.Sequence();
        _lifeSequence.AppendInterval(timeLife)
            .AppendCallback(() => Detonate());
    }

    #region From IUnit
    public override void BecomeDirty()
    {
        Detonate();
    }

    private new void Die()
    {
        _lifeSequence.Kill();
        if (_isLive)
        {
            _isLive = false;
            base.Die();
        }
    }
    #endregion

    #region Detonate
    private void Detonate()
    {
        Cell.SpawningUnit(dirtySplashPrefab);
        CalculateBlastWave(Direction.Up, 1);
        CalculateBlastWave(Direction.Up, -1);
        CalculateBlastWave(Direction.Right, 1);
        CalculateBlastWave(Direction.Right, -1);

        Die();
    }

    private void CalculateBlastWave(Direction.Directions direction, int multiplier, int waveIndex = 1)
    {
        Cell currentCell = Field.singleton.GiveCell(Cell, direction, waveIndex * multiplier);
        Sequence blastWaveSequence = DOTween.Sequence();
        blastWaveSequence.AppendInterval(blastWaveDelay)
            .AppendCallback(() => currentCell.SpawningUnit(dirtySplashPrefab))
            .AppendCallback(() =>
            {
                // checking that the wave can be launched further
                if (waveIndex < range && currentCell && currentCell.UnitsIsEmpty(new DirtySplash()))
                    CalculateBlastWave(direction, multiplier, ++waveIndex);
            });
    }
    #endregion
}
