using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private float _splashDelay = 2f;
    [SerializeField] private float _loadingIconRotateSpeed = 200f;
    [SerializeField] private Transform _loadingIcon = null;
    [SerializeField] private GameObject GDPR;

    [Header("Database References :")]
    [SerializeField] UIData _data;

    private void Start()
    {
        CheckForGDPR();
        Invoke("StartGame", _splashDelay);
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
                PlayerPrefs.DeleteAll();
        }

        RotateLoadingIcon();
    }

    private void RotateLoadingIcon()
    {
        _loadingIcon.Rotate(Vector3.forward * Time.deltaTime * -_loadingIconRotateSpeed, Space.World);
    }

    private void StartGame()
    {
        LevelLoader.LoadLevel(1);
    }

    private void CheckForGDPR()
    {
        if(PlayerPrefs.GetInt("npa", -1) == -1)
        {
            //show gdpr popup
            GDPR.SetActive(true);

            //pause the game
            Time.timeScale = 0;
        }
    }

    //Popup events
    public void OnUserClickAccept()
    {
        PlayerPrefs.SetInt("npa", 0);

        //hide gdpr popup
        GDPR.SetActive(false);

        //play the game
        Time.timeScale = 1;
    }
    public void OnUserClickCancel()
    {
        PlayerPrefs.SetInt("npa", 1);

        //hide gdpr popup
        GDPR.SetActive(false);

        //play the game
        Time.timeScale = 1;
    }
    public void OnUserClickPrivacyPolicy()
    {
        Application.OpenURL(_data.PrivacyPolicy);
    }
}
