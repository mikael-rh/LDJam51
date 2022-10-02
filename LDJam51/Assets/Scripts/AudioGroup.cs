using RandomExtensions;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioGroup", menuName = "Audio/New Audio Group")]
public class AudioGroup : ScriptableObject
{
    [SerializeField]
    private AudioClip[] sounds;
    
#if UNITY_EDITOR
    [SerializeField, XRange(-1, 2), ContextMenuItem("Preview/Low Pitch", "PreviewLowPitch"), ContextMenuItem("Preview/High Pitch", "PreviewHighPitch")]
#endif
    private FloatRange pitchRange = new FloatRange() { min = 0, max = 2 };

#if UNITY_EDITOR
    [SerializeField, XRange(0, 1), ContextMenuItem("Preview/Low Volume", "PreviewLowVolume"), ContextMenuItem("Preview/High Volume", "PreviewHighVolume")]
#endif
    private FloatRange volumeRange = new FloatRange() { min = 0.75f, max = 1 };

    public void Modulate(AudioSource source)
    {
        source.pitch = Random.Range(pitchRange.min, pitchRange.max);
        source.volume = Random.Range(volumeRange.min, volumeRange.max);
    }
    public void PlayRandomFrom(AudioSource source)
    {
        source.pitch = Random.Range(pitchRange.min, pitchRange.max);
        source.volume = Random.Range(volumeRange.min, volumeRange.max);
        source.PlayOneShot(sounds.RandomElement());
    }

    #region PREVIEW_IN_EDITOR_FUNCTIONALITY
#if UNITY_EDITOR
    private static AudioSource previewSource;

    private static AudioSource PreviewSource
    {
        get
        {
            if (previewSource == null)
            {
                previewSource = new GameObject("audio_preview_source").AddComponent<AudioSource>();
                AudioListener listener = FindObjectOfType<AudioListener>();
                if (listener == null)
                    previewSource.gameObject.AddComponent<AudioListener>();
                previewSource.gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
            return previewSource;
        }
    }

    [ContextMenu("Play Random")]
    void PlayRandom()
    {
        AudioClip clip = sounds.RandomElement();
        clip.LoadAudioData();
        Modulate(PreviewSource);
        PreviewSource.PlayOneShot(clip);
        
    }

    void PreviewLowPitch()
    {
        AudioClip clip = sounds.RandomElement();
        clip.LoadAudioData();
        PreviewSource.pitch = pitchRange.min;
        PreviewSource.PlayOneShot(clip);
    }

    void PreviewHighPitch()
    {
        AudioClip clip = sounds.RandomElement();
        clip.LoadAudioData();
        PreviewSource.pitch = pitchRange.max;
        PreviewSource.PlayOneShot(clip);
    }
    void PreviewLowVolume()
    {
        AudioClip clip = sounds.RandomElement();
        clip.LoadAudioData();
        PreviewSource.PlayOneShot(clip, volumeRange.min);
    }
    void PreviewHighVolume()
    {
        AudioClip clip = sounds.RandomElement();
        clip.LoadAudioData();
        PreviewSource.PlayOneShot(clip, volumeRange.max);
    }
#endif
    #endregion
}