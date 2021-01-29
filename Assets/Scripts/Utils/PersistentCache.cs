using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Stores data in persistent data storage.
/// </summary>
public static class PersistentCache
{
    public static bool Initialized = false;
    private static string persistentDataPath;
    public static string cacheDataPath;

    public static TimeSpan MaxCacheKeepingTime;

    public static void Init()
    {
        Initialized = true;
        MaxCacheKeepingTime = new TimeSpan(7, 0, 0, 0);
        persistentDataPath = Application.persistentDataPath;

        cacheDataPath = Path.Combine(persistentDataPath, "PersistentCache");

        if (!Directory.Exists(cacheDataPath))
            Directory.CreateDirectory(cacheDataPath);
    }

    /// <summary>
    /// Clean cache directory
    /// </summary>
    public static void ClearPersistentStorage()
    {
        if (!Initialized)
            Init();

        if (Directory.Exists(cacheDataPath))
        {
            Directory.Delete(cacheDataPath, true);
            Directory.CreateDirectory(cacheDataPath);
        }
    }

    public static T TryLoad<T>(bool removeIfOutOfDate = false)
    {
        return TryLoad<T>(typeof(T).Name, removeIfOutOfDate);
    }

    public static T TryLoad<T>(string key, bool removeIfOutOfDate = false)
    {
        if (!Initialized)
            Init();

        var fullPath = GetPath(key);
        if (File.Exists(fullPath))
        try
        {
            var lastWrite = File.GetLastWriteTime(fullPath);
            if (!removeIfOutOfDate || DateTime.Now - lastWrite < MaxCacheKeepingTime)
            {
                using (var fs = File.OpenRead(fullPath))
                    return (T)new BinaryFormatter().Deserialize(fs);
            }else
            {
                File.Delete(fullPath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return default(T);
    }

    public static bool Save<T>(T data)
    {
        return Save(data.GetType().Name, data);
    }

    public static bool Save<T>(string key, T data)
    {
        if (!Initialized)
            Init();

        var fullPath = GetPath(key);
        try
        {
            using (var fs = File.Create(fullPath))
                new BinaryFormatter().Serialize(fs, data);
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return false;
    }

    public static byte[] TryLoad(string key, bool removeIfOutOfDate = false)
    {
        if (!Initialized)
            Init();

        var fullPath = GetPath(key);
        if (File.Exists(fullPath))
        try
        {
            var lastWrite = File.GetLastWriteTime(fullPath);

            if (!removeIfOutOfDate || DateTime.Now - lastWrite < MaxCacheKeepingTime)
            {
                return File.ReadAllBytes(fullPath);
            }
            else
            {
                File.Delete(fullPath);
            }
        }
        catch { }

        return null;
    }

    public static string Save(string key, byte[] bytes)
    {
        if (!Initialized)
            Init();

        var fullPath = GetPath(key);
        File.WriteAllBytes(fullPath, bytes);
        return fullPath;
    }

    public static string GetPath(string key)
    {
        return Path.Combine(cacheDataPath, WWW.EscapeURL(key));
    }

    public static bool Remove(string key)
    {
        var fullName = GetPath(key);
        if (File.Exists(fullName))
        {
            File.Delete(fullName);
            return true;
        }

        return false;
    }
}