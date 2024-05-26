using UnityEngine;

[CreateAssetMenu(menuName = "Database/UI Data")]
public class UIData : ScriptableObject
{
    [Header("Package Name :")]
    [SerializeField] [TextArea] string _packageName;

    [Header("Privacy Policy Link :")]
    [SerializeField] [TextArea] string _privacyPolicy;

    [Header("Customize Credit Text :")]
    [SerializeField] [TextArea] string _devText;
    [SerializeField] [TextArea] string _sfxText;
    [SerializeField] [TextArea] string _contactText;

    public string PackageName => _packageName;
    public string PrivacyPolicy => _privacyPolicy;

    public string DevText => _devText;
    public string SfxTxt => _sfxText;
    public string ContactText => _contactText;
}
