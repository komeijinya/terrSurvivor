using Godot;
using System;

public partial class BoomArea : HitBox
{
    private CollisionShape2D shape;

    public float Radius = 70;

    [Export] public PackedScene BoomParticle;

    private bool avilibile = false;

    public override void _Ready()
    {
        base._Ready();
        

    }
    public void Set()
    {
        CircleShape2D Boomarea = new CircleShape2D();
        GD.Print("fact: ", Radius);
        Boomarea.Radius = Radius;
        shape = GetNode<CollisionShape2D>("CollisionShape2D");
        shape.Shape = Boomarea;

    }

    public override void _Process(double delta)
    {
       
        if (avilibile)
        {
            
            GpuParticles2D BoomParticleInstance = BoomParticle.Instantiate<GpuParticles2D>();
            GetTree().GetFirstNodeInGroup("Particles").AddChild(BoomParticleInstance);
            BoomParticleInstance.GlobalPosition = GlobalPosition;
            base._Process(delta);
            
        }

    }

    public void Boom()
    {
        avilibile = true;
    }

   public override void _Draw()
    {
        DrawCircle(Vector2.Zero, Radius, new Color(1, 0, 0, 0.5f));
        GD.Print(Radius);
    }



}
