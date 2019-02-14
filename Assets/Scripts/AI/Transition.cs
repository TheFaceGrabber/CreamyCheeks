using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreamyCheaks.AI.Decisions;

namespace CreamyCheaks.AI
{
    [Serializable()]
    public class Transition
    {
        public Decision Decision;

        public State TrueState;
    }
}