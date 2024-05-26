using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    public Menu[] _menus;

    private Hashtable _menuTable = new Hashtable();
    private Stack<Menu> _menuStack = new Stack<Menu>();

    private MenuType _currentMenu;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    private void Update()
    {
        GetMobileInput();
    }

    private void Init()
    {
        RegisterAllMenus();
        OpenMenu(MenuType.Main);
    }

    #region Public Functions
    public void SwitchMenu(MenuType type)
    {
        CloseMenu();    // Disable the last menu
        OpenMenu(type); // Open desired menu
    }

    public void OpenMenu(MenuType type)
    {
        if (type == MenuType.None) return;
        if (!MenuExist(type))
        {
            Debug.LogWarning($"You are trying to open a Menu {type} that has not been registered.");
            return;
        }

        Menu menu = GetMenu(type);
        menu.gameObject.SetActive(true);
        _menuStack.Push(menu);

        _currentMenu = menu._type;
    }

    public void CloseMenu()
    {
        if (_menuStack.Count <= 0)
        {
            Debug.LogWarning("MenuController CloseMenu ERROR: No menus in stack!");
            return;
        }
        Menu lastMenuStack = _menuStack.Pop();

        // Disable GameObject
        lastMenuStack.SetDisable();

        if (_menuStack.Count > 0)
            _currentMenu = _menuStack.Peek()._type;
    }
    #endregion

    #region Private Functions
    private void RegisterAllMenus()
    {
        foreach (Menu menu in _menus)
        {
            RegisterMenu(menu);

            // disable menu after register to hash table.
            menu.gameObject.SetActive(false);
        }
        Debug.Log("Successfully registered all menus.");
    }

    private void RegisterMenu(Menu menu)
    {
        if (menu._type == MenuType.None)
        {
            Debug.LogWarning($"You are trying to register a {menu._type} type menu that has not allowed.");
            return;
        }

        if (MenuExist(menu._type))
        {
            Debug.LogWarning($"You are trying to register a Menu {menu._type} that has already been registered.");
            return;
        }

        _menuTable.Add(menu._type, menu);
    }

    private Menu GetMenu(MenuType type)
    {
        if (!MenuExist(type)) return null;

        return (Menu)_menuTable[type];
    }

    private bool MenuExist(MenuType type)
    {
        return _menuTable.ContainsKey(type);
    }

    //Mobile Input
    private void GetMobileInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentMenu == MenuType.Main)
            {
                OpenMenu(MenuType.Exit);
            }
            else if (_currentMenu == MenuType.Setting || _currentMenu == MenuType.Credit)
            {
                CloseMenu();
            }
        }
    }
    #endregion
}
