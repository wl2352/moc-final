using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mp3AudioClip;

    void Start()
    {
        // Assign the MP3 audio clip to the AudioSource component
        audioSource.clip = mp3AudioClip;

        // Set the audio to loop
        audioSource.loop = true;

        // Play the MP3 audio clip
        audioSource.Play();
    }


}
