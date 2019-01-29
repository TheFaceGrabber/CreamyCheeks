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

        static List<Button> _inputs = new List<Button>(); //All inputs current loaded

        private static bool _isInitialised = false; //Has the Input Manager been initialised?

        #region Util Functions

        /// <summary>
        /// Call this at the when application is launched
        /// </summary>
        /// <param name="saveToFile">Will any changes to auto-saved to the input save value at the end of this functions (recommended)</param>
        /// <returns>Returns true if init process was successful and false if not</returns>
        public static bool Initialise(bool saveToFile = true)
        {
            try
            {
                _inputs = Resources.Load<InputDefaults>("Input Defaults").Inputs; //Load "Input Defaults" file from the Resources folder
                
                //Load and values from the save file and then merge any new inputs into the currently loaded file
                //(This is why you would want to auto save on init)
                var temp = DoLoad(); //Load the save file
                
                for (int i = 0; i < temp.Count; i++)
                {
                    var input = _inputs.FindIndex(x => x.Name == temp[i].Name);
                    if (input != -1)
                    {
                        _inputs[input].PositiveKey = temp[i].PositiveKey;
                        _inputs[input].NegativeKey = temp[i].NegativeKey;
                        _inputs[input].Axis = temp[i].Axis;
                        _inputs[input].ButtonDirection = temp[i].ButtonDirection;
                    }
                }

                if (saveToFile) //Save the updated inputs (if requested as a parameter)
                    DoSave();

                _isInitialised = true; //Finally tell the program that the Input Manager has been initialised

                return true;
            }
            catch (Exception ex) //Catch and log any errors
            {
                Debug.LogError("There was an error when initialising Input Manager: \n" + ex.Message + " - " +
                               ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// Loads all saved buttons from save file
        /// </summary>
        /// <returns>Return list of buttons loaded from file</returns>
        public static List<Button> DoLoad()
        {
            try
            {
                if (Directory.Exists(Path.GetDirectoryName(SAVEPATH))) //Check if the save path exists on the Hard Drive
                {
                    using (TextReader reader = new StreamReader(SAVEPATH)) //Create a text reader than will be disposed of as soon as we leave its block of code
                    {
                        XmlSerializer x = new XmlSerializer(typeof(List<Button>)); 

                        return (List<Button>) x.Deserialize(reader); //Deserialize and return the save file (from XML format)
                    }
                }

                return null;
            }
            catch (Exception ex) //Catch and log any errors
            {
                Debug.LogError("There was an error when saving the inputs: \n" + ex.Message + " - " + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Saves "Inputs" list to a save at "SAVEPATH" in XML format
        /// </summary>
        /// <returns>Returns whether or not the operation was a success</returns>
        public static bool DoSave()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(SAVEPATH))) //Check if the save path exists on the Hard Drive
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(SAVEPATH)); //If not, create it
                }

                using (TextWriter writer = new StreamWriter(SAVEPATH)) //Create a text reader than will be disposed of as soon as we leave its block of code
                {
                    XmlSerializer x = new XmlSerializer(typeof(List<Button>));
                    x.Serialize(writer, _inputs); //Serialize and save the Inputs list to the save file (into XML format)
                }

                return true;
            }
            catch (Exception ex) //Catch and log any errors
            {
                Debug.LogError("There was an error when saving the inputs: \n" + ex.Message + " - " + ex.StackTrace);
            }
            
            return false;
        }

        #endregion

        #region GamePlay Functions
        
        /// <summary>
        /// Use the same way you would use the Unity "GetButton" function
        /// </summary>
        /// <param name="button">The button you want to check</param>
        /// <returns>Return whether or not the button is held down</returns>
        public static bool GetButton(string button)
        {
            if (!_isInitialised) //Make sure that we have been init before running this code
            {
                Debug.LogError(
                    "Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            var b = _inputs.SingleOrDefault(x => x.Name == button); //Find the button with requested name

            if (b == null) //If no button exists, exit out of the function
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            if (string.IsNullOrEmpty(b.Axis)) //Is the axis value assigned?
            {
                return UnityEngine.Input.GetKey(b.PositiveKey); //Get key of keycode
            }

            if (b.ButtonDirection == PositiveNegative.Positive) //Does the button return a positive value from the axis?
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) > 0;
            }
            else
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) < 0;
            }
        }

        /// <summary>
        /// Use the same way you would use the Unity "GetButtonDown" function
        /// </summary>
        /// <param name="button">The button you want to check</param>
        /// <returns>Return whether or not the button was pressed</returns>
        public static bool GetButtonDown(string button)
        {
            if (!_isInitialised) //Make sure that we have been init before running this code
            {
                Debug.LogError(
                    "Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            var b = _inputs.SingleOrDefault(x => x.Name == button); //Find the button with requested name

            if (b == null) //If no button exists, exit out of the function
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            if (string.IsNullOrEmpty(b.Axis)) //Is the axis value assigned?
                return UnityEngine.Input.GetKeyDown(b.PositiveKey); //Get key down of keycode

            if (b.ButtonDirection == PositiveNegative.Positive) //Does the button return a positive value from the axis?
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) > 0;
            }
            else
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) < 0;
            }
        }

        /// <summary>
        /// Use the same way you would use the Unity "GetButtonUp" function
        /// </summary>
        /// <param name="button">The button you want to check</param>
        /// <returns>Return whether or not the button was released</returns>
        public static bool GetButtonUp(string button)
        {
            if (!_isInitialised) //Make sure that we have been init before running this code
            {
                Debug.LogError(
                    "Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return false;
            }

            var b = _inputs.SingleOrDefault(x => x.Name == button); //Find the button with requested name

            if (b == null) //If no button exists, exit out of the function
            {
                Debug.LogError("Button \"" + button + "\" could not be found");
                return false;
            }

            if (string.IsNullOrEmpty(b.Axis)) //Is the axis value assigned?
                return UnityEngine.Input.GetKeyUp(b.PositiveKey); //Get key up of keycode

            if (b.ButtonDirection == PositiveNegative.Positive) //Does the button return a positive value from the axis?
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) > 0;
            }
            else
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis) < 0;
            }
        }

        /// <summary>
        /// Use the same way you would use the Unity "GetAxis" function
        /// </summary>
        /// <param name="axis">The axis you want to check</param>
        /// <returns>Returns the direction that the axis is being held</returns>
        public static float GetAxis(string axis)
        {
            if (!_isInitialised) //Make sure that we have been init before running this code
            {
                Debug.LogError(
                    "Input Manager has not not initialised, you must call InputManager.Initialise() before using an functionallity");
                return 0;
            }

            var b = _inputs.SingleOrDefault(x => x.Name == axis); //Find the button with requested name

            if (b == null) //If no button exists, exit out of the function
            {
                Debug.LogError("Axis \"" + axis + "\" could not be found");
                return 0;
            }

            if (string.IsNullOrEmpty(b.Axis)) //Is the axis value assigned?
            {
                if (UnityEngine.Input.GetKey(b.PositiveKey)) //Return positive value if positive key is held
                    return 1;

                if (UnityEngine.Input.GetKey(b.NegativeKey)) //Return negative value if negative key is held
                    return -1;
            }
            else
            {
                return UnityEngine.Input.GetAxisRaw(b.Axis); //Use Unity's built in Input for joystick/gamepad axis
            }

            return 0;
        }

        #endregion

        #region Key Binding Functions

        /// <summary>
        /// Runs operation to update a key binding
        /// </summary>
        /// <param name="button">The button you wish to update</param>
        /// <param name="type">The direction of the input that you want to update (Positive or Negative)</param>
        /// <param name="save">Whether or not you want to save the new key binding (recommended)</param>
        /// <returns></returns>
        public static IEnumerator UpdateInput(string button, PositiveNegative type = PositiveNegative.Positive, bool save = false)
        {
            var b = _inputs.SingleOrDefault(x => x.Name == button); //Find the button with requested name

            if (b != null) //If no button exists, exit out of the function
            {
                string input = "";
                bool isButton = false;
                PositiveNegative dir = 0;

                yield return new WaitUntil(() => WaitForAnyKey(out input, out isButton, out dir)); //Wait until any input is detected (and assign values with details of said input)
                
                if (isButton) //Was a button and not an axis detected?
                {
                    var code = (KeyCode) Enum.Parse(typeof(KeyCode), input); //Convert from string to KeyCode enum

                    b.Axis = ""; //Clear button axis

                    switch (type) //Update button values
                    {
                        case PositiveNegative.Positive:
                            b.PositiveKey = code;
                            break;
                        case PositiveNegative.Negative:
                            b.NegativeKey = code;
                            break;
                    }
                }
                else
                {
                    //Clear button values
                    b.PositiveKey = KeyCode.None;
                    b.NegativeKey = KeyCode.None;
                    //Updated Unity axis
                    b.Axis = input;
                    //Set Input direction
                    b.ButtonDirection = dir;
                }

                if (save) //Save if requests
                    DoSave();

                Debug.Log("Updated " + button + "'s " + type.ToString() + " key to " + input);
            }
            else
                Debug.LogError("Button \"" + button + "\" could not be found");
        }

        static bool WaitForAnyKey(out string input, out bool isButton, out PositiveNegative direction)
        {
            float axisVal = 0;
            var axis = GetAxis(out axisVal); //Detect any axis (that has been defined within the function)
            if (!string.IsNullOrEmpty(axis)) //Has axis return anything?
            {
                input = axis; //Set output for "input" string
                isButton = false; //Set output for "isButton" bool

                if (axisVal > 0) //Was the axis found to be positive?
                    direction = PositiveNegative.Positive; //Set output for "direction" enum
                else
                    direction = PositiveNegative.Negative; //Set output for "direction" enum

                return true;
            }

            //Loop through all possible buttons that can be pressed
            foreach (var key in Enum.GetNames(typeof(KeyCode)).ToList())
            {
                if (key != "None")
                {
                    var e = (KeyCode)Enum.Parse(typeof(KeyCode), key); //Convert button string to KeyCode enum
                    if (UnityEngine.Input.GetKeyDown(e)) //Detect if it has been pressed
                    {
                        input = e.ToString(); //Set output for "input" string
                        isButton = true; //Set output for "isButton" bool
                        direction = PositiveNegative.Positive; //Set output for "direction" enum (default value since it is not relavant to this input)
                        return true;
                    }
                }
            }

            //No input detected yet, return false will default values
            input = "";
            isButton = false;
            direction = PositiveNegative.Negative;

            return false;
        }

        static string GetAxis(out float value)
        {
            string[] axis = new[] //Predefined axis (to add more you must first add them in the Unity Input window
            {
                "Left Stick X",
                "Left Stick Y",
                "Right Stick X",
                "Right Stick Y",
                "Triggers",
                "DPad Y",
                "DPad X"
            };

            for (int i = 0; i < axis.Length; i++) //Loop through each axis
            {
                float val = UnityEngine.Input.GetAxis(axis[i]);
                if (Mathf.Abs(val) > 0.1f) //Detect whether it is being used
                {
                    value = val; //Set out for the direction of the axis
                    return axis[i]; //Return the axis name
                }
            }

            value = 0;
            return "";
        }

        #endregion
    }


    /// <summary>
    /// Name: The name assigned to this button
    /// PositiveKey: Key that returns true when button is requested or returns 1 when axis is requests
    /// NegativeKey: Key returns -1 when axis is requests
    /// Axis: Axis (held in Unity's Input window) that the user will use to control this input
    /// ButtonDirection: Only applies if used a button, determines the direction the axis (above) must be facing for any button functions to return true
    /// </summary>
    [System.Serializable]
    public class Button
    {
        public string Name;
        public KeyCode PositiveKey;
        public KeyCode NegativeKey;
        public string Axis;
        public PositiveNegative ButtonDirection;

        public override bool Equals(object obj)
        {
            Button b = (Button) obj;
            return b.Name == Name;
        }
    }

    public enum PositiveNegative
    {
        Positive,
        Negative
    }
}
