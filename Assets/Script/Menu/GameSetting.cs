using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // Red
    public void SetRedHumanType(bool on)
    {
        if (on) SaveSettings.players[0] = "HUMAN";
    }
    public void SetRedCPUType(bool on)
    {
        if (on) SaveSettings.players[0] = "CPU";
    }
    public void SetRedNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[0] = "NP";
    }
    // blue
    public void SetBlueHumanType(bool on)
    {
        if (on) SaveSettings.players[1] = "HUMAN";
    }
    public void SetBlueCPUType(bool on)
    {
        if (on) SaveSettings.players[1] = "CPU";
    }
    public void SetBlueNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[1] = "NP";
    }
    // yellow
    public void SetYellowHumanType(bool on)
    {
        if (on) SaveSettings.players[2] = "HUMAN";
    }
    public void SetYellowCPUType(bool on)
    {
        if (on) SaveSettings.players[2] = "CPU";
    }
    public void SetYellowNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[2] = "NP";
    }
    // purple
    public void SetPurpleHumanType(bool on)
    {
        if (on) SaveSettings.players[3] = "HUMAN";
    }
    public void SetPurpleCPUType(bool on)
    {
        if (on) SaveSettings.players[3] = "CPU";
    }
    public void SetPurpleNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[3] = "NP";
    }
    // Green
    public void SetGreenHumanType(bool on)
    {
        if (on) SaveSettings.players[4] = "HUMAN";
    }
    public void SetGreenCPUType(bool on)
    {
        if (on) SaveSettings.players[4] = "CPU";
    }
    public void SetGreenNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[4] = "NP";
    }
    // Orange
    public void SetOrangeHumanType(bool on)
    {
        if (on) SaveSettings.players[5] = "HUMAN";
    }
    public void SetOrangeCPUType(bool on)
    {
        if (on) SaveSettings.players[5] = "CPU";
    }
    public void SetOrangeNoPlayerType(bool on)
    {
        if (on) SaveSettings.players[5] = "NP";
    }
}

public static class SaveSettings
{
    //red blue yellow purple green orange
    public static string[] players = new string[6];
}
