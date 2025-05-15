using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;

    #endregion

    #region Private Variables
    private OnLevelLoaderCommand _levelLoaderCommand;
    private OnLevelDestroyerCommand _levelDestroyerCommand;

    private short _currentLevel;
    private LevelData _levelData;
    #endregion
    #endregion

    private void Awake()
    {
        _levelData = GetLevelData();
        _currentLevel = GetActiveLevel();
        Debug.Log(_currentLevel);
        Debug.Log(_levelData.Pools.Count);
        Init();
    }


    private void Init()
    {
        _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
        _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
    }

    private LevelData GetLevelData()
    {
        var cdLevel = Resources.Load<CD_Level>("Data/CD_Level");
        if (cdLevel == null)
        {
            Debug.LogError("CD_Level asset not found at path: Data/CD_Level");
        }

        if (cdLevel.Levels == null || cdLevel.Levels.Count == 0)
        {
            Debug.LogError("CD_Level.Levels is empty!");
        }

        if (_currentLevel >= cdLevel.Levels.Count)
        {
            Debug.LogError($"_currentLevel ({_currentLevel}) is out of bounds! Levels.Count: {cdLevel.Levels.Count}");
        }

        return cdLevel.Levels[_currentLevel];
    }


    private byte GetActiveLevel()
    {
        return (byte)_currentLevel;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }
    private void OnNextLevel()
    {
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }

    private void OnRestartLevel()
    {
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }

    private byte OnGetLevelValue()
    {
        return (byte)((byte)_currentLevel % totalLevelCount);
    }

    private void UnSubscribeEvents()
    {

        CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void Start()
    {
        Debug.Log($"LevelManager Start Current Level : {_currentLevel} Total Level Count : {totalLevelCount}");
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
    }
}
