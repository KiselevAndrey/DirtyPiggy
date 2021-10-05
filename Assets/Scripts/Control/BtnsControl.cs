using UnityEngine;

public class BtnsControl : MonoBehaviour
{
    public static System.Action MoveUpAction;
    public static System.Action MoveDownAction;
    public static System.Action MoveLeftAction;
    public static System.Action MoveRightAction;
    public static System.Action PlantBombAction;

    public void MoveUp() => MoveUpAction?.Invoke();
    public void MoveDown() => MoveDownAction?.Invoke();
    public void MoveLeft() => MoveLeftAction?.Invoke();
    public void MoveRight() => MoveRightAction?.Invoke();
    public void PlantBomb() => PlantBombAction?.Invoke();
}
