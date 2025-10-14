#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ShieldDataSO))]
public class ShieldDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var so = (ShieldDataSO)target;

        //공통 필드
        EditorGUILayout.LabelField("방패 기본 정보", EditorStyles.boldLabel);

        so.shieldID = EditorGUILayout.TextField("방패 ID", so.shieldID);
        so.shieldType = (ShieldType)EditorGUILayout.EnumPopup("방패 타입", so.shieldType);
        so.iconPrefab = (GameObject)EditorGUILayout.ObjectField("방패 아이콘", so.iconPrefab, typeof(GameObject), false);
        so.parryProjectilePrefab = (GameObject)EditorGUILayout.ObjectField("패리 성공 시 투사체 프리팹", so.parryProjectilePrefab, typeof(GameObject), false);

        EditorGUILayout.Space();

        switch (so.shieldType)
        {
            case ShieldType.Scatter:
                so.scatter.pelletCount = EditorGUILayout.IntField("Pellet Count", so.scatter.pelletCount);
                so.scatter.spreadDeg = EditorGUILayout.FloatField("Spread Deg", so.scatter.spreadDeg);
                so.scatter.jitterDeg = EditorGUILayout.FloatField("Jitter Deg", so.scatter.jitterDeg);
                so.scatter.speedVariance = EditorGUILayout.FloatField("Speed Variance", so.scatter.speedVariance);
                break;
        }

        if (GUI.changed)
            EditorUtility.SetDirty(so);

    }
}
#endif
