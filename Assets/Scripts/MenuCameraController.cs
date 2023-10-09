using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    public enum AnimState
    {
        NotPlaying = 0,
        Playing,
        Ended
    }

    private AnimState isAnimPlaying = AnimState.NotPlaying;

    public void OnStartAnimationStarted()
    {
        isAnimPlaying = AnimState.Playing;
    }

    public void OnStartAnimationEnded()
    {
        isAnimPlaying = AnimState.Ended;
    }

    public void OnEndAnimationStarted()
    {
        isAnimPlaying = AnimState.Playing;
    }

    public void OnEndAnimationEnded()
    {
        isAnimPlaying = AnimState.Ended;
    }
    
    public AnimState IsAnimPlaying()
    {
        AnimState state = isAnimPlaying;

        if (state == AnimState.Ended)
        {
            isAnimPlaying = AnimState.NotPlaying;
        }

        return state;
    }
}
