using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(MenuTweening))]
public abstract class Menu : MonoBehaviour
{
    [SerializeField] bool _useAnimation;

    MenuTweening _menuTweening;
    public MenuType _type;

    protected virtual void Awake()
    {
        _menuTweening = GetComponent<MenuTweening>();
    }

    public void SetDisable()
    {
        if (!_useAnimation)
        {
            gameObject.SetActive(false);
            return;
        }

        // disable game object after tweening
        _menuTweening.SetDisableGameobject();
    }

    protected void OnButtonPressed(Button button, UnityAction buttonListener)
    {
        if (!button)
        {
            Debug.LogWarning($"There is a 'Button' that is not attached to the '{gameObject.name}' script,  but a script is trying to access it.");
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(buttonListener);
    }
}
