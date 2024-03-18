using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SCR_Options : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider GeneraleSlider, MusiqueSlider, AmbianceSlider, SFXSlider, VoiceSlider;



    public void Generale(float volume)
    {
        audioMixer.SetFloat("Generale", volume);
        
    }

    public void Musique(float volume)
    {
        
        audioMixer.SetFloat("Musique", volume);
        AudioManager.instanceAM.SetOptionVolume(volume);
    }
    public void Ambiance(float volume)
    {
        
        audioMixer.SetFloat("Ambiance", volume);
    }

    public void SFX(float volume)
    {
        
        audioMixer.SetFloat("SFX", volume);
    }

    public void VoiceActing(float volume)
    {
        
        audioMixer.SetFloat("Voice", volume);
    }

    public void UpdateValue()
    {
        audioMixer.GetFloat("Générale", out float valeurGenerale);
        GeneraleSlider.value = valeurGenerale; 

        audioMixer.GetFloat("Musique", out float valeurMusic);
        MusiqueSlider.value = valeurMusic; ;

        audioMixer.GetFloat("Ambiance", out float valeurAmbiance);
        AmbianceSlider.value = valeurAmbiance;

        audioMixer.GetFloat("SFX", out float valeurSFX);
        SFXSlider.value = valeurSFX; ;

        audioMixer.GetFloat("Voice", out float valeurVoice);
        VoiceSlider.value = valeurVoice; ;
    }


}
