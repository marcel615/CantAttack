using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParryModeDatabase")]
public class ParryModeDatabaseSO : ScriptableObject
{
    public List<ParryModeDataSO> allParryModes;

    private static ParryModeDatabaseSO _instance;
    public static ParryModeDatabaseSO Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<ParryModeDatabaseSO>("SOs/Database/ParryModeDatabase");
            return _instance;
        }
    }

    public ParryModeDataSO GetParryModeDataSOByType(ParryModeType type)
    {
        return allParryModes.Find(s => s.parryModeType == type);
    }

}
