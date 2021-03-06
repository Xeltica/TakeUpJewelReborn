using System.Drawing;
using DotFeather;
using Codeplex.Data;

namespace TakeUpJewel
{
	[EntityRegistry("Coin", 28)]
	public class EntityCoin : EntitySprite
	{
		private bool _animating;
		private WorkingType _worktype = WorkingType.Normal;

		public EntityCoin(Vector pnt, Tile[] obj, byte[,,] chips, EntityList par)
		{
			Location = pnt;
			Mpts = obj;
			Map = chips;
			Parent = par;
			Size = new Size(16, 16);
			SetAnime(6, 9, 8);

			Velocity.Y = -12;
		}

		public override Texture2D[] ImageHandle => ResourceManager.Item;


		public override EntityGroup MyGroup => EntityGroup.Stage;

		public override Entity SetEntityData(dynamic jsonobj)
		{
			if (jsonobj.IsDefined("WorkingType"))
				_worktype = (WorkingType)jsonobj.WorkingType;
			base.SetEntityData((object)jsonobj);
			if (_worktype == WorkingType.FromBlock)
				AnimeSpeed = 3;
			return this;
		}

		public override void OnUpdate()
		{
			if (!_animating)
				switch (_worktype)
				{
					case WorkingType.Normal:
						Velocity.Y = 0;
						foreach (var entity in Parent.FindEntitiesByType<EntityPlayer>())
						{
							var ep = (EntityPlayer)entity;
							if (!ep.IsDying && new RectangleF(ep.Location.ToPoint(), ep.Size).CheckCollision(new RectangleF(Location.ToPoint(), Size)))
								_animating = true;
						}
						break;
					case WorkingType.FromBlock:
						_animating = true;
						DESound.Play(Sounds.GetCoin);
						Core.I.Coin++;
						break;
				}

			if (_animating)
				switch (_worktype)
				{
					case WorkingType.Normal:
						IsDead = true;
						Parent.Add(Core.I.EntityRegistry.CreateEntity("Coin", Location, Mpts, Map, Parent,
							DynamicJson.Parse(@"{""WorkingType"": 1}")));
						break;
					case WorkingType.FromBlock:
						Velocity.Y *= 0.8f;
						if (Velocity.Y > -0.1f)
							Kill();
						break;
				}

			base.OnUpdate();
		}
	}
}