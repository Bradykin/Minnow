using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Util
{
    public static class SceneLoader
    {
        private static readonly Dictionary<string, SceneRoot> SCENES = new Dictionary<string, SceneRoot>
        {
            { "MainMenuScene", null },
            { "LevelScene", null },
            { "AlexTestScene", null },
            { "NickTestScene", null },
            { "LevelSelectScene", null },
            { "LevelCreatorScene", null }
        };

        private static MonoBehaviour _coroutineRunner;
        private static bool _sceneLoaderReady;
        
        //============================================================================================================//

        private static string currentScene;
        private static string lastScene;

        public static string CurrentScene => currentScene;
        

        //============================================================================================================//
        
        public static void SubscribeSceneRoot(SceneRoot sceneRoot, string sceneName)
        {
            if (!_sceneLoaderReady)
            {
                SetupSceneLoader(sceneRoot, sceneName);
                return;
            }

            if (SCENES[sceneName] != null) 
                return;
            
            SCENES[sceneName] = sceneRoot;
        }

        private static void SetupSceneLoader(SceneRoot sceneRoot, string sceneName)
        {
            _sceneLoaderReady = true;

            if (_coroutineRunner == null)
                _coroutineRunner = sceneRoot;

            SubscribeSceneRoot(sceneRoot, sceneName);

            currentScene = sceneName;
            lastScene = string.Empty;

            _coroutineRunner.StartCoroutine(Startup());
        }
        
        //============================================================================================================//

        public static bool SetActiveScene(string sceneName)
        {
            lastScene = currentScene;
            currentScene = sceneName;
            
            return SCENES.ContainsKey(sceneName) && SceneManager.SetActiveScene(SCENES[sceneName].Scene);
        }

        public static bool ActivateScene(string sceneName)
        {
            lastScene = currentScene;
            currentScene = sceneName;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            
            return SetSceneObjectsActive(sceneName, true);
        }

        public static bool DeactivateScene(string sceneName)
        {
            return SetSceneObjectsActive(sceneName, false);
        }

        public static bool ActivateScene(string sceneName, string sceneNameToDeload, bool updateJsonData = false)
        {
            lastScene = sceneNameToDeload;
            currentScene = sceneName;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            
            SetSceneObjectsActive(sceneNameToDeload, false);
            
            return SetSceneObjectsActive(sceneName, true);
        }

        public static bool ResetCurrentScene()
        {
            return SetSceneObjectsActive(currentScene, true);
        }

        public static bool LoadPreviousScene()
        {
            return !string.IsNullOrEmpty(lastScene) && ActivateScene(lastScene, currentScene);
        }
        
        //============================================================================================================//
        
        private static IEnumerator Startup()
        {
            List<string> enumerationKeys = SCENES.Keys.ToList();
            
            foreach (string entry in enumerationKeys)
            {
                yield return _coroutineRunner.StartCoroutine(LoadSceneAsync(entry));
            }

            ActivateScene(SceneManager.GetActiveScene().name);
        }

        private static IEnumerator LoadSceneAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).IsValid())
            {
                yield break;
            }

            AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncLoadLevel.isDone)
            {
                yield return null;
            }

            while(SCENES[sceneName] == null || !SCENES[sceneName].IsStarted)
            {
                yield return null;
            }

            DeactivateScene(sceneName);
        }
        
        //============================================================================================================//

        private static bool SetSceneObjectsActive(string sceneName, bool active)
        {
            if (SCENES.ContainsKey(sceneName) && SCENES[sceneName] != null)
            {
                SCENES[sceneName].SetSceneObjectsActive(active);
                return true;
            }
            
            Debug.Log("Attempted to set scene active that is not loaded");
            return false;
        }
        
        //============================================================================================================//
    }
}