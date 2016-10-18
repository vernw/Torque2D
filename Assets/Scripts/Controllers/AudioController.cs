using UnityEngine;
using System.Collections;

public class AudioController : GenericSingletonClass<AudioController> {

    /*** Audio ***/
    private int _musicVolume;
    public int musicVolume
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

    private int _effectsVolume;
    public int effectsVolume
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

        musicVolume = 50;
        effectsVolume = 50;
    }
}
