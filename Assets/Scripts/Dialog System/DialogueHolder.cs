using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CreamyCheaks.AI;
using CreamyCheaks.Input;
using UnityEngine;
using UnityEngine.UI;

namespace CreamyCheaks.DialogSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        [System.Serializable]
        public class ReplyBox
        {
            public GameObject Panel;
            public Text Text;
        }

        public Text SpeechText;
        public GameObject SpeechPanel;

        public List<ReplyBox> ReplyBoxes = new List<ReplyBox>();
        public GameObject ReplyPanel;

        public bool IsDialogueRunning { get; private set; }

        private bool doSkip;
        private Branch curBranch;

        private FiniteStateMachine curTalkingTo;

        private PlayerController.PlayerController player;
        
        void Awake()
        {
            player = GetComponent<PlayerController.PlayerController>();
        }

        void Update()
        {
            if(IsDialogueRunning)
            {
                if (InputManager.GetButtonDown("Cancel Interaction"))
                {
                    StartCoroutine(End());
                }

                if (curTalkingTo)
                {
                    var dir = curTalkingTo.transform.position - transform.position;

                    transform.eulerAngles = new Vector3(transform.eulerAngles.x,
                        Quaternion.LookRotation(dir).eulerAngles.y, transform.eulerAngles.z);
                }
            }
        }
        public void BeginDialogue(Branch branch, FiniteStateMachine ai)
        {
            if (IsDialogueRunning) return;
            
            if (branch != null)
            {
                IsDialogueRunning = true;
                curTalkingTo = ai;
                StartCoroutine(Run(branch));
            }
            else
            {
                IsDialogueRunning = false;
            }
        }

        public void OptionPressed(int option)
        {
            if (option == 0)
            {
                if (curBranch != null)
                {
                    if (curBranch.GetReplies()[0].NextBranch != null)
                    {
                        StartCoroutine(Run(curBranch.GetReplies()[0].NextBranch));
                    }
                    else
                    {
                        StartCoroutine(End());
                    }
                }
            }
            else if (option == 1)
            {
                if (curBranch != null)
                {
                    if (curBranch.GetReplies()[1].NextBranch != null)
                    {
                        StartCoroutine(Run(curBranch.GetReplies()[1].NextBranch));
                    }
                    else
                    {
                        StartCoroutine(End());
                    }
                }
            }
            else if (option == 2)
            {
                if (curBranch != null)
                {
                    if (curBranch.GetReplies()[2].NextBranch != null)
                    {
                        StartCoroutine(Run(curBranch.GetReplies()[2].NextBranch));
                    }
                    else
                    {
                        StartCoroutine(End());
                    }
                }
            }
        }

        IEnumerator End()
        {
            if (curTalkingTo)
            {
                curTalkingTo.EndTalk();
                curTalkingTo = null;
            }

            IsDialogueRunning = false;
            curBranch = null;
            ReplyPanel.SetActive(false);
            SpeechPanel.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            player.SetAllowInput(true);
            StopAllCoroutines();
        }

        IEnumerator Run(Branch branch)
        {
            player.SetAllowInput(false);

            curBranch = branch;
            ReplyPanel.SetActive(false);
            SpeechPanel.SetActive(true);
            foreach (string line in branch.Text)
            {
                doSkip = false;

                int index = branch.Text.IndexOf(line);
                if (branch.VoiceOver.ElementAtOrDefault(index) != null)
                {
                    GameObject.Find("SfxPlayer").GetComponent<SfxPlayer>().PlaySfx(branch.VoiceOver[index],transform.position);
                    //TODO PLAY SOUND
                    //GameManager.Instance.SoundsManager.PlaySound(branch.VoiceOver[index]);
                }

                int length = line.Length;
                SpeechText.text = "";

                for (int i = 0; i < length; i++)
                {
                    if (doSkip)
                    {
                        doSkip = false;
                        break;
                    }

                    SpeechText.text += line[i];
                    yield return new WaitForSeconds(0.0625f);
                }

                SpeechText.text = line;
                yield return new WaitForSeconds(1f);
                doSkip = true;
                yield return new WaitUntil(() => doSkip == true);
            }

            SpeechPanel.SetActive(false);

            if (branch.GetReplies().Count > 0)
            {
                ReplyPanel.SetActive(true);
                for (int i = 0; i < ReplyBoxes.Count; i++)
                {
                    if (branch.GetReplies().ElementAtOrDefault(i) != null)
                    {
                        ReplyBoxes[i].Text.text = branch.GetReplies()[i].Text;
                        ReplyBoxes[i].Panel.SetActive(true);
                        ReplyBoxes[i].Panel.GetComponent<UnityEngine.UI.Button>().Select();
                    }
                    else
                    {
                        ReplyBoxes[i].Panel.SetActive(false);
                    }
                }
            }
            else
            {
                StartCoroutine(End());
            }
        }
    }
}
