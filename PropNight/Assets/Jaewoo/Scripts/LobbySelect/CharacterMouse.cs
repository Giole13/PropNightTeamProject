using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouse : MonoBehaviour
{
    [SerializeField]
    private GameObject noHairKiller = default;
    [SerializeField]
    private GameObject hairKiller = default;
    [SerializeField]
    private GameObject runnerPlayer = default;
    [SerializeField]
    private GameObject healerPlayer = default;
    [SerializeField]
    private GameObject psychoPlayer = default;
    [SerializeField]
    private GameObject jumperPlayer = default;


    void Awake()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(false);
    }
    public void OnMouseNoHairKiller()
    {
        noHairKiller.SetActive(true);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(false);
    }
    public void OnMouseHairKiller()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(true);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(false);
    }

    public void OnMouseRunnerPlayer()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(true);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(false);
    }

    public void OnMouseHealerPlayer()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(true);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(false);
    }
    public void OnMousePsyChoPlayer()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(true);
        jumperPlayer.SetActive(false);
    }

    public void OnMouseJumperPlayer()
    {
        noHairKiller.SetActive(false);
        hairKiller.SetActive(false);
        runnerPlayer.SetActive(false);
        healerPlayer.SetActive(false);
        psychoPlayer.SetActive(false);
        jumperPlayer.SetActive(true);
    }



}
