using System;
using UnityEngine;

public static class InputEvents
{
    //InputManager�� Context ���� �̺�Ʈ
    public static event Action<InputContext, bool> OnContextUpdate;
    //�� UI���� ���� ���� Selectable ������Ʈ �����ϰ� ���ִ� �̺�Ʈ
    public static event Action<GameObject> OnSelectFirstSelectable;

    //InputManager�� Context ���� �̺�Ʈ
    public static void InvokeContextUpdate(InputContext context, bool state)
    {
        OnContextUpdate?.Invoke(context, state);
    }
    //�� UI���� ���� ���� Selectable ������Ʈ �����ϰ� ���ִ� �̺�Ʈ
    public static void InvokeSelectFirstSelectable(GameObject panel)
    {
        OnSelectFirstSelectable?.Invoke(panel);
    }

    public static PlayerInputEvents Player { get; private set; } = new PlayerInputEvents();
    public static SystemMenuInputEvents SystemMenu { get; private set; } = new SystemMenuInputEvents();
    public static SettingInputEvents Setting { get; private set; } = new SettingInputEvents();

    /*
    /// <?>
    //R �̺�Ʈ (ȸ�� ������ ���)
    public static event Action<bool> OnUseHealItem;
    /// </?>

    /// <Item>
    //R �̺�Ʈ (ȸ�� ������ ���)
    public static void InvokeUseHealItem(bool r)
    {
        OnUseHealItem?.Invoke(r);
    }
    /// </Item>
    */

}