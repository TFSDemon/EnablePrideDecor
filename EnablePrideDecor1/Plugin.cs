using BepInEx;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnablePrideDecor
{
    [BepInPlugin("com.emily.enablepridedecor", "Enable Pride Decor", "1.0.0")]
    public class EnablePrideDecor : BaseUnityPlugin
    {
        private readonly HashSet<string> processedScenes = new HashSet<string>();

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
            if (processedScenes.Contains(scene.name))
                return;

            bool foundPrideObjects = false;

            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (obj.name == "PrideObjects" && obj.scene == scene)
                {
                    foundPrideObjects = true;
                    EnableRecursively(obj.transform);
                }
            }

           
            if (foundPrideObjects)
            {
                processedScenes.Add(scene.name);
            }
        }

        private void EnableRecursively(Transform parent)
        {
            parent.gameObject.SetActive(true);

            foreach (Transform child in parent)
            {
                EnableRecursively(child);
            }
        }
    }
}