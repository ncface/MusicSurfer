using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class SongEngine : MonoBehaviour
{
    public string songFile;

    public Song LoadSong(string songFileName)
    {
        string jsonString = File.ReadAllText(songFileName);
        Song song = JsonUtility.FromJson<Song>(jsonString);

        return song;
    }
}

[Serializable]
public class Song
{
    public string BackgroundTrack;
    public Chord[] Chords;

    [System.Serializable]
    public struct Chord
    {
        public string Time;
        public string Track;
    }
}
