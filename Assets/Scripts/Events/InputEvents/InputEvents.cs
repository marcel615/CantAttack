using System;
using UnityEngine;

public static class InputEvents
{
    //InputManager의 Context 관리 이벤트
    public static event Action<InputContext, bool> OnContextUpdate;
    //각 UI들의 가장 위의 Selectable 오브젝트 선택하게 해주는 이벤트
    public static event Action<GameObject> OnSelectFirstSelectable;

    //InputManager의 Context 관리 이벤트
    public static void InvokeContextUpdate(InputContext context, bool state)
    {
        OnContextUpdate?.Invoke(context, state);
    }
    //각 UI들의 가장 위의 Selectable 오브젝트 선택하게 해주는 이벤트
    public static void InvokeSelectFirstSelectable(GameObject panel)
    {
        OnSelectFirstSelectable?.Invoke(panel);
    }

    public static PlayerInputEvents Player { get; private set; } = new PlayerInputEvents();
    public static SystemMenuInputEvents SystemMenu { get; private set; } = new SystemMenuInputEvents();
    public static SettingInputEvents Setting { get; private set; } = new SettingInputEvents();

    /*
    /// <?>
    //R 이벤트 (회복 아이템 사용)
    public static event Action<bool> OnUseHealItem;
    /// </?>

    /// <Item>
    //R 이벤트 (회복 아이템 사용)
    public static void InvokeUseHealItem(bool r)
    {
        OnUseHealItem?.Invoke(r);
    }
    /// </Item>
    */

}