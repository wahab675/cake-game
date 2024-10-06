using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    // Exposed public static properties for game settings
    public static bool SoundEnabled { get; private set; }
    public static bool MusicEnabled { get; private set; }
    public static bool HapticsEnabled { get; private set; }

    // UI toggles for the settings
    public Toggle soundToggle;
    public Toggle musicToggle;
    public Toggle hapticsToggle; // Changed from vibrationToggle to hapticsToggle

    private void Start()
    {
        // Load settings on start
        //LoadSettings();

        // Set initial toggle values based on loaded settings
        soundToggle.isOn = Profile.SoundEnabled;
        musicToggle.isOn = Profile.MusicEnabled;
        hapticsToggle.isOn = Profile.HapticsEnabled; // Changed from vibrationToggle to hapticsToggle

        // Customize toggle appearance
        CustomizeToggle(soundToggle);
        CustomizeToggle(musicToggle);
        CustomizeToggle(hapticsToggle); // Changed from vibrationToggle to hapticsToggle

        // Add listener methods to the toggles
        soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        musicToggle.onValueChanged.AddListener(OnMusicToggleChanged);
        hapticsToggle.onValueChanged.AddListener(OnHapticsToggleChanged); // Changed from vibrationToggle to hapticsToggle
    }

    // Method to customize the appearance of the toggle
    private void CustomizeToggle(Toggle toggle)
    {
        // Disable the background image when the toggle is on
        //toggle.graphic.CrossFadeAlpha(toggle.isOn ? 0f : 1f, 0f, true);
        //toggle.graphic.canvasRenderer.SetAlpha(0);
        //toggle.graphic.canvasRenderer.SetAlpha(toggle.isOn ? 0f : 1f);
        //toggle.graphic.GetComponent<Image>().color = toggle.isOn ? Color.clear : Color.white;
        //toggle.graphic.enabled = toggle.isOn;

        // Disable the background when the toggle is on
        toggle.transform.GetChild(0).GetComponent<Image>().color = toggle.isOn ? Color.clear : Color.white;
    }



    // Method to handle sound toggle change
    private void OnSoundToggleChanged(bool value)
    {
        Profile.SoundEnabled = value;
        //SaveSettings();
        CustomizeToggle(soundToggle);

        SoundController.Instance.PlaySound(SoundType.Click);
    }

    // Method to handle music toggle change
    private void OnMusicToggleChanged(bool value)
    {
        Profile.MusicEnabled = value;
        //SaveSettings();
        CustomizeToggle(musicToggle);

        SoundController.Instance.PlaySound(SoundType.Click);
        SoundController.Instance.UpdateBgmMuteState();
    }

    // Method to handle haptics toggle change
    private void OnHapticsToggleChanged(bool value)
    {
        Profile.HapticsEnabled = value;
        //SaveSettings();
        CustomizeToggle(hapticsToggle);

        SoundController.Instance.PlaySound(SoundType.Click);
    }

    // Method to save settings using PlayerPrefs
    private void SaveSettings()
    {
        PlayerPrefs.SetInt("SoundEnabled", SoundEnabled ? 1 : 0);
        PlayerPrefs.SetInt("MusicEnabled", MusicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("HapticsEnabled", HapticsEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    //// Method to load settings using PlayerPrefs
    //public static void LoadSettings()
    //{
    //    SoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
    //    MusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
    //    HapticsEnabled = PlayerPrefs.GetInt("HapticsEnabled", 1) == 1;
    //}
}
