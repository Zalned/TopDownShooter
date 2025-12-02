using UnityEngine;

[CreateAssetMenu( fileName = "SessionConfig", menuName = "Scriptable Objects/SessionConfig" )]
public class SessionConfigSO : ScriptableObject {
    [SerializeField] private int _roundForWin;
    [SerializeField] private int _countCardToChoose;
    [SerializeField] private int _timeToPickBonus;

    public int RoundForWin => _roundForWin;
    public int CountCardToChoose => _countCardToChoose;
    public int TimeToPickBonus => _timeToPickBonus;
}
