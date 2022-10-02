using RandomExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioEmmitter : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;


    private void Awake()
    {
#if UNITY_EDITOR
        Debug.Assert(source != null, "An AudioSource is needed!");
#endif
    }
    
    public void EmmitSound(AudioGroup audioType)
    {
        audioType.PlayRandomFrom(source);
    }
}
