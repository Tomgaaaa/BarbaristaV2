using UnityEngine;
using UnityEngine.Audio;

// Cr�ation d'une class pour pouvoir parametrer les sons
[System.Serializable]
public class SoundSettings
{
    // un nom pour pouvoir jouer le son souhait�
    public string label;

    // un array de clip pour pouvoir jouer un son random parmis une list de son
    public AudioClip[] clip;

    // cach� car on a pas besoin de le renseigner puisqu'on l'instantie au d�but 
    [HideInInspector]
    public AudioSource source;

    // pour d�finir si on veut que le son loop ou non
    public bool loop;

    [Range(0,2)] 
    public float volume=1;

    [Range(0,2)]
    public float pitch;

    public AudioMixer Mixer;

    
     

}




public class AudioManager : MonoBehaviour
{
    // un array de SoundSettings (class juste au dessus), pour lister tout les sons du jeu
    [SerializeField] SoundSettings[] sounds;

    // singleton 
    public static AudioManager instanceAM;

    private void Awake()
    {
        if (instanceAM == null)
            instanceAM = this;
        else
            Destroy(gameObject);
        // pas de destroyOnLoad, il est vrai que la derni�re fois je ne m'etais pas relus

        // lance la fonction Init qui initialise les AudioSources 
        Init();


    }

    private void Start()
    {
        
        

    }


    private void Init()
    {

        // on veut que pour chaque �l�ment de l'array Sound on pplique les changements suivants
        foreach (SoundSettings s in sounds)
        {
            // on ajoute le component AudioSource
            s.source = gameObject.AddComponent<AudioSource>();

            // on renseigne le clip, je met clip[0] pour mettre le premier son par d�faut mais je le change apr�s
            s.source.clip = s.clip[0];

            // active ou d�sactive le loop en fonction de ce qu'on a renseign�
            s.source.loop = s.loop;

            // augmente ou baisse le volume en fonction de ce qu'on a renseign�
            s.source.volume = s.volume;

            // augmente ou baisse la vitesse du son
            s.source.pitch = s.pitch;

            


        }
    }


    // prend le parametre volume (de base a 1), pour varier le volume lorsqu'on rate la caisse
    public void Play(string name, float volume = 1)
    {
        // cherche parmis l'array sounds un �l�ment qui a un label = au parametre name
        SoundSettings s = System.Array.Find(sounds, sound => sound.label == name);

        // choisis un son random parmis l'array de clip pour vari� le clip
        s.source.clip = s.clip[Random.Range(0, s.clip.Length)];

        // set le volume de la source � la valeur Random re�u comme parametre
        s.source.volume = volume;

        // si le name n'est pas trouv� on return pour pas bloquer le jeu
        if (s == null)
            return;

        // indique � l'audio source, correspondant au parametre name, de play le clip choisit aleatoirement
        s.source.Play();
    }

    public void Pause(string name)
    {
        // cherche parmis l'array sounds un �l�ment qui a un label = au parametre name
        SoundSettings s = System.Array.Find(sounds, sound => sound.label == name);


        // si le name n'est pas trouv� on return pour pas bloquer le jeu
        if (s == null)
            return;

        // indique � l'audio source, correspondant au parametre name, de play le clip choisit aleatoirement
        s.source.Pause();
    }


}
