using System;

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
}
