using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace Overworld.Projectiles.Minions
{
	public abstract class DashFly : Minion
	{
		//Anything labelled "PROTECTED" should be set in SetDefaults() of the projectile. This is most of the following.
		protected float accellerationMult = 0f; //how quickly it accellerates, or something.
		protected float maxSpeed = 12f; //maximum speed normally
		protected float dashSpeed = 20f; //maximum dashing speed
		protected float dashTime = 180f; //Cooldown between dashes.

		protected float detectionRadius = 300f; //how far the minion can see
		protected bool canSeeThroughTiles = true; //Can the minion see through tiles?
		protected float followDistance = 600f; //how far will the minion go before returning to player?
		protected float hoverRadius = 160f; //how far the projectile will passively float around the target
		protected bool killWithoutTarget = false; //Kill the projectile when it doesn't have a target?

		protected float dashLength = 12f; //how far away the minion can dash
		private float dashCounter; //counts dash length in travel, used to modify dash speed slightly

		private Vector2 targetPos;
		private int AiState; //0 = resting, 1 = attacking, 2 = dashing
		private int dashes = 0; //How many times it has consecutively dashed.

		#region Timers
		public float DashCooldown //Just the dash timer / cooldown. Fancy way of getting projectile.ai[0]
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		#endregion
		public override void Behavior()
		{
			bool defaultRotation = ModifyRotation(AiState);
			Player player = Main.player[projectile.owner]; //projectile's owner.
			bool hasTarget = false;
			targetPos = player.position; //Vector2 to aim for
			if (AiState != 2) //This prevents any of the following from running while it dashes
			{
				if (!hasTarget) //Will always run, but I don't really care.
				{
					float distance = detectionRadius * 2f;
					for (int i = 0; i < Main.maxNPCs; i++) //iterate through Main.npc[]
					{
						NPC target = Main.npc[i]; //get each target
						if (Vector2.Distance(projectile.position, target.position) <= detectionRadius && Vector2.Distance(projectile.position, target.position) < distance && !target.friendly && target.type != NPCID.TargetDummy && target.lifeMax > 10 && target.active && target.life > 0)
						{
							distance = projectile.Distance(target.position);
							//Make a ton of comparisons. In essence, target is in range, not dead, not friendly, has more hp than a worm, and isn't a target dummy
							if (!canSeeThroughTiles) //if the projectile *can't* "see" through tiles
							{
								if (projectile.ownerHitCheck) //then check to see if the owner can see the target
								{
									hasTarget = true; //has a target
									targetPos = target.position; //has coords
								}
							}
							else //if the projectile *can* "see" through tiles
							{
								hasTarget = true; //see above
								targetPos = target.position;
							}
						}
					}
				}

				if (hasTarget) //ai change
				{
					projectile.velocity = Vector2.Zero;
					AiState = 1; //attacking
				}
				else if (!hasTarget) //ai change
				{
					if (killWithoutTarget)
					{
						projectile.timeLeft = 2;
						projectile.active = false;
						projectile.Kill();
					}
					else
						AiState = 0; //idle
				}

				if (AiState == 0) //idle
				{
					if (defaultRotation)
						projectile.rotation = 0;
					if (projectile.Distance(player.position) <= followDistance && projectile.Distance(player.position) > 128f) //if the projectile is a distance away from the player
					{
						projectile.velocity = (projectile.DirectionTo(player.position) * maxSpeed) * (1 + accellerationMult * 0.6f); //change the velocity towards the player
					}
					else if (projectile.Distance(player.position) > followDistance) //if the projectile is further away from the player
					{
						projectile.velocity = (projectile.DirectionTo(player.position) * maxSpeed) * (1 + accellerationMult * 1.4f); //change velocity towards the player, but faster
					}
					if (projectile.velocity == Vector2.Zero) //just so there's no non-moving projectiles.
						projectile.velocity = Vector2.One; //set it to (1,1)
				}
				if (AiState == 1) //attacking
				{
					if (projectile.velocity == Vector2.Zero) //just so there's no non-moving projectiles.
						projectile.velocity.Y = -1f; //set y-vel to -1
					if (defaultRotation)
						projectile.rotation = 0;
					if (Vector2.Distance(projectile.position, targetPos) > hoverRadius * 0.9f) //if the distance is greater than the hoverRadius
					{
						projectile.velocity += (Vector2.Normalize(targetPos - projectile.position) * maxSpeed) * (1 + accellerationMult); //add velocity towards the target position
						MathHelper.Clamp(projectile.velocity.X, -maxSpeed, maxSpeed); //max x vel
						MathHelper.Clamp(projectile.velocity.Y, -maxSpeed, maxSpeed); //max y vel
					}
					if (Vector2.Distance(projectile.position, targetPos) <= hoverRadius * 1.1f) //if distance is less than hoverRadius
					{
						if (DashCooldown >= dashTime) //check to see if dash is off cooldown.
						{
							AiState = 2; //set it to dashing, to ignore everything before this
							projectile.velocity = Vector2.Normalize(targetPos - projectile.position) * (dashSpeed * 1.8f); //set the base velocity for the dash
							dashCounter = dashLength; //set the counter
							DashCooldown = 0f; //start cooldown
						}
						projectile.velocity += (Vector2.Normalize(targetPos - projectile.position) * maxSpeed) * -(1 + accellerationMult); //if this isn't dashing, set the velocity
						MathHelper.Clamp(projectile.velocity.X, -maxSpeed, maxSpeed); //max x vel
						MathHelper.Clamp(projectile.velocity.Y, -maxSpeed, maxSpeed); //max y vel
					}
				}
				if (projectile.Distance(player.position) >= followDistance + detectionRadius * 2f) //if this gets too far from the player...
				{
					projectile.position = player.Center; //teleport back
					AiState = 0; //set the ai to idle
				}
			}
			if (AiState == 2) //during the dash
			{
				if (dashCounter > dashLength * 0.95f) //during the start
					projectile.velocity *= (1 + accellerationMult); //slight accelleration for a whole almost no time lol.
				if (dashCounter < dashLength * 0.05f) //during the end
					projectile.velocity *= (1 - accellerationMult); //slight deccelleration for a whole almost no time xD.
				if (dashCounter <= 0f) //when the counter runs out (<= 0)
				{
					AiState = 1; //set ai state to attacking
					float temp = 0f;
					PostDash(dashes+1);
					temp = ModifyPostDash(dashes++);
					DashCooldown = dashTime - temp;
					projectile.ai[1] = dashTime;
					if (DashCooldown >= dashTime)
						dashes++;
					else
						dashes = 0;
				}
				dashCounter -= 1f; //subtract from dash counter, so it can properly reset ai state
				if (defaultRotation)
					projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2; //rotation. Assumes projectile is facing upwards
			}
			if (DashCooldown < dashTime * 1.5f) //ensures not to reach or surpass float.maxValue
				DashCooldown += 1f; //decrease cooldown (I know it's backwards, shut up)
		}
		public override void OnCreated()
		{
				SafeOnCreated();
				DashCooldown = dashTime - ModifyCooldown(DashCooldown);
		}

		/// <summary>
		/// Called when the projectile is created.
		/// </summary>
		public virtual void SafeOnCreated() { }

		/// <summary>
		/// Called when the projectile is created to modify the time before next dash. 0 = immediately.
		/// </summary>
		/// <param name="dashCooldown"></param>
		public virtual float ModifyCooldown(float dashCooldown) { return dashCooldown; }

		/// <summary>
		/// Called before AI. Return false to handle rotation manually. Returns true by default.
		/// </summary>
		/// <param name="currentAI">0 = idle, 1 = found enemy, 2 = dashing</param>
		public virtual bool ModifyRotation(int currentAI) { return true; }

		/// <summary>
		/// Called after dashing. Might use this for something idk.
		/// </summary>
		/// <param name="consecutiveDashes">Number of dashes consecutively</param>
		public virtual void PostDash(int consecutiveDashes) { }

		/// <summary>
		/// Allows modification of the dash cooldown after dashing. Can be used to string multiple together. Returns max cooldown by default. 0 is no time before next attack
		/// </summary>
		/// <param name="consecutiveDashes">Number of consecutive dash attacks</param>
		/// <returns></returns>
		public virtual float ModifyPostDash(int consecutiveDashes) { return dashTime; }

		/// <summary>
		/// Returns the location the projectile is aiming for.
		/// </summary>
		public Vector2 AimedLocation { get { return targetPos; } }
	}
}
