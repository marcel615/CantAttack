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
                so.scatter.pelletCount = EditorGUILayout.IntField("펠릿 개수", so.scatter.pelletCount);
                so.scatter.spreadDeg = EditorGUILayout.FloatField("퍼지는 최대 각도", so.scatter.spreadDeg);
                so.scatter.jitterDeg = EditorGUILayout.FloatField("오차 각도", so.scatter.jitterDeg);
                so.scatter.speedVariance = EditorGUILayout.FloatField("펠릿 간 속도 오차 정도", so.scatter.speedVariance);
                break;
        }

        if (GUI.changed)
            EditorUtility.SetDirty(so);

    }
}
#endif
