using UnityEditor;
using UnityEngine;
using System.IO;

public class JSONEditorWindow : EditorWindow
{
    private StageData stageData;
    private string filePath = "Assets/StreamingAssets/stagedata.json";

    [MenuItem("Tools/JSON Editor")]
    public static void ShowWindow()
    {
        GetWindow<JSONEditorWindow>("JSON Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("JSON Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Load JSON"))
        {
            LoadJSON();
        }

        if (stageData != null)
        {
            foreach (var stage in stageData.stages)
            {
                EditorGUILayout.BeginVertical("box");
                stage.stageId = EditorGUILayout.IntField("Stage ID", stage.stageId);

                foreach (var room in stage.rooms)
                {
                    EditorGUILayout.BeginVertical("box");
                    room.roomId = EditorGUILayout.TextField("Room ID", room.roomId);
                    room.type = EditorGUILayout.TextField("Room Type", room.type);

                    for (int i = 0; i < room.connectedRooms.Length; i++)
                    {
                        room.connectedRooms[i] = EditorGUILayout.TextField($"Connected Room {i + 1}", room.connectedRooms[i]);
                    }

                    if (GUILayout.Button("Add Connected Room"))
                    {
                        ArrayUtility.Add(ref room.connectedRooms, "");
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
            }
        }

        if (GUILayout.Button("Save JSON"))
        {
            SaveJSON();
        }
    }

    private void LoadJSON()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            stageData = JsonUtility.FromJson<StageData>(json);
            Debug.Log("JSON Loaded");
        }
        else
        {
            Debug.LogError($"File not found: {filePath}");
        }
    }

    private void SaveJSON()
    {
        if (stageData != null)
        {
            string json = JsonUtility.ToJson(stageData, true);
            File.WriteAllText(filePath, json);
            AssetDatabase.Refresh();
            Debug.Log("JSON Saved");
        }
    }
}
