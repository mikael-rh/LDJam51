using RandomExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioModulator : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] sounds;

    [SerializeField, XRange(-3, 3)]
    private FloatRange pitchRange = new FloatRange(){ min=0, max=2};
    [SerializeField, XRange(0, 1)]
    private FloatRange volumeRange = new FloatRange() { min = 0.75f, max = 1 };

    [SerializeField]
    AudioMixerGroup group;

#if DEBUG
    private void Awake()
    {
        Debug.Assert(source != null, "An AudioSource is needed!");
        Debug.Assert(sounds.Length > 0, "AudioModulator needs at least 1 sound clip to use");
    }
#endif

    [ContextMenu("Play and Modulate")]
    void Play()
    {
        Modulate();
        source.PlayOneShot(sounds.RandomElement());
    }

    private void Modulate()
    {
        source.pitch = Random.Range(pitchRange.min, pitchRange.max);
        source.volume = Random.Range(volumeRange.min, volumeRange.max);
    }
}
