using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _yesButton;
    [SerializeField] Button _noButton;

    private void OnEnable()
    {
        _noButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_yesButton, YesButtonPressed);
        OnButtonPressed(_noButton, NoButtonPressed);
    }

    private void NoButtonPressed()
    {
        _noButton.interactable = false;

        MenuController.GetInstance().CloseMenu();
    }

    private void YesButtonPressed()
    {
        Application.Quit();
    }


}
