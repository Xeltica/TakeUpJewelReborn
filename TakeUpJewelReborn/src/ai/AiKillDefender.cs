using System.Drawing;
using DotFeather;

namespace TakeUpJewel
{
    public class AiKillDefender : AiBase
    {
        public AiKillDefender(EntityLiving host) : base(host) { }

        public override bool Use => !HostEntity.IsDying;

        public override void OnUpdate()
        {
            foreach (EntityPlayer ep in HostEntity.Parent.FindEntitiesByType<EntityPlayer>())
            {
                if (ep.IsDying)
                    continue;
                if (new Rectangle(((VectorInt)ep.Location).ToPoint(), ep.Size)
                    .CheckCollision(new Rectangle((int)HostEntity.Location.X, (int)HostEntity.Location.Y + 8, HostEntity.Size.Width,
                        HostEntity.Size.Height - 8)))
                    ep.Kill();
            }
        }
    }
}