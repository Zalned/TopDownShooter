using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "CardSO", menuName = "ScriptableObjects/CardSO" )]
public class CardSO : ScriptableObject {
    [SerializeField] private string cardName;
    [SerializeField] private List<ModSO> mods = new();
    [SerializeField] private string description;
    [SerializeField] private string statDescription;

    public string Name => cardName;
    public List<ModSO> Effects => mods;
    public string Description => description;
    public string StatDescription => statDescription;
}
