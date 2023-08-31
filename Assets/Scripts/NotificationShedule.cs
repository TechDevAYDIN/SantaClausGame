using UnityEngine;
using System;
using Assets.SimpleAndroidNotifications;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class NotificationShedule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NotificationManager.CancelAll();
        NotificationManager.SendWithAppIcon(TimeSpan.FromMinutes(Random.Range(60, 180)), "Hey Santa We Need Your Help!", "There are so many gifts that need to be given.", new Color(0, 0.6f, 1), NotificationIcon.Message);
        NotificationManager.SendWithAppIcon(TimeSpan.FromDays(Random.Range(5, 15)), "Hey Santa We Need Your Help!", "There are so many gifts that need to be given.", new Color(0, 0.6f, 1), NotificationIcon.Message);
        ScheduleCustom();
    }
    public void ScheduleCustom()
    {
        var notificationParams = new NotificationParams
        {
            Id = UnityEngine.Random.Range(0, int.MaxValue),
            Delay = TimeSpan.FromDays(Random.Range(1, 3)),
            Title = "Hey Santa We Need Your Help!",
            Message = "Good children are waiting for you!",
            Ticker = "Ticker",
            Sound = true,
            Vibrate = true,
            Light = true,
            SmallIcon = NotificationIcon.Heart,
            SmallIconColor = new Color(0, 0.5f, 0),
            LargeIcon = "app_icon"
        };
        var notificationParams2 = new NotificationParams
        {
            Id = UnityEngine.Random.Range(0, int.MaxValue),
            Delay = TimeSpan.FromDays(Random.Range(6, 8)),
            Title = "Do You Know This Song?",
            Message = "We wish you a Merry Christmas! We wish you a Merry Christmas!",
            Ticker = "Ticker",
            Sound = true,
            Vibrate = true,
            Light = true,
            SmallIcon = NotificationIcon.Heart,
            SmallIconColor = new Color(0, 0.5f, 0),
            LargeIcon = "app_icon"
        };
        var notificationParams3 = new NotificationParams
        {
            Id = UnityEngine.Random.Range(0, int.MaxValue),
            Delay = TimeSpan.FromDays(Random.Range(3, 6)),
            Title = "Do You Know This Song?",
            Message = "Jingle Bells! Jingle Bells! Jingle all the bells",
            Ticker = "Ticker",
            Sound = true,
            Vibrate = true,
            Light = true,
            SmallIcon = NotificationIcon.Heart,
            SmallIconColor = new Color(0, 0.5f, 0),
            LargeIcon = "app_icon"
        };
        NotificationManager.SendCustom(notificationParams);NotificationManager.SendCustom(notificationParams2);NotificationManager.SendCustom(notificationParams3);
    }
}
