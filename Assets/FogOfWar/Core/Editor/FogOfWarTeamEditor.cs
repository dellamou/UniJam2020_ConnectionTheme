using UnityEngine;
using UnityEditor;
using FoW;

[CustomEditor(typeof(FogOfWarTeam))]
[CanEditMultipleObjects]
public class FogOfWarTeamEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (targets.Length == 1)
        {
            FogOfWarTeam team = (FogOfWarTeam)target;
            if (team.renderType == FogOfWarRenderType.Hardware)
                EditorGUILayout.HelpBox("Hardware is currently experimental and some features are not supported. Check the user guide for more details.", MessageType.Warning);
            if (team.renderType == FogOfWarRenderType.Hardware && team.multithreaded)
                EditorGUILayout.HelpBox("Multithreading is not supported in hardware mode!", MessageType.Error);
        }

        if (Application.isPlaying && GUILayout.Button("Reinitialize"))
        {
            foreach (Object objtarget in targets)
                ((FogOfWarTeam)objtarget).Reinitialize();
        }
    }
}
