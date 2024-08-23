using System;

[Serializable]
public class LevelSystem
{
    public double currentLevel = 1;
    public double currentExp = 0;
    public LevelConfig config;

    public GameEvent levelup;

    public void GetExp(double exp)
    {
        currentExp += exp;

        if(currentExp >= NextLevel(currentLevel))
        {
            currentLevel++;
            levelup.Raise();
            currentExp = 0;
        }
    }

    public void GetLevel(double level)
    {
        currentLevel = level;
    }

    public double NextLevel(double level)
    {
        return Math.Pow(level + config.levelConfig[(int)level - 1], 2);
    }
}
