﻿using System.Drawing;
using DotFeather;

namespace TakeUpJewel
{
	public class EntityParticleBase : EntityLiving
	{
		public EntityParticleBase(Vector pnt, Tile[] obj, byte[,,] chips, EntityList par)
		{
			Location = pnt;
			Mpts = obj;
			Map = chips;
			Parent = par;
			Size = new Size(8, 8);
		}

		public override Texture2D[] ImageHandle => ResourceManager.Particle;


		public override EntityGroup MyGroup => EntityGroup.Particle;

		public override int DyingMax => 0;

		public override void UpdatePhysics(ColliderType top, ColliderType bottom, ColliderType left, ColliderType right)
		{
		}

		public override void SetKilledAnime()
		{
		}

		public override void SetCrushedAnime()
		{
		}

		public override void Kill()
		{
			IsDead = true;
		}

		public override void OnUpdate()
		{
			if ((Location.X < -Size.Width) || (Location.X > Core.I.CurrentMap.Size.X * 16) || (Location.Y < -Size.Height) ||
				(Location.Y > Core.I.CurrentMap.Size.Y * 16))
				Kill(true, false);
			base.OnUpdate();
		}
	}
}