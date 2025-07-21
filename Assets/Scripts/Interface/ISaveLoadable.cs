using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장 및 로드 기능 인터페이스
public interface ISaveLoadable
{
    string DicKey { get; }
    object Save();
    void Load(object saveData);

}
