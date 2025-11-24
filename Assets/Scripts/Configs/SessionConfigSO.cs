using UnityEngine;

[CreateAssetMenu( fileName = "SessionConfig", menuName = "Scriptable Objects/SessionConfig" )]
public class SessionConfigSO : ScriptableObject {
    public int RoundForWin;
    public int CountBonusToPick;
    public int TimeToPickBonus;
}
