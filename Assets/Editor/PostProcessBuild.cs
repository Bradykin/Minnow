using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game.Util
{
    public static class PostProcessBuild
    {
        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            var buildDirectory = new DirectoryInfo(pathToBuiltProject).Parent;

            var dataDirectory = Path.Combine(buildDirectory.FullName, GameFiles.BUILD_PATH);
            Debug.Log($"Moving files from {GameFiles.EDITOR_PATH} to {buildDirectory}");

            var editorDirectory = new DirectoryInfo(GameFiles.EDITOR_PATH);
            var files = editorDirectory.GetFiles("*.txt");


            if (!Directory.Exists(dataDirectory))
                buildDirectory = Directory.CreateDirectory(dataDirectory);

            foreach (var file in files)
            {
                var path = Path.Combine(buildDirectory.FullName, file.Name);
                file.CopyTo(path, true);

                Debug.Log($"Copied {file.Name} to {path}");
            }
        }
    }
}
