using UnityEngine;
using UnityEditor;

public class MissingScriptFinder
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void FindMissingScripts()
    {
        GameObject[] goArray = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject g in goArray)
        {
            Component[] components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script found in: {g.name}", g);
                }
            }
        }
    }
}
