using Godot;

public partial class Hurtbox : Area2D
{
    public void OnAreaEntered(Area2D hitbox)
    {
        foreach (var item in hitbox.Owner.GetGroups())
        {
            int i = 0;
            if (item  == Owner.GetGroups()[i])
            {
                return;
            }
            i++;
        }    


        if (Owner.Name == "TargetDummy")
        {
            Owner.Call("TakeDamage", hitbox.Get("damage"), hitbox.Get("hitstopTimeScale"), hitbox.Get("hitstopDuration"));
        }
        else
        {
            Owner.Call("TakeDamage", hitbox.Get("damage"), hitbox.Get("hitstopTimeScale"), hitbox.Get("hitstopDuration"), hitbox.GlobalPosition);
        }

    }



}
