using UnityEngine;

[CreateAssetMenu]

public class Vector3Data : ScriptableObject
{
    public Vector3 value;
    public Vector3 shopResetPosition;
    public Vector3 craftingResetPosition;

    public void SetValue(Vector3 v3)
    {
        value = v3;
    }
}