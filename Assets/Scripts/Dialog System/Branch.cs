using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreamyCheaks.DialogSystem
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="New Branch", menuName = "Dialogue System/Create Branch")]
    public class Branch : ScriptableObject
    {
        public List<string> Text = new List<string> (){ "Aela, ic aem Dwarhmann" };
        public List<AudioClip> VoiceOver = new List<AudioClip>();

        public List<Reply> Replies = new List<Reply>();

        public List<Reply> GetReplies()
        {
            return Replies;
        }

        public void AddReply(Reply r)
        {
            if (Replies.Count < 3)
                Replies.Add(r);
            else
                throw new System.Exception("Replies are full!");
        }

        [System.Serializable]
        public class Reply
        {
            public string Text = "Alea Dwarhmann, hu farest þu?";
            public Branch NextBranch;
            public bool FoldOut;
        }
    }
}