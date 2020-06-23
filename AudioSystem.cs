/**
 * @author Kleber Ribeiro da Silva
 * @email krsmga@gmail.com
 * @create date 2020-06-23 15:13:45
 * @modify date 2020-06-23 15:13:45
 * @desc This class controls the general Audio (AudioListener) of the current scene.
 * @github https://github.com/krsmga/AudioSystem
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the general Audio (AudioListener) of the current scene.
/// </summary>
/// <remarks>
/// <param name="_iconEnabledAudio">Attach a GameObject that contains an Image (Sprite) to represent the enabled audio.</param>
/// <param name="_iconDisabledAudio">Attach a GameObject that contains an Image (Sprite) to represent the disabled audio. (Optional)</param>
/// <param name="_iconVolume">Attach images (Sprites) with a sequence that demonstrates the volume level. From the smallest to the largest. (Optional)</param>
/// <param name="_audioListener">Attach the AudioListener component of the current scene.</param>
/// <param name="_volume">Sets the volume value of the General Audio (AudioListener).</param>
/// </remarks>
public class AudioSystem : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private GameObject _iconEnabledAudio = default;
    [SerializeField] private GameObject _iconDisabledAudio = default;
    [SerializeField] private Sprite[] _iconVolume = default;
    [SerializeField] private AudioListener _audioListener = default;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _volume = default;

    private bool _isActive;

    /// <summary>
    /// Enables or disables audio for the current scene.
    /// </summary>
    /// <returns>
    /// (bool) -> true is actived
    /// </returns>
    public bool isActive
    {
        get 
        { return _isActive; }
        set 
        {
            if (_isActive != value)
            {
                _isActive = value;
                UpdateIcons(_isActive, -1);
            }
        }
    }

    /// <summary>
    /// Controls the audio volume.
    /// </summary>
    /// <returns>
    /// (float) -> 0 to 1
    /// </returns>
    public float volume
    {
        get { return _volume; }
        set
        {
            _volume = value;
            AudioListener.volume = _volume;

            if (_volume == 0)
            {
                UpdateIcons(false, 0);
            }
            else if (_volume == 1)
            {
                UpdateIcons(true, _iconVolume.Length-1);
            }
            else
            {
                float j = 0;
                for (int i = 0; i < _iconVolume.Length; i++)
                {
                    if (_volume <= j)
                    {
                        UpdateIcons(true, i);
                        break;
                    }
                    j+=1f / _iconVolume.Length;
                }
            }
        }
    }

    // Start default values
    void Awake() 
    {
        AudioListener.volume = _volume;
        volume = _volume;
    }

    private void UpdateIcons(bool value, int volume) 
    {
        if (_audioListener != null)
        {
            _audioListener.enabled = value;
        }

        if (_iconEnabledAudio != null)
        {
            _iconEnabledAudio.SetActive(value);
            if (_iconVolume.Length > 0 && volume >= 0)
            {
                _iconEnabledAudio.GetComponent<Image>().sprite = _iconVolume[volume];
            }
        }

        if (_iconDisabledAudio != null)
        {
            _iconDisabledAudio.SetActive(!value);
            if (_iconVolume.Length > 0 && volume >= 0)
            {
                _iconDisabledAudio.GetComponent<Image>().sprite = _iconVolume[volume];
            }
        }
    }
}