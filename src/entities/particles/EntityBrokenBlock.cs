using DotFeather;
using TakeUpJewel.Data;
using TakeUpJewel.Util;

namespace TakeUpJewel.Entities
{
    [EntityRegistry("BrokenBlock", -1)]
    public class EntityBrokenBlock : EntityParticleBase
    {
        public EntityBrokenBlock(Vector p, Object[] mpts, byte[,,] mp, EntityList par)
            : base(p, mpts, mp, par)
        {
            Velocity.X = Game.GetRand(4) - 2;
            Velocity.Y = -Game.GetRand(5) - 5;
            SetGraphic(DevelopmentUtility.GetRandom(18, 19, 50, 51));
        }

        public override Texture2D[] ImageHandle => ResourceUtility.MapChipMini;

        public override void OnUpdate()
        {
            Velocity.Y += 0.3f;
            base.OnUpdate();
        }
    }
}