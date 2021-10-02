using DG.Tweening;
using UnityEngine;

public class TargetDisplayUnit : Unit
{
    [SerializeField, Range(0, 5)] private float lifeTime;

    private void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(lifeTime)
            .AppendCallback(() => Die());
    }
}
