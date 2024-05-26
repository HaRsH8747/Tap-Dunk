using UnityEngine;

[CreateAssetMenu(menuName = "Database/Gameplay Data")]
public class GameplayData : ScriptableObject
{
    [Header("Hoop/Rim Prefabs Variant :")]
    [SerializeField] Transform[] _easyList;
    [SerializeField] Transform[] _normalList;
    [SerializeField] Transform[] _hardList;
    [SerializeField] Transform[] _expertList;

    public Transform GetEasyRim => _easyList[Random.Range(0, _easyList.Length)];
    public Transform GetNormalRim => _normalList[Random.Range(0, _normalList.Length)];
    public Transform GetHardRim => _hardList[Random.Range(0, _hardList.Length)];
    public Transform GetExpertRim => _expertList[Random.Range(0, _expertList.Length)];
}