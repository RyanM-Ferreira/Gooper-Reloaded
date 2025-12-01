using Godot;
public partial class HitStop
{
    public async void Hitstop(double timescale, double duration, Node Owner)
    {
        if (double.IsNaN(timescale) || double.IsNaN(duration))
        {
            GD.PrintErr("Go and fall of a cliff.");
            return;
        }

        if (timescale <= 0.05)
        {
            timescale = 0.05;
        }

        double originalTimeScale = 1;
        Engine.TimeScale = timescale;

        await Owner.ToSignal(Owner.GetTree().CreateTimer(duration * timescale, processInPhysics: true), "timeout");
        Engine.TimeScale = originalTimeScale;
    }
}