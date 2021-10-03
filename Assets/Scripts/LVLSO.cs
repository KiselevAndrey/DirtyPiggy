using UnityEngine;

[CreateAssetMenu(fileName = "LVLSO")]
public class LVLSO : ScriptableObject
{
    [Header("Counts")]
    [SerializeField, Min(1)] private int minGabbageCount;
    [SerializeField, Min(1)] private int maxGabbageCount;
    [SerializeField, Min(0)] private int dogCount;
    [SerializeField, Min(0)] private int fermerCount;

    #region Properties
    public int GabbageCount { get => Random.Range(minGabbageCount, maxGabbageCount + 1); }
    public int DogCount { get => dogCount; }
    public int FermerCount { get => fermerCount; }
    #endregion


}
