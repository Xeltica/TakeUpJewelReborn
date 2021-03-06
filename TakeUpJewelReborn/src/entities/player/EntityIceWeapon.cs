using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DotFeather;

namespace TakeUpJewel
{
	public class EntityIceWeapon : EntityLiving
	{
		public const int SpeedX = 4;

		private readonly int _defspeed = 0;
		public int Life = 100;

		public EntityIceWeapon(Vector pnt, Tile[] obj, byte[,,] chips, EntityList par)
		{
			Location = pnt;
			Mpts = obj;
			Map = chips;
			Parent = par;
			Size = new Size(16, 16);
			SetGraphic(14);
		}

		public override Texture2D[] ImageHandle => ResourceManager.Weapon;

		public override EntityGroup MyGroup => EntityGroup.DefenderWeapon;

		public override Sounds KilledSound => Sounds.Null;

		public override void SetKilledAnime()
		{
		}

		public override void SetCrushedAnime()
		{
		}

		public override void OnUpdate()
		{
			UpdateBehavior();
			if ((int)Velocity.X == 0)
				Velocity.X = _defspeed * 4f;

			base.OnUpdate();
		}

		private void UpdateBehavior()
		{
			if (Location.X < 0)
				Kill();

			if (Location.X > Core.I.CurrentMap.Size.X * 16 - 16)
				Kill();

			if (Location.Y < 0)
			{
				Velocity.Y = 0;
				Location.Y = 0;
			}

			if (Location.Y > Core.I.CurrentMap.Size.Y * 16)
				Kill(true, false);

			foreach (EntityLiving e in Parent.FindEntitiesByType<EntityLiving>().ToList())
				if (!e.IsDying && (e.MyGroup == EntityGroup.Enemy) &&
					new RectangleF(Location.ToPoint(), Size).CheckCollision(new RectangleF(e.Location.ToPoint(), e.Size)))
					e.Kill();

			if (Life < 1)
				Kill();

			if (Life == 70)
			{
				SetGraphic(15);
			}
			if (Life == 35)
			{
				SetGraphic(16);
			}

			if ((CollisionLeft() == ColliderType.Land) || (CollisionRight() == ColliderType.Land))
				Velocity.X *= -1;
			Life--;
		}

		public override void Dying()
		{
			IsDead = true;
		}

		public override Entity SetEntityData(dynamic jsonobj)
		{
			base.SetEntityData((object)jsonobj);
			if (jsonobj.IsDefined("SpeedX"))
				Velocity.X = (float)jsonobj.SpeedX;
			return this;
		}
	}
}