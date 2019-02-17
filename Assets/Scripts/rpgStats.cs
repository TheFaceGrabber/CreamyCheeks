using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rpgStats : MonoBehaviour {

    public int StartHealth, StartSanity, StartStrength, StartAgility, StartIntelligence, StartWillpower, StartPerception, StartCharisma; // public ints to enter a characters starting stats.

    public struct stat
    {
        private int value;  // the value of the stat

        public void SetValue(int val)   // Set stat to specific value.
        {
            if(val <= 10 && val >= 0) // Stats all have a range of 0 to 10. If the desired value is within this range, set the value.
            {
                value = val;
            } else // If the desired value is outside the range, do not set the value and log the message.
            {
                Debug.Log("Desired value is not within stat range");
            }
        }
        
        public void Add(int val)    // Used to add and subtract values from stat.
        {
            if(value + val >= 0 && value + val <= 10)   // If result value is within range, do the math.
            {
                value += val;
            } else if(value + val >= 10)    // If result value is above max, set to max.
            {
                value = 10;
            } else      // If the code runs to this point it means the result value is less than the min, therefore, set the value to min.
            {
                value = 0;
            }
        }

        public bool RollCheck()
        {
            if(Random.Range(1, value) <= value)
            {
                return true;
            } else
            {
                return false;
            }
        }

    };

    public stat Health, Sanity, Strength, Agility, Intelligence, Willpower, Perception, Charisma; // Stats that are accessed through other scripts. E.g. GetComponent<rpgStats>().health.Add(-2);

    private void Awake()    // All stat values set to their starting values.
    {
        Health.SetValue(StartHealth);
        Sanity.SetValue(StartSanity);
        Strength.SetValue(StartStrength);
        Agility.SetValue(StartAgility);
        Intelligence.SetValue(StartIntelligence);
        Willpower.SetValue(StartWillpower);
        Perception.SetValue(StartPerception);
        Charisma.SetValue(StartCharisma);
        GameObject.Find("UI").GetComponent<UIManager>().SetStats(StartStrength, StartAgility, StartIntelligence, StartWillpower, StartPerception, StartCharisma); //This line added by Lewis. Will crash however if value is over max value. Need to update
    }
}
