using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    private void Start()
    {
        FadeEvents.InvokeFadeOpen(SceneTransitionManager.Instance.fadeTime, FadeDirection.FadeIn);
    }

}
