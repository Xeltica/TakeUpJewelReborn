﻿using System.Drawing;
using DotFeather;

namespace TakeUpJewel
{
	[EntityRegistry("Boxer", 16)]
	public class EntityBoxer : EntityLiving
	{
		public EntityBoxer(Vector pnt, Tile[] obj, byte[,,] chips, EntityList par)
		{
			Location = pnt;
			Mpts = obj;
			Map = chips;
			Parent = par;
			Size = new Size(16, 32);
			CollisionAIs.Add(new AiKillDefender(this));
			SetAnime(0, 1, 8);
		}

		public override Texture2D[] ImageHandle => ResourceManager.Boxer;


		public override EntityGroup MyGroup => EntityGroup.Enemy;

		public override void SetKilledAnime()
		{
			SetGraphic(0);
		}

		public override void SetCrushedAnime()
		{
			SetGraphic(0);
			IsCrushed = false;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			Velocity.X = -1;

			if (Misc.CheckHit(new Vector(Location.X - 16, Location.Y + Size.Height / 2)) == ColliderType.Land && IsOnLand)
			{
				Velocity.Y = -3.4f;
			}

			if (IsDying)
				Velocity = Vector.Zero;
		}

		public override Entity SetEntityData(dynamic jsonobj)
		{
			base.SetEntityData((object)jsonobj);
			return this;
		}
	}
}