﻿using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AppSettingService : IAppSettings
    {
        private static AppSettingService _instance;
        private static readonly object ObjLocked = new object();
        private IConfiguration _configuration;

        //protected AppSettingService()
        //{
        //}

        public void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public static AppSettingService Instance
        //{
        //    get
        //    {
        //        if (null == _instance)
        //        {
        //            lock (ObjLocked)
        //            {
        //                if (null == _instance)
        //                    _instance = new AppSettingService();
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        public bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(_configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetConnection(string key, string defaultValue = "")
        {
            try
            {
                return _configuration.GetConnectionString(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        public int GetInt32(string key, int defaultValue = 0)
        {
            try
            {
                return Int32.Parse(_configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public long GetInt64(string key, long defaultValue = 0L)
        {
            try
            {
                return Int64.Parse(_configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key).Value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                var value = _configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key)?.Value;
                return string.IsNullOrEmpty(value) ? defaultValue : value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return _configuration.Get<T>();
            else
                return _configuration.GetSection(key).Get<T>();
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (_configuration.GetSection(key) == null)
                return defaultValue;

            if (string.IsNullOrWhiteSpace(key))
                return _configuration.Get<T>();
            else
                return _configuration.GetSection(key).Get<T>();
        }

        //public static T GetObject<T>(string key = null)
        //{
        //    if (string.IsNullOrWhiteSpace(key))
        //        return Instance._configuration.Get<T>();
        //    else
        //    {
        //        var section = Instance._configuration.GetSection(key);
        //        return section.Get<T>();
        //    }
        //}

        //public static T GetObject<T>(string key, T defaultValue)
        //{
        //    if (Instance._configuration.GetSection(key) == null)
        //        return defaultValue;

        //    if (string.IsNullOrWhiteSpace(key))
        //        return Instance._configuration.Get<T>();
        //    else
        //        return Instance._configuration.GetSection(key).Get<T>();
        //}
    }
}
