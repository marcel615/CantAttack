using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldDatabase")]
public class ShieldDatabaseSO : ScriptableObject
{
    public List<ShieldDataSO> allShields;

    private static ShieldDatabaseSO _instance;
    public static ShieldDatabaseSO Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<ShieldDatabaseSO>("SOs/Database/ShieldDatabase");
            return _instance;
        }
    }

    public ShieldDataSO GetShieldDataSOByType(ShieldType type)
    {
        return allShields.Find(s => s.shieldType == type);
    }
}
