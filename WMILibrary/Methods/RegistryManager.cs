using Common;
using System;
using Microsoft.Win32;

namespace CheckConnection.Methods
{
    public class RegistryManager<T> : ClassWithLogger<RegistryManager<T>>
    {
        string _path;
        public RegistryManager(string path)
        {
            _path = path;
        }

        public T GetValue(string valueName)
        {
            T keyvalue = default(T);
            try
            {
                keyvalue = (T)Registry.GetValue(_path, valueName, null);
            }
            catch(Exception ex)
            {
                log.Error("Ошибка чтения реестра: ", ex);
            } 
            return keyvalue;
        }

        public int SetValue(string valueName, T keyvalue)
        {
            int ret = 1;
            try
            {
                Registry.SetValue(_path, valueName, keyvalue);
            }
            catch (Exception ex)
            {
                log.Error("Ошибка записи в реестр: ", ex);
                ret = 0;
            }
            return ret;
        }
    }
}
