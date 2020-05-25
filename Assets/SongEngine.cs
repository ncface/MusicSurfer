using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class SongEngine : MonoBehaviour
{
    public string songFile;
    public string songFolder;

    public void Start()
    {
        LoadSong(songFile);
    }

    public Song LoadSong(string songFileName)
    {
        string jsonString = File.ReadAllText(songFileName);
        Song song = JsonUtility.FromJson<Song>(jsonString);

        song.LoadTracks(gameObject, songFolder);
        return song;
    }
}

[Serializable]
public class Song
{
    public string BackgroundTrack;
    public Chord[] Chords;

    public void LoadTracks(GameObject audioManager, string SongFolder)
    {
        foreach(Chord chord in Chords)
        {
            chord.loadTrack(audioManager, SongFolder);
        }
    }
    
    [Serializable]
    public struct Chord
    {
        public string Time;
        public string Track;

        [NonSerialized]
        public AudioSource audioSource;

        public void loadTrack(GameObject audioManager, string SongFolder)
        {
            audioSource = audioManager.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = Resources.Load(SongFolder + Track) as AudioClip;
        }
    }
}
