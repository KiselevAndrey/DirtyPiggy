using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Move sprites")]
    [SerializeField] private Sprite moveUp;
    [SerializeField] private Sprite moveDown;
    [SerializeField] private Sprite moveLeft;
    [SerializeField] private Sprite moveRigth;

    public void ChangeSprite(KAP.Helper.Direction.Directions direction)
    {
        switch (direction)
        {
            case KAP.Helper.Direction.Directions.Up:
                spriteRenderer.sprite = moveUp;
                break;
            case KAP.Helper.Direction.Directions.Down:
                spriteRenderer.sprite = moveDown;
                break;
            case KAP.Helper.Direction.Directions.Left:
                spriteRenderer.sprite = moveLeft;
                break;
            case KAP.Helper.Direction.Directions.Right:
                spriteRenderer.sprite = moveRigth;
                break;
        }
    }
}
