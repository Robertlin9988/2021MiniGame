using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiosName : MonoBehaviour
{
    public static string objname = "AudioResources";
    public static string clock = "clock";
    public static string wakeup = "wakeup";
    public static string washingwater = "washingwater";
    public static string continuebutton = "continuebutton";
    public static string metrosound = "metrosound";
    public static string ruinbgm = "ruinbgm";
    public static string walkstep = "walkstep";


    public enum audioname
    {
        clock, wakeup, washingwater, continuebutton, metrosound,
        ruinbgm,door
    }
}
