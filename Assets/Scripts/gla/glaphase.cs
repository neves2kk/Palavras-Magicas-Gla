using System;
using System.Collections.Generic;

[Serializable]
public class GlaPhase
{
    public string phase_id;
    public List<GlaSection> sections = new List<GlaSection>();
}