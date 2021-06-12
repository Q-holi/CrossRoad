using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Toggle Mute;
    public Slider SFXSlider;
    public Slider BGMSlider;

    private void Start()
    {
        bool m = masterMixer.GetFloat("MASTER", out float master);
        bool s = masterMixer.GetFloat("SFX", out float sfx);
        bool b = masterMixer.GetFloat("BGM", out float bgm);

        if(master == -80)
        {
            Mute.isOn = true;
        }
        else
        {
            Mute.isOn = false;
        }

        SFXSlider.value = sfx;
        BGMSlider.value = bgm;
    }

    private void Update()
    {
        bool m = masterMixer.GetFloat("MASTER", out float master);
        bool s = masterMixer.GetFloat("SFX", out float sfx);
        bool b = masterMixer.GetFloat("BGM", out float bgm);
        float s_sound = SFXSlider.value;
        float b_sound = BGMSlider.value;

        if (Mute.isOn == true)
        {
            masterMixer.SetFloat("Master", -80);
        }
        else
        {
            masterMixer.SetFloat("Master", 0);
        }

        if (s_sound == -40f) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", s_sound);

        if (b_sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", b_sound);
    }
}
