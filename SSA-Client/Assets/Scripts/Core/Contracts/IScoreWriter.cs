
public interface IScoreWriter
{
    int CurrentScore { get; }
    void AddScore(int value);
    void ResetScore();
}
