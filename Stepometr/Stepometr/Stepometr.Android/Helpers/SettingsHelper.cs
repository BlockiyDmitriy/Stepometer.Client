using Android.App;
using Android.Content;
using Android.OS;
using System;
using Android.Preferences;

namespace Stepometer.Droid.Helpers
{
    internal class SettingsHelper
    {
        public static ISharedPreferences SharedPreferences { get; set; }
        private static ISharedPreferencesEditor SharedPreferencesEditor { get; set; }
        private readonly object locker = new object();

        public SettingsHelper()
        {
            SharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            SharedPreferencesEditor = SharedPreferences.Edit();
        }

        /// <summary>
        /// Gets the current value or the default that you specify.
        /// </summary>
        /// <typeparam name="T">Vaue of t (bool, int, float, long, string)</typeparam>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <returns>Value or default</returns>
        public T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            lock (locker)
            {
                Type typeOf = typeof(T);
                if (typeOf.IsGenericType && typeOf.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    typeOf = Nullable.GetUnderlyingType(typeOf);
                }

                object value = null;
                var typeCode = Type.GetTypeCode(typeOf);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        value = SharedPreferences.GetBoolean(key, Convert.ToBoolean(defaultValue));
                        break;
                    case TypeCode.Int64:
                        value = SharedPreferences.GetLong(key, Convert.ToInt64(defaultValue));
                        break;
                    case TypeCode.String:
                        value = SharedPreferences.GetString(key, Convert.ToString(defaultValue));
                        break;
                    case TypeCode.Int32:
                        value = SharedPreferences.GetInt(key, Convert.ToInt32(defaultValue));
                        break;
                    case TypeCode.Single:
                        value = SharedPreferences.GetFloat(key, Convert.ToSingle(defaultValue));
                        break;
                    case TypeCode.DateTime:
                        var ticks = SharedPreferences.GetLong(key, -1);
                        if (ticks == -1)
                            value = defaultValue;
                        else
                            value = new DateTime(ticks);
                        break;
                }

                return null != value ? (T) value : defaultValue;
            }
        }

        /// <summary>
        /// Adds or updates a value
        /// </summary>
        /// <param name="key">key to update</param>
        /// <param name="value">value to set</param>
        /// <returns>True if added or update and you need to save</returns>
        public bool AddOrUpdateValue(string key, object value)
        {
            lock (locker)
            {
                Type typeOf = value.GetType();
                if (typeOf.IsGenericType && typeOf.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    typeOf = Nullable.GetUnderlyingType(typeOf);
                }

                var typeCode = Type.GetTypeCode(typeOf);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        SharedPreferencesEditor.PutBoolean(key, Convert.ToBoolean(value));
                        break;
                    case TypeCode.Int64:
                        SharedPreferencesEditor.PutLong(key, Convert.ToInt64(value));
                        break;
                    case TypeCode.String:
                        SharedPreferencesEditor.PutString(key, Convert.ToString(value));
                        break;
                    case TypeCode.Int32:
                        SharedPreferencesEditor.PutInt(key, Convert.ToInt32(value));
                        break;
                    case TypeCode.Single:
                        SharedPreferencesEditor.PutFloat(key, Convert.ToSingle(value));
                        break;
                    case TypeCode.DateTime:
                        SharedPreferencesEditor.PutLong(key, ((DateTime) (object) value).Ticks);
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Saves out all current settings
        /// </summary>
        public void Save()
        {
            lock (locker)
            {
                SharedPreferencesEditor.Commit();
            }
        }
    }
}