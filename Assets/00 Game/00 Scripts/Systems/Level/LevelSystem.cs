using System;

[Serializable]
public class LevelSystem
{
    public double currentLevel = 0;
    public double currentExp = 0;

    public void GetExp(double exp)
    {
        currentExp += exp;

        if(currentExp >= NextLevel(currentLevel))
        {
            currentLevel++;
            currentExp = 0;
        }
    }

    public void GetLevel(double level)
    {
        currentLevel = level;
    }

    private double NextLevel(double level)
    {
        return Math.Round((4 * Math.Pow(level, 3)) / 5);
    }
}
