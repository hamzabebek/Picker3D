using UnityEngine;

public class OnLevelLoaderCommand
{
    private Transform _levelHolder;

    internal OnLevelLoaderCommand(Transform levelHolder)
    {
        _levelHolder = levelHolder;
    }

    internal void Execute(byte levelIndex) {
        var levelObject = Object.Instantiate(
            Resources.Load<GameObject>($"Prefab/LevelPrefabs/level {levelIndex}"), _levelHolder, true);
    }
}
