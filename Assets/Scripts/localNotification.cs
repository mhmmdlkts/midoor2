using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IPHONE
    using Unity.Notifications.iOS;
#elif UNITY_ANDROID
    using Unity.Notifications.Android;
#endif
public class localNotification : MonoBehaviour
{

    public static void createNotification(string title, string body, string subtitle)
    {
        #if UNITY_IPHONE
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(0, 0, 1),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            // You can specify a custom identifier which can be used to manage the notification later.
            // If you don't provide one, a unique string will be generated automatically.
            Identifier = "_notification_01",
            Title = title,
            Body = body,
            Subtitle = subtitle,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);

        #elif UNITY_ANDROID

            var notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = subtitle;
            notification.FireTime = System.DateTime.Now;

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        #endif
    }
}
