using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXSO")]
public class SFXSO : ScriptableObject
{
    public SFXType sfxType;
    public AudioClip audioClip;
}
