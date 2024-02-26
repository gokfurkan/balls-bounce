namespace Game.Dev.Scripts
{
    public enum SceneType
    {
        Load,
        Game,
    }

    public enum PanelType
    {
        Dev,
        OpenSettings,
        Settings, 
        Win, 
        Lose, 
        Level, 
        Money, 
        Restart,
        EndContinue,
        Upgrade,
        LevelProgress,
    }

    public enum LevelTextType
    {
        Level,
        LevelCompleted,
        LevelFailed,
    }

    public enum IncomeTextType
    {
        Win,
    }

    public enum CameraType
    {
        Menu,
    }

    public enum AudioType
    {
        Pad,
        MergeStart,
        MergeComplete,
        Pop, 
    }

    public enum PoolType
    {
        IncomeVisual,
    }

    public enum UpgradeType
    {
        AddPad,
        AddBall,
        MergeBall,
    }
}