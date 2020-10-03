using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDebugTurnLog
{
    public List<string> m_tilesScannedForTargets = new List<string>();
    
    public List<string> m_possibleUnitTargets = new List<string>();
    public List<string> m_possibleBuildingTargets = new List<string>();

    public List<string> m_vulnerableUnitTargets = new List<string>();
    public List<string> m_vulnerableBuildingTargets = new List<string>();

    public int m_waveNumber;
    public int m_turnNumber;

    public string m_targetGameElementName = null;
    public string m_targetGameTileLocation = null;
}
