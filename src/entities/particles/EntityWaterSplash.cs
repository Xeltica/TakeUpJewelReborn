using DotFeather;
using TakeUpJewel.Data;

namespace TakeUpJewel.Entities
{
    [EntityRegistry("WaterSplash", -1)]
    public class EntityWaterSplash : EntityParticleBase
    {
        public EntityWaterSplash(Vector p, Object[] mpts, byte[,,] mp, EntityList par)
            : base(p, mpts, mp, par)
        {
            Velocity.X = Game.GetRand(2) - 1;
            Velocity.Y = -4;
            SetGraphic(0);
        }

        public override void OnUpdate()
        {
            Velocity.Y += 0.3f;
            base.OnUpdate();
        }
    }
}