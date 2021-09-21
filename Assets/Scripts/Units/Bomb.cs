using UnityEngine;
using DG.Tweening;

public class Bomb : Unit, IUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float timeLife = 2f;
    [SerializeField, Min(1)] private int range;

    [Header("References")]
    [SerializeField] private GameObject dirtySplashPrefab;

    private bool _isLive = true;
    private Sequence _lifeSequence;
    private Field _field;

    private void Start()
    {
        _lifeSequence = DOTween.Sequence();
        _lifeSequence.AppendInterval(timeLife)
            .AppendCallback(() => Detonate());
    }

    public void SetParameters(Field field)
    {
        _field = field;
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

    public new void BecomeDirty()
    {
        Detonate();
    }

    private void Detonate()
    {
        Cell.SpawningUnit(dirtySplashPrefab);   // спавн бомбы под собой
        Cell.PrintUnits();
        Cell.UnitsIsEmpty(new DirtySplash());


        Die();
    }

    private void CalculateBlastWave(Field.Direction direction, int multiplier)
    {
        for (int i = 1; i < range; i++)
        {
            Cell currentCell = _field.GiveCell(Cell, direction, i * multiplier);
        }
    }
}
