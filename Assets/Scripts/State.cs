public interface State {
    void Start(Enemy e) { }

    void Tick(Enemy e);

    State Transition(Enemy e) { return null; }

    void Exit(Enemy e) { }
}