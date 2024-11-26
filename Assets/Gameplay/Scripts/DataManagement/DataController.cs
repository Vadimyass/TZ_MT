using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using UI.Scripts;
using UnityEngine;

namespace Gameplay.Scripts.DataManagement 
{
        public class DataController
        {
                private static readonly string DATAPATH = (Application.persistentDataPath + "/PlayerData.json");

                public static PlayerPrefsData ReadUserDataFromFileAsync()
                {
                        if (!File.Exists(DATAPATH))
                        {
                                File.WriteAllText(DATAPATH, String.Empty);
                                return null;
                        }

                        var userData = File.ReadAllText(DATAPATH);

                        return JsonConvert.DeserializeObject<PlayerPrefsData>(userData);;
                }


                public static void SaveUserDataToFileAsync(PlayerPrefsData playerPrefsData)
                {
                        
                        var output = JsonConvert.SerializeObject(playerPrefsData);
                        
                        File.WriteAllTextAsync(DATAPATH, output);
                }


                public static void ResetProgress()
                {
                        File.Delete(DATAPATH);
                }
        }
}