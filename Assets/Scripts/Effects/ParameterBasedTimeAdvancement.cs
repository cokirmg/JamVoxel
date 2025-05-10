using UnityEngine;

// Attach this script to any game object that should influence time progression
public class ParameterBasedTimeAdvancement : MonoBehaviour
{
    [SerializeField] private DayNightCycle _dayNightCycle;

    [Header("Distance-Based Advancement")]
    [SerializeField] private bool _useDistanceBasedAdvancement = false;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _distanceToAdvanceTime = 10f; // How many units to travel before advancing time
    [SerializeField] private float _timeAdvancementPerDistance = 0.01f; // How much time to advance per distance threshold
    private Vector3 _lastPosition;

    [Header("Event-Based Advancement")]
    [SerializeField] private bool _useEventBasedAdvancement = true;

    private void Start()
    {
        if (_dayNightCycle == null)
        {
            _dayNightCycle = FindObjectOfType<DayNightCycle>();
            if (_dayNightCycle == null)
            {
                Debug.LogError("No DayNightCycle found in the scene!");
            }
        }

        if (_useDistanceBasedAdvancement && _playerTransform == null)
        {
            // Try to find the player
            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (_playerTransform == null)
            {
                Debug.LogWarning("No player transform assigned and couldn't find object with 'Player' tag!");
                _useDistanceBasedAdvancement = false;
            }
            else
            {
                _lastPosition = _playerTransform.position;
            }
        }
    }

    private void Update()
    {
        if (_useDistanceBasedAdvancement && _playerTransform != null)
        {
            CheckDistanceAndAdvanceTime();
        }
    }

    private void CheckDistanceAndAdvanceTime()
    {
        float distance = Vector3.Distance(_lastPosition, _playerTransform.position);
        if (distance >= _distanceToAdvanceTime)
        {
            // Calculate how many distance thresholds were crossed
            int steps = Mathf.FloorToInt(distance / _distanceToAdvanceTime);
            _dayNightCycle.AdvanceTime(_timeAdvancementPerDistance * steps);

            // Update last position but keep remaining distance for accumulation
            Vector3 direction = (_playerTransform.position - _lastPosition).normalized;
            _lastPosition = _lastPosition + direction * (_distanceToAdvanceTime * steps);
        }
    }

    // Call these methods from your game events

    public void OnMinigameCompleted(string difficulty = "normal")
    {
        if (!_useEventBasedAdvancement) return;

        // Adjust time advancement based on minigame difficulty
        float timeAdvancement = 0.05f; // Default advancement (5% of a day)

        switch (difficulty.ToLower())
        {
            case "easy":
                timeAdvancement = 0.03f;
                break;
            case "normal":
                timeAdvancement = 0.05f;
                break;
            case "hard":
                timeAdvancement = 0.08f;
                break;
        }

        _dayNightCycle.AdvanceTime(timeAdvancement);
    }

    public void OnRoomDiscovered()
    {
        if (!_useEventBasedAdvancement) return;

        _dayNightCycle.AdvanceTime(0.02f);
    }

    public void OnStoryEventCompleted(int eventImportance)
    {
        if (!_useEventBasedAdvancement) return;

        // eventImportance should be 1-5, with 5 being most important
        float timeAdvancement = 0.03f * eventImportance;
        _dayNightCycle.AdvanceTime(timeAdvancement);
    }

    public void OnCustomEvent(string eventName)
    {
        if (!_useEventBasedAdvancement) return;

        _dayNightCycle.ProgressTimeByEvent(eventName);
    }

    // Set time directly to specific day phases
    public void SetTimeToDawn() => _dayNightCycle.SetTime(0.25f);
    public void SetTimeToNoon() => _dayNightCycle.SetTime(0.5f);
    public void SetTimeToDusk() => _dayNightCycle.SetTime(0.75f);
    public void SetTimeToMidnight() => _dayNightCycle.SetTime(0f);
}