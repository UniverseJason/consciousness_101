using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace Script.Music
{
    public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public float maxVolume = 0.0f;
    private string currentTrack = "BasicV";

    public void Start()
    {
        audioMixer.SetFloat("BasicV", maxVolume);
        audioMixer.SetFloat("DarkV", -80.0f);
        audioMixer.SetFloat("FightV", -80.0f);
    }

    public void SmoothChangeTo(string targetTrack)
    {
        StartCoroutine(CrossfadeTrack(targetTrack));
    }

    private IEnumerator CrossfadeTrack(string targetTrack)
    {
        string previousTrack = FindCurrentTrack();

        if (previousTrack == targetTrack) yield break;

        float transitionTime = 0.1f;
        float elapsedTime = 0;

        // Get the current volume levels of the tracks
        audioMixer.GetFloat(previousTrack, out float previousTrackVolume);
        audioMixer.GetFloat(targetTrack, out float targetTrackVolume);

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;

            // Lerp the volume of the previous track down to -80dB
            float newPreviousVolume = Mathf.Lerp(previousTrackVolume, -80.0f, elapsedTime / transitionTime);
            audioMixer.SetFloat(previousTrack, newPreviousVolume);

            // Lerp the volume of the target track up to maxVolume
            float newTargetVolume = Mathf.Lerp(targetTrackVolume, maxVolume, elapsedTime / transitionTime);
            audioMixer.SetFloat(targetTrack, newTargetVolume);

            yield return null;
        }

        // Ensure the target track is set to max volume and previous track to minimum at the end of the transition
        audioMixer.SetFloat(previousTrack, -80.0f);
        audioMixer.SetFloat(targetTrack, maxVolume);

        // Update the current track
        currentTrack = targetTrack;
    }

    private string FindCurrentTrack()
    {
        // Initialize the volume
        float highestVolume = -80.0f;
        string playingTrack = currentTrack; // Default to the current track

        // Array of all tracks
        string[] tracks = new string[] { "BasicV", "DarkV", "FightV" };

        foreach (var track in tracks)
        {
            audioMixer.GetFloat(track, out float volume);
            // If this track's volume is higher than the highest we found, it's the current track
            if (volume > highestVolume)
            {
                highestVolume = volume;
                playingTrack = track;
            }
        }

        return playingTrack;
    }
}
}