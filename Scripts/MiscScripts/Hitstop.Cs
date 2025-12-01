using Godot;
public partial class HitStop
{
    public async void Hitstop(double timescale, double duration, Node Owner)
    {
        double originalTimeScale = 1;
        Engine.TimeScale = timescale;
        await Owner.ToSignal(Owner.GetTree().CreateTimer(duration * timescale, processInPhysics: true), "timeout");
        Engine.TimeScale = originalTimeScale;
    }
}