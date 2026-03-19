
public interface IScorePersistence
{
    void SaveScore(string sceneName,int score);
    int LoadScore(string sceneName);
    int LoadHighScore(string sceneName);
}
