using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
[CanEditMultipleObjects]
public class SvgToMeshEditor : Editor
{
    LevelGenerator _levelGenerator;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _levelGenerator = target as LevelGenerator;

        if(GUILayout.Button("Create Sprite"))
        {
            EditorUtility.SetDirty(_levelGenerator);
            _levelGenerator.SpriteToMesh();
        }
    }


}
