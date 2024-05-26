using UnityEngine;

public class RimGenerator : MonoBehaviour
{
    private const float _ballDistanceFromLastRim = 20f;

    [SerializeField] Transform _firstRimPrefabs;
    [SerializeField] Transform _ball;

    [Space]
    [SerializeField] float _gamBetweenRim = 4f;
    [SerializeField] float _rimOffset = 1.25f;

    float minMaxYPos = 2f;
    Vector2 _lastHoopPosition;

    [Header("Game Data:")]
    [SerializeField] GameplayData _data;

    int _rimSpawnIndex = 0;
    Transform _selectedRim;

    private void Start()
    {
        _selectedRim = _firstRimPrefabs;

        // calculate first hoop position
        _lastHoopPosition.x += _rimOffset;

        SpawnRim(_lastHoopPosition, _firstRimPrefabs);
    }

    private void Update()
    {
        if (Vector2.SqrMagnitude(_lastHoopPosition - (Vector2)_ball.position) < (_ballDistanceFromLastRim * _ballDistanceFromLastRim))
        {
            CalculateDifficulty();

            SpawnRim(_lastHoopPosition, _selectedRim);
        }
    }

    private void SpawnRim(Vector2 position, Transform rimTransform)
    {
        // generate new rim
        Transform newRim = Instantiate(rimTransform, transform);
        newRim.position = position;

        _rimSpawnIndex++;
        AddRimGap();
    }

    // calculate distance between hoop
    private void AddRimGap()
    {
        _lastHoopPosition.x += _gamBetweenRim;
        _lastHoopPosition.y = Random.Range(-minMaxYPos + .5f, minMaxYPos);
    }

    private void CalculateDifficulty()
    {
        if (_rimSpawnIndex <= 3)
        {
            _selectedRim = _data.GetEasyRim;
        }
        else if (_rimSpawnIndex > 3 && _rimSpawnIndex <= 10)
        {
            _selectedRim = _data.GetNormalRim;
        }
        else if (_rimSpawnIndex > 10 && _rimSpawnIndex <= 20)
        {
            minMaxYPos = 2.5f;
            _selectedRim = _data.GetHardRim;
        }
        else
        {
            minMaxYPos = 3.5f;
            _selectedRim = _data.GetExpertRim;
        }
    }
}
