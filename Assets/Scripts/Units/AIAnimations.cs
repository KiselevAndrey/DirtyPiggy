using UnityEngine;

public class AIAnimations : Animations
{
    [Header("Dirty sprites")]
    [SerializeField] private Sprite moveUpDirty;
    [SerializeField] private Sprite moveDownDirty;
    [SerializeField] private Sprite moveLeftDirty;
    [SerializeField] private Sprite moveRigthDirty;

    [Header("Angry sprites")]
    [SerializeField] private Sprite moveUpAngry;
    [SerializeField] private Sprite moveDownAngry;
    [SerializeField] private Sprite moveLeftAngry;
    [SerializeField] private Sprite moveRigthAngry;

    public void ChangeDirtySprite(KAP.Helper.Direction.Directions direction)
    {
        switch (direction)
        {
            case KAP.Helper.Direction.Directions.Up:
                spriteRenderer.sprite = moveUpDirty;
                break;
            case KAP.Helper.Direction.Directions.Down:
                spriteRenderer.sprite = moveDownDirty;
                break;
            case KAP.Helper.Direction.Directions.Left:
                spriteRenderer.sprite = moveLeftDirty;
                break;
            case KAP.Helper.Direction.Directions.Right:
                spriteRenderer.sprite = moveRigthDirty;
                break;
        }
    }

    public void ChangeAngrySprite(KAP.Helper.Direction.Directions direction)
    {
        switch (direction)
        {
            case KAP.Helper.Direction.Directions.Up:
                spriteRenderer.sprite = moveUpAngry;
                break;
            case KAP.Helper.Direction.Directions.Down:
                spriteRenderer.sprite = moveDownAngry;
                break;
            case KAP.Helper.Direction.Directions.Left:
                spriteRenderer.sprite = moveLeftAngry;
                break;
            case KAP.Helper.Direction.Directions.Right:
                spriteRenderer.sprite = moveRigthAngry;
                break;
        }
    }
}
