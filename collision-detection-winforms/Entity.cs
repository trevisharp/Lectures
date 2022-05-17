using System.Drawing;

public abstract class Entity
{
    public Entity(HitBox hitbox)
    {
        this.HitBox = hitbox;
    }
    public HitBox HitBox { get; set; }
    public PointF Location { get; set; }

    public void CheckCollision(Entity entity)
    {
        var info = HitBox.IsColliding(entity.HitBox);
        if (info.IsColliding)
            OnCollision(info);
    }

    public virtual void OnCollision(CollisionInfo info) { }
    public virtual void OnFrame() { }
    public abstract void Draw(Graphics g);
}