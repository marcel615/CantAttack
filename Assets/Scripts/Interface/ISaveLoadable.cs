using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �� �ε� ��� �������̽�
public interface ISaveLoadable
{
    string DicKey { get; }
    object Save();
    void Load(object saveData);

}
