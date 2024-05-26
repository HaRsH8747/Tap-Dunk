using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _creditButton;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _settingsButton;

    [Header("Database References :")]
    [SerializeField] UIData _data;

    private void Start()
    {
        OnButtonPressed(_creditButton, CreditButtonPressed);
        OnButtonPressed(_rateButton, RateButtonPressed);
        OnButtonPressed(_settingsButton, SettingsButtonPressed);
    }

    private void SettingsButtonPressed()
    {
        MenuController.GetInstance().OpenMenu(MenuType.Setting);
    }

    private void RateButtonPressed()
    {
        Application.OpenURL($"market://details?id={_data.PackageName}");
    }

    private void CreditButtonPressed()
    {
        MenuController.GetInstance().OpenMenu(MenuType.Credit);
    }
}
