using UnityEngine;
using DG.Tweening;

public class Bomb : Unit, IUnit
{
    [Header("Parameters")]
    [SerializeField, Min(0)] private float timeLife = 2f;

    private bool _isLive = true;
    private Sequence _lifeSequence;

    private void Start()
    {
        _lifeSequence = DOTween.Sequence();
        _lifeSequence.AppendInterval(timeLife)
            .AppendCallback(() => Die());
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
}
