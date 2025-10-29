#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ParryModeDataSO))]
public class ParryModeDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var so = (ParryModeDataSO)target;

        //공통 필드
        EditorGUILayout.LabelField("패리 모드 기본 정보", EditorStyles.boldLabel);

        so.parryModeID = EditorGUILayout.TextField("패리 모드 ID", so.parryModeID);
        so.parryModeType = (ParryModeType)EditorGUILayout.EnumPopup("패리 모드 타입", so.parryModeType);
        so.equipIconPrefab = (GameObject)EditorGUILayout.ObjectField("패리 모드 장착 아이콘", so.equipIconPrefab, typeof(GameObject), false);
        so.inventoryIconPrefab = (GameObject)EditorGUILayout.ObjectField("패리 모드 인벤토리 아이콘", so.inventoryIconPrefab, typeof(GameObject), false);

        EditorGUILayout.Space();

        switch (so.parryModeType)
        {
            case ParryModeType.Directional:
                so.directional.slowModeScale = EditorGUILayout.FloatField("슬로우 모드 정도", so.directional.slowModeScale);
                so.directional.confirmDelay = EditorGUILayout.FloatField("방향 확정 시간", so.directional.confirmDelay);
                so.directional.maxWaitTime = EditorGUILayout.FloatField("최대 입력 대기 시간", so.directional.maxWaitTime);
                break;
        }

        if (GUI.changed)
            EditorUtility.SetDirty(so);

    }

}
#endif