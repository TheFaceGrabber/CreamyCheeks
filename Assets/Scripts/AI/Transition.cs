using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable()]
public class Transition
{
    public Decision Decision;

    public State[] TrueState;

    public State[] FalseState;
}