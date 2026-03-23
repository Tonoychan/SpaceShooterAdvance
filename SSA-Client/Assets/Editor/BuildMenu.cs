using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Editor menu: Build → Android / iOS / Windows. Outputs under &lt;project&gt;/Builds (per-platform subfolder), outside Assets.
/// </summary>
public static class BuildMenu
{
    const string LogPrefix = "[Build]";

    /// <summary>Project root Builds folder (sibling of Assets), e.g. SSA-Client/Builds.</summary>
    static string BuildRoot
    {
        get
        {
            string projectRoot = Path.GetDirectoryName(Application.dataPath)
                ?? Application.dataPath;
            return Path.Combine(projectRoot, "Builds");
        }
    }

    static string[] EnabledScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        var paths = new List<string>();
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].enabled)
                paths.Add(scenes[i].path);
        }

        return paths.ToArray();
    }

    static bool TryBeginBuild(string platformLabel, out string[] scenes)
    {
        scenes = EnabledScenes();
        if (scenes.Length == 0)
        {
            Debug.LogWarning($"{LogPrefix} {platformLabel}: no scenes in Build Settings.");
            EditorUtility.DisplayDialog("Build", "Add at least one scene in File → Build Settings.", "OK");
            return false;
        }

        Debug.Log($"{LogPrefix} --- {platformLabel} build started ---");
        Debug.Log($"{LogPrefix} Product name: {PlayerSettings.productName}");
        Debug.Log($"{LogPrefix} Enabled scenes ({scenes.Length}): {string.Join(", ", scenes)}");
        return true;
    }

    /// <summary>Removes the platform output folder so each build starts clean.</summary>
    static void CleanPlatformOutputFolder(string platformFolderAbsolute)
    {
        Debug.Log($"{LogPrefix} Step: clean output folder → {platformFolderAbsolute}");
        if (Directory.Exists(platformFolderAbsolute))
            Directory.Delete(platformFolderAbsolute, true);
        Directory.CreateDirectory(platformFolderAbsolute);
        Debug.Log($"{LogPrefix} Step: output folder ready (empty).");
    }

    static void LogBuildResult(BuildReport report, string platformLabel, string outputPath)
    {
        var summary = report.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"{LogPrefix} Step: build pipeline finished.");
            Debug.Log(
                $"{LogPrefix} ✓ {platformLabel} build succeeded. Output: {outputPath} | " +
                $"Time: {summary.totalTime} | Size: {summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError(
                $"{LogPrefix} ✗ {platformLabel} build failed. Result: {summary.result} | " +
                $"Errors: {summary.totalErrors} | Warnings: {summary.totalWarnings} | Output: {outputPath}");
        }
    }

    [MenuItem("Build/Android", false, 1)]
    static void BuildAndroid()
    {
        if (!TryBeginBuild("Android", out string[] scenes))
            return;

        string dir = Path.Combine(BuildRoot, "Android");
        CleanPlatformOutputFolder(dir);
        string apk = Path.Combine(dir, PlayerSettings.productName + ".apk");

        Debug.Log($"{LogPrefix} Step: compiling & packaging → {apk}");
        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = apk,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);
        LogBuildResult(report, "Android", apk);
    }

    [MenuItem("Build/iOS", false, 2)]
    static void BuildiOS()
    {
#if UNITY_EDITOR_OSX
        if (!TryBeginBuild("iOS", out string[] scenes))
            return;

        string dir = Path.Combine(BuildRoot, "iOS");
        CleanPlatformOutputFolder(dir);

        Debug.Log($"{LogPrefix} Step: compiling & exporting Xcode project → {dir}");
        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = dir,
            target = BuildTarget.iOS,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);
        LogBuildResult(report, "iOS", dir);
#else
        Debug.LogWarning($"{LogPrefix} iOS: skipped (requires Unity Editor on macOS).");
        EditorUtility.DisplayDialog("Build", "iOS builds require Unity Editor on macOS.", "OK");
#endif
    }

    [MenuItem("Build/Windows", false, 3)]
    static void BuildWindows()
    {
        if (!TryBeginBuild("Windows", out string[] scenes))
            return;

        string dir = Path.Combine(BuildRoot, "Windows");
        CleanPlatformOutputFolder(dir);
        string exe = Path.Combine(dir, PlayerSettings.productName + ".exe");

        Debug.Log($"{LogPrefix} Step: compiling & packaging → {exe}");
        var opts = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = exe,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);
        LogBuildResult(report, "Windows", exe);
    }
}
