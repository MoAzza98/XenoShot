using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptableObjectEditorWindow : EditorWindow {
    ScriptableObjectEditorWorkspace[] workspaces;
    List<string> workspaceNames = new List<string>();
    int currentWorkspace;

    ScriptableObjectEditorWorkspace editorConfiguration;

    List<Editor> scriptableObjectEditors = new List<Editor>();
    List<string> tabNames = new List<string>();
    int currentTab;
    Vector2 scrollPosition;

    [MenuItem("TheKiwiCoder/Scriptable Object Editor ...")]
    public static void ShowWindow() {
        ScriptableObjectEditorWindow window = GetWindow<ScriptableObjectEditorWindow>();
        window.titleContent = new GUIContent("Scriptable Object Editor");
    }

    ScriptableObjectEditorWorkspace[] FindWorkspaces() {
        string[] assetIds = AssetDatabase.FindAssets("t:ScriptableObjectEditorWorkspace");
        List<ScriptableObjectEditorWorkspace> workspaces = new List<ScriptableObjectEditorWorkspace>();
        foreach (var assetId in assetIds) {
            string path = AssetDatabase.GUIDToAssetPath(assetId);
            ScriptableObjectEditorWorkspace asset = AssetDatabase.LoadAssetAtPath<ScriptableObjectEditorWorkspace>(path);
            workspaces.Add(asset);
        }
        return workspaces.ToArray();
    }

    void RefreshWorkspaces() {
        if (workspaces == null ||
            workspaces.Length == 0) {
            workspaces = FindWorkspaces();
            workspaceNames = new List<string>();

            foreach (var workspace in workspaces) {
                workspaceNames.Add(workspace.displayName);
            }
        }
    }

    private void OnGUI() {
        RefreshWorkspaces();
        if (workspaces == null || workspaces.Length == 0) {
            return;
        }

        currentWorkspace = EditorGUILayout.Popup(currentWorkspace, workspaceNames.ToArray());

        int previousWorkspace = currentWorkspace;
        editorConfiguration = workspaces[currentWorkspace];

        if (previousWorkspace != currentWorkspace) {
            scriptableObjectEditors = new List<Editor>();
        }

        if (!editorConfiguration) {
            return;
        }

        CreateEditors();
        DrawEditors();

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Refresh")) {
            ForceRefresh();
        }
    }

    void ForceRefresh() {
        workspaces = null;
        scriptableObjectEditors = null;
        tabNames = null;
        // Default to first real workspace
        currentWorkspace = 1;
    }

    void CreateEditors() {
        if (scriptableObjectEditors == null ||
            scriptableObjectEditors.Count != editorConfiguration.tabs.Count || GUI.changed) {
            scriptableObjectEditors = new List<Editor>();
            tabNames = new List<string>();

            foreach (var tab in editorConfiguration.tabs) {
                Editor editor = null;
                Editor.CreateCachedEditor(tab.scriptableObject, null, ref editor);
                scriptableObjectEditors.Add(editor);
                tabNames.Add(tab.tabName);
            }
        }
    }

    void DrawEditors() {
        currentTab = Mathf.Clamp(currentTab, 0, scriptableObjectEditors.Count - 1);

        if (scriptableObjectEditors.Count == 0) {
            return;
        }

        currentTab = GUILayout.Toolbar(currentTab, tabNames.ToArray());
        Editor currentEditor = scriptableObjectEditors[currentTab];

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        if (currentEditor) {
            currentEditor.OnInspectorGUI();
        }
        EditorGUILayout.EndScrollView();
    }
}
