using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Song
{
    public string BackgroundTrack;
    public Chord[] Chords;
    
    

    public void LoadTracks(string SongFolder)
    {
        //chords laden und erstellen
        for(int i = 0; i < Chords.Length; i++)
        {
            //load track from disc
            Chords[i].loadTrack(SongFolder);
        }
    }

    [Serializable]
    public struct Chord
    {
        public int Time;
        public string Track;
        public int Lane;

        [NonSerialized]
        public AudioClip chord;

        public void loadTrack(string SongFolder)
        {
            chord = Resources.Load<AudioClip>(SongFolder + Track);
        }
    }
}

