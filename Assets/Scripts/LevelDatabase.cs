using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    [Tooltip("Список имён сцен уровней в порядке прогрессии (как в Build Settings).")]
    public string[] levelSceneNames;

    public int LevelCount => levelSceneNames != null ? levelSceneNames.Length : 0;

    public string GetSceneNameByIndex(int index)
    {
        if (levelSceneNames == null || index < 0 || index >= levelSceneNames.Length) return null;
        return levelSceneNames[index];
    }
}
