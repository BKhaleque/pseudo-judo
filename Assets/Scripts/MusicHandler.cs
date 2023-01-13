using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] musicList;
    public AudioSource audio;
    private bool firstSongPlaying = true;

    int GetMusicFromCharacter(string characterName)
    {
        if (characterName == null)
        {
            characterName = "";
        }

        switch (characterName.ToLower())
        {
            case "putin":
                return 0;
                break;

            case "boris":
                return 1;
                break;

            case "biden":
                return 2;
                break;

            case "macron":
                return 3;
                break;

            // Defaults to Putin's theme if an invalid selection
            default:
                return 0;
                break;
        }

        return 0;
    }

    void PlaySongs(int songOne, int songTwo)
    {
        if (firstSongPlaying)
        {
            firstSongPlaying = false;
            audio.clip = musicList[songOne];
        }
        else
        {
            firstSongPlaying = true;
            audio.clip = musicList[songTwo];
        }

        audio.Play();
        Invoke("PlaySongs", audio.clip.length);
        Debug.Log("Playing the other song.");
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        int songNumberOne = GetMusicFromCharacter(PlayerCharacterHolder.playerOneCharacterName);
        int songNumberTwo = GetMusicFromCharacter(PlayerCharacterHolder.playerTwoCharacterName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
