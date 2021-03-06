﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class SceneRootExtensions
    {
        public static void SetSceneObjectsActive(this SceneRoot sceneRoot, bool state, bool shouldTriggerReset = true)
        {
            foreach (GameObject gameObject in sceneRoot.Scene.GetRootGameObjects())
            {
                if (state)
                    gameObject.SetActive(true);

                if (!shouldTriggerReset)
                    continue;

                IReset[] resets = gameObject.GetComponents<IReset>();
                foreach (IReset reset in resets)
                {
                    if (state)
                        reset.Activate();
                    else
                        reset.Reset();
                }

                if (!state)
                    gameObject.SetActive(false);
            }
        }
    }
}
