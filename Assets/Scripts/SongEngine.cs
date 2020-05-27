using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class SongEngine : MonoBehaviour
{
    public string songFolder;
    public GameObject chordPrefab;

    public void Start()
    {
        Song song = LoadSong(songFolder);
        instantiateChords(song);
        instantiateObstacles(song);
    }

    public Song LoadSong(string songFolder)
    {
        string jsonString = File.ReadAllText("Assets/Resources/" + songFolder + "song.json");
        Song song = JsonUtility.FromJson<Song>(jsonString);
        
        song.LoadResources(songFolder);
        return song;
    }

    public void instantiateChords(Song song)
    {
        foreach(Song.Chord chord in song.Chords)
        {
            GameObject lane = GameSettings.Instance.lanes[chord.Lane];

            float zOffset = chord.Time / 10;
            float zPos = lane.transform.position.z + zOffset;
            float xPos = lane.transform.position.x;
            float yPos = lane.transform.position.y + 0.5f;
            Vector3 position = new Vector3(xPos, yPos, zPos);

            //insantiate chord as game object
            GameObject chordObject = Instantiate(chordPrefab, position, Quaternion.identity);

            chordObject.transform.parent = lane.transform;

            // add audio-clip to new audio-source
            AudioSource audioSource = chordObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = chord.chord;

        }
    }

    public void instantiateObstacles(Song song)
    {
        foreach (Song.Obstacle obstacle in song.Obstacles)
        {
            GameObject lane = GameSettings.Instance.lanes[obstacle.Lane];

            float zOffset = obstacle.Time / 10;
            float zPos = lane.transform.position.z + zOffset;
            float xPos = lane.transform.position.x;
            float yPos = lane.transform.position.y;
            Vector3 position = new Vector3(xPos, yPos, zPos);

            //insantiate chord as game object
            GameObject obstacleObject = Instantiate(obstacle.prefab, position, Quaternion.identity);
            obstacleObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

            obstacleObject.transform.parent = lane.transform;
        }
    }

    public void instantiateFinish(Song song)
    {
        
    }
}