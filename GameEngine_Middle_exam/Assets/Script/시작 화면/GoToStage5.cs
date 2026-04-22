using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GoToStage5 : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset scene; 
#endif

    int sceneIndex;

#if UNITY_EDITOR
    void OnValidate()
    {
        if (scene != null)
        {
            string path = AssetDatabase.GetAssetPath(scene);
            sceneIndex = SceneUtility.GetBuildIndexByScenePath(path);
        }
    }
#endif

    public void Go()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}