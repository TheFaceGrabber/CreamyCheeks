using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

namespace CreamyCheaks.Input
{
    public class InputManager
    {
        public static readonly string SAVEPATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                                 Path.DirectorySeparatorChar + "CreamyCheeks" +
                                                 Path.DirectorySeparatorChar + "HorrorGame" +
                                                 Path.DirectorySeparatorChar + "Input.xml";

        public static List<Button> Inputs = new List<Button>();

        private static bool _isInitialised = false;
        
        public static bool Initialise(bool saveToFile = false)
        {
            try
            {
                Inputs = Resources.Load<InputDefaults>("Input Defaults").Inputs;
                _isInitialised = true;

                DoLoad();

                if (saveToFile)
                    DoSave();

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("There was an error when initialising Input Manager: \n" + ex.Message + " - " +
                               ex.StackTrace);
            }

            return false;
        }

        public static bool DoLoad()
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(SAVEPATH)))
                {
                    using (TextReader reader = new StreamReader(SAVEPATH))
                    {
                        XmlSerializer x = new XmlSerializer(typeof(List<Button>));

                        Inputs = (List<Button>) x.Deserialize(reader);
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.LogError("There was an error when saving the inputs: \n" + ex.Message + " - " + ex.StackTrace);
            }
            
            return false;
        }

        public static bool DoSave()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(SAVEPATH)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(SAVEPATH));
                }

                using (TextWriter writer = new StreamWriter(SAVEPATH))
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<Button>));
                    x.Serialize(writer, Inputs);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("There was an error when saving the inputs: \n" + ex.Message + " - " + ex.StackTrace);
            }
            
            return false;
        }

        public static bool GetButton(string button)
        {
            if (!_isInitialised)
            {
                Debug.LogError("Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            var b = Inputs.SingleOrDefault(x => x.Name == button);

            if (b == null)
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            return UnityEngine.Input.GetKey(b.PositiveKey);
        }

        public static bool GetButtonDown(string button)
        {
            var b = Inputs.SingleOrDefault(x => x.Name == button);

            if (!_isInitialised)
            {
                Debug.LogError("Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            if (b == null)
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            return UnityEngine.Input.GetKeyDown(b.PositiveKey);
        }

        public static bool GetButtonUp(string button)
        {
            var b = Inputs.SingleOrDefault(x => x.Name == button);

            if (!_isInitialised)
            {
                Debug.LogError("Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            if (b == null)
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            return UnityEngine.Input.GetKeyUp(b.PositiveKey);
        }

        public static float GetAxis(string axis)
        {
            var b = Inputs.SingleOrDefault(x => x.Name == axis);

            if (!_isInitialised)
            {
                Debug.LogError("Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return 0;
            }

            if (b == null)
            {
                Debug.LogError("Axis \"" + axis + "\" could not be found");
                return 0;
            }

            if (UnityEngine.Input.GetKey(b.PositiveKey))
                return 1;

            if (UnityEngine.Input.GetKey(b.NegativeKey))
                return -1;

            return 0;
        }

        public static IEnumerator UpdateInput(string button, UpdateType type = UpdateType.Positive, bool save = false)
        {
            var b = Inputs.SingleOrDefault(x => x.Name == button);

            if (b != null)
            {
                yield return new WaitUntil(() => UnityEngine.Input.anyKeyDown);

                KeyCode input = KeyCode.None;

                foreach (var key in Enum.GetNames(typeof(KeyCode)).ToList())
                {
                    if (key != "None")
                    {
                        var e = (KeyCode) Enum.Parse(typeof(KeyCode), key);
                        if (UnityEngine.Input.GetKeyDown(e))
                        {
                            input = e;
                            break;
                        }
                    }
                }

                Debug.Log(input);

                switch (type)
                {
                    case UpdateType.Positive:
                        b.PositiveKey = input;
                        break;
                    case UpdateType.Negative:
                        b.NegativeKey = input;
                        break;
                }

                if (save)
                    DoSave();

                Debug.Log("Updated " + button + "'s " + type.ToString() + " key to " + input);
            }
            else
                Debug.LogError("Button \"" + button + "\" could not be found");
        }
    }

    /// <summary>
    /// Name: The name assigned to this button
    /// ButtonName: The name of the input as found in the Unity Input Window
    /// IsAxis: To determine whether or not to use this input as an axis
    /// </summary>
    [System.Serializable]
    public class Button
    {
        public string Name;
        public KeyCode PositiveKey;
        public KeyCode NegativeKey;
    }

    public enum UpdateType
    {
        Positive,
        Negative
    }
}
