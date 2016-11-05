using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioController : GenericSingletonClass<AudioController> {

    public GameObject musicDisplay;
    public GameObject effectsDisplay;

    /*** Audio ***/
    private float _musicVolume;
    public float musicVolume
    {
        get { return musicVolume; }
        set
        {
            if (value < 0)
                _musicVolume = 0;
            else if (value > 100)
                _musicVolume = 100;
            else
                _musicVolume = value;
        }
    }

    private float _effectsVolume;
    public float effectsVolume
    {
        get { return _effectsVolume; }
        set
        {
            if (value < 0)
                _effectsVolume = 0;
            else if (value > 100)
                _effectsVolume = 100;
            else
                _effectsVolume = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        musicVolume = 100;
        effectsVolume = 100;
    }

    // Called by slider event trigger to change music volume
    public void AdjustMusicVolume(float newVolume)
    {
        musicVolume = newVolume;

        if (newVolume != 0)
            musicDisplay.GetComponent<Text>().text = newVolume.ToString();
        else
            musicDisplay.GetComponent<Text>().text = "Off";
    }

    // Called by slider event trigger to change effect volume
    public void AdjustEffectsVolume(float newVolume)
    {
        effectsVolume = newVolume;

        if (newVolume != 0)
            effectsDisplay.GetComponent<Text>().text = newVolume.ToString();
        else
            effectsDisplay.GetComponent<Text>().text = "Off";
    }
}
