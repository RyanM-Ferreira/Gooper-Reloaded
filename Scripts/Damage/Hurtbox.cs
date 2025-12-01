using Godot;

public partial class Hurtbox : Area2D
{
    private bool _isPlayerInside;
    private Area2D _playerHitbox;

    private Timer _damageTimer;

    [Export] private float _timerWait = 0.5f;

    public override void _Ready()
    {
        _damageTimer = GetNode<Timer>("DamageTimer");
        _damageTimer.Timeout += PlayerDamageTick;
    }

    public void OnAreaEntered(Area2D hitbox)
    {
        foreach (var group in hitbox.Owner.GetGroups())
        {
            if (Owner.IsInGroup(group))
            {
                return;
            }
        }

        switch (Owner.Name)
        {
            case "Player":
                Owner.Call("PlayerHurt", hitbox.Get("damage"), hitbox.Get("hitstopTimeScale"), hitbox.Get("hitstopDuration"));
                _isPlayerInside = true;
                _playerHitbox = hitbox;
                _damageTimer.Start();
                break;
            case "TargetDummy":
            case "Brushte":
                Owner.Call("TakeDamage", hitbox.Get("damage"), hitbox.Get("hitstopTimeScale"), hitbox.Get("hitstopDuration"));
                break;
            default:
                Owner.Call("TakeDamage", hitbox.Get("damage"), hitbox.Get("hitstopTimeScale"), hitbox.Get("hitstopDuration"), hitbox.GlobalPosition);
                break;
        }
    }

    public void OnAreaExited(Area2D hitbox)
    {
        if (hitbox.Owner != null && hitbox.Owner.IsInGroup("Player"))
        {
            _isPlayerInside = false;
            _playerHitbox = null;
            _damageTimer.Stop();
        }
    }

    public void PlayerDamageTick()
    {
        if (!_isPlayerInside || _playerHitbox == null)
        {
            return;
        }
        else
        {
            Owner.Call("PlayerHurt", _playerHitbox.Get("damage"), _playerHitbox.Get("hitstopTimeScale"), _playerHitbox.Get("hitstopDuration"));

            _damageTimer.WaitTime = _timerWait;
            _damageTimer.Start();
        }
    }
}