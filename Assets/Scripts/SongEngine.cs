using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class SongEngine : MonoBehaviour
{
    public string songFolder;
    public GameObject chordPrefab;
    public GameObject chordsParent;

    public void Start()
    {
        Song song = LoadSong(songFolder);
        instantiateChords(song);
    }

    public Song LoadSong(string songFolder)
    {
        string jsonString = File.ReadAllText("Assets/Resources/" + songFolder + "song.json");
        Song song = JsonUtility.FromJson<Song>(jsonString);
        
        song.LoadTracks(songFolder);
        return song;
    }

    public void instantiateChords(Song song)
    {
        foreach(Song.Chord chord in song.Chords)
        {
            //insantiate chord as game object
            GameObject chordObject = Instantiate(chordPrefab, chordsParent.transform);

            // add audio-clip to new audio-source
            AudioSource audioSource = chordObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = chord.chord;

        }
    }
}