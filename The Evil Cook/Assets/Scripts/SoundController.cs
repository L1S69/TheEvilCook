using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SoundController : MonoBehaviour
{
    [SerializeField]private Scrollbar SFXScrollbar;
    [SerializeField]private Text SFXNow;
    [SerializeField]private Scrollbar MusicScrollbar;
    [SerializeField]private Text MusicNow;
    [SerializeField]private SettingsSaver data;

    public float SFX;
    public float Music;

    void Update()
    {
        SFX = SFXScrollbar.value * 100f;
        Music = MusicScrollbar.value * 100f;
        SFXNow.text = SFX.ToString("0") + "%";
        MusicNow.text = Music.ToString("0") + "%";
    }

    void Start()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        data.SFX = SFX;
        data.Music = Music;
        File.WriteAllText(Application.streamingAssetsPath + "Sound.json", JsonUtility.ToJson(data));
    }

    public void LoadSettings()
    {
        data = JsonUtility.FromJson<SettingsSaver>(File.ReadAllText(Application.streamingAssetsPath + "Sound.json"));
        SFX = data.SFX;
        Music = data.Music;
    }
}
