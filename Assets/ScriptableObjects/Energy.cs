using UnityEngine;

[CreateAssetMenu(fileName = "Energy", menuName = "ScriptableObjects/Energy", order = 1)]
public class Energy : ScriptableObject
{
    [Range(0, 1)]
    public float energy = 1.0f;
    [Range(0, 1)]
    public float cost = 0.0f;
}
