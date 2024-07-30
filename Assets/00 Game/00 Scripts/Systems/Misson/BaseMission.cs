using System;
using System.Collections.Generic;

[Serializable]
public class BaseMission
{
    public enum MissionType
    {
        Fighting,
        StopCar,
        Shipping
    }

    public MissionType missionType;

    public List<Enemy> enemyList = new List<Enemy>();

}
