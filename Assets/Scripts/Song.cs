using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Song
{
    public string BackgroundTrack;
    public int Finish;
    public Chord[] Chords;
    public Obstacle[] Obstacles;
    
    public void LoadResources(string SongFolder)
    {
        LoadTracks(SongFolder);
        LoadObstacles();
    }

    private void LoadObstacles()
    {
        //chords laden und erstellen
        for (int i = 0; i < Obstacles.Length; i++)
        {
            //load track from disc
            Obstacles[i].loadObstaclePrefab();
        }
    }

    private void LoadTracks(string SongFolder)
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
        public int Layer;
        public bool HurdleBehind;
        public int HrudleBehindType;
        public int DistanceToHurdle;

        [NonSerialized]
        public AudioClip chord;
        [NonSerialized]
        public GameObject prefab;
        [NonSerialized]
        public GameObject hurdlePrefab;
        
        public void loadTrack(string SongFolder)
        {
            chord = Resources.Load<AudioClip>(SongFolder + Track);

            prefab = GameSettings.Instance.chordPrefab;

            //load prefab for obstacle
            if (HurdleBehind
                && GameSettings.Instance.obstaclePrefabs.Count > HrudleBehindType
                && HrudleBehindType >= 0)
            {
                hurdlePrefab = GameSettings.Instance.obstaclePrefabs[HrudleBehindType];
            }
        }
    }

    [Serializable]
    public struct Obstacle
    {
        public int Time;
        public int Type;
        public int Lane;

        [NonSerialized]
        public GameObject prefab;

        public void loadObstaclePrefab()
        {
            if (GameSettings.Instance.obstaclePrefabs.Count > Type 
                && Type >= 0)
            {
                prefab = GameSettings.Instance.obstaclePrefabs[Type];
            }
        }
    }
}

