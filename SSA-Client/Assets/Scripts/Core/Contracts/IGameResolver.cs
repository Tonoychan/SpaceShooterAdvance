
public interface IGameResolver
{
    void ResolveWin();
    void ResolveLose();
    void StartResolveSequence(float delayInSeconds = 2f);
    void ResolveGame();
}
