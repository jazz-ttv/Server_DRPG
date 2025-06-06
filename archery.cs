// Recipes moved to content/recipes_weapons

// Each of these is a copy of default arrow and stuff
if(!$DamageType::ArrowDirect)
{
	addDamageType("ArrowDirect",'<bitmap:add-ons/Weapon_Bow/CI_arrow> %1','%2 <bitmap:add-ons/Weapon_Bow/CI_arrow> %1',0.5,1);
}
addDamageType("DRPGGun",'<bitmap:base/client/ui/CI/generic> %1','%2 <bitmap:base/client/ui/CI/generic> %1',0.75,1);
if(!isObject(dartArrowProjectile))
{
datablock ProjectileData(dartArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 10;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(bronzeArrowProjectile))
{
datablock ProjectileData(bronzeArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 25;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(silverArrowProjectile))
{
datablock ProjectileData(silverArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 35;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(ironArrowProjectile))
{
datablock ProjectileData(ironArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 65;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(dermiteArrowProjectile))
{
datablock ProjectileData(dermiteArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 45;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(steelArrowProjectile))
{
datablock ProjectileData(steelArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 75;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(destiniteArrowProjectile))
{
datablock ProjectileData(destiniteArrowProjectile)
{
   projectileShapeName = "Add-Ons/Weapon_Bow/arrow.dts";

   directDamage        = 85;
   directDamageType    = $DamageType::ArrowDirect;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::ArrowDirect;

   explosion             = arrowExplosion;
   stickExplosion        = arrowStickExplosion;
   bloodExplosion        = arrowStickExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = true;
   bounceAngle         = 170; //stick almost all the time
   minStickVelocity    = 10;
   bounceElasticity    = 0.2;
   bounceFriction      = 0.01;   
   gravityMod = 0.25;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
//Bullets

if(!isObject(bronzeBulletProjectile))
{
datablock ProjectileData(bronzeBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 26;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(silverBulletProjectile))
{
datablock ProjectileData(silverBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 36;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(ironBulletProjectile))
{
datablock ProjectileData(ironBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 66;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(dermiteBulletProjectile))
{
datablock ProjectileData(dermiteBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 46;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(steelBulletProjectile))
{
datablock ProjectileData(steelBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 76;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}
if(!isObject(destiniteBulletProjectile))
{
datablock ProjectileData(destiniteBulletProjectile)
{
   projectileShapeName = "base/data/shapes/empty.dts";

   directDamage        = 86;
   directDamageType    = $DamageType::DRPGGun;

   radiusDamage        = 0;
   damageRadius        = 0;
   radiusDamageType    = $DamageType::DRPGGun;

   explosion             = arrowExplosion;
   particleEmitter       = arrowTrailEmitter;
   explodeOnPlayerImpact = true;
   explodeOnDeath        = true;  

   armingDelay         = 4000;
   lifetime            = 15000;
   fadeDelay           = 4000;

   isBallistic         = false;
   gravityMod = 0.05;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   muzzleVelocity      = 65;
   velInheritFactor    = 1;

   uiName = "";
};
}

//Items
if(!isObject(oakBowItem))
{
datablock ItemData(oakBowItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Oak Bow";
	iconName = "Add-Ons/Weapon_Bow/icon_bow";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = oakBowImage;
	canDrop = true;
};
}
if(!isObject(willowBowItem))
{
datablock ItemData(willowBowItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Willow Bow";
	iconName = "Add-Ons/Weapon_Bow/icon_bow";
	doColorShift = true;
	colorShiftColor = "0 0.5 0.25";

	 // Dynamic properties defined by the scripts
	image = willowBowImage;
	canDrop = true;
};
}
if(!isObject(mapleBowItem))
{
datablock ItemData(mapleBowItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Maple Bow";
	iconName = "Add-Ons/Weapon_Bow/icon_bow";
	doColorShift = true;
	colorShiftColor = "0.9 0.341 0.078";

	 // Dynamic properties defined by the scripts
	image = mapleBowImage;
	canDrop = true;
};
}
if(!isObject(yewBowItem))
{
datablock ItemData(yewBowItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Yew Bow";
	iconName = "Add-Ons/Weapon_Bow/icon_bow";
	doColorShift = true;
	colorShiftColor = "0.9 0.9 0";

	 // Dynamic properties defined by the scripts
	image = yewBowImage;
	canDrop = true;
};
}
if(!isObject(moonwellBowItem))
{
datablock ItemData(moonwellBowItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Moonwell Bow";
	iconName = "Add-Ons/Weapon_Bow/icon_bow";
	doColorShift = true;
	colorShiftColor = "0.9 0.9 0.9";

	 // Dynamic properties defined by the scripts
	image = moonwellBowImage;
	canDrop = true;
};
}
// Code for drawing
function ShapeBaseImageData::checkArrows(%data,%player,%slot)
{
	if(!isObject(%player.client))
	{
		%player.setImageAmmo(%slot,1);
	}
	%arrows = getField(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),1);
	if(!isObject(%projectile = $DRPG::Items::AmmoProjectile[%arrows]) || $DRPG::Items::AmmoType[%arrows] !$= "BOW")
	{
		centerprint(%player.client,"You have no arrows equipped!",2);
		%player.setImageAmmo(%slot,0);
		return;
	}
	%player.setImageAmmo(%slot,1);
}
function ShapeBaseImageData::checkBullets(%data,%player,%slot)
{
	if(!isObject(%player.client))
	{
		%player.setImageAmmo(%slot,1);
	}
	%bullets = getField(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),1);
	if(!isObject(%projectile = $DRPG::Items::AmmoProjectile[%bullets]) || $DRPG::Items::AmmoType[%bullets] !$= "GUN")
	{
		centerprint(%player.client,"You have no bullets equipped!",2);
		%player.setImageAmmo(%slot,0);
		return;
	}
	%player.setImageAmmo(%slot,1);
}
function ShapeBaseImageData::beginDraw(%data,%player,%slot)
{
	%skill = %player.client.getSkillLevel("archery");
	%time = mClampF(5000 - (mFloor(%skill / 5) * 100),1000,5000);
	commandToClient(%player.client,'DRPGABeginDraw',%time);
	%player.drawTime = $Sim::Time;
	%player.fullDrawTime = %time;
	%player.playThread(1,"armReadyRight");
}
function ShapeBaseImageData::onRelease(%data,%player,%slot)
{
	%time = ($Sim::Time - %player.drawTime) * 1000;
	%perc = (%time / %player.fullDrawTime) * 100;
	// Must draw at least 25%
	if(%perc < 25)
	{
		if(isObject(%player.client))
		{
			commandtoclient(%player.client,'DRPGAEndDraw');
		}
		return;
	}
	%arrows = getField(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),1);
	if(!isObject(%projectile = $DRPG::Items::AmmoProjectile[%arrows]))
	{
		return;
	}
	if(isObject(%player.client))
	{
		commandToClient(%player.client,'DRPGAEndDraw');
		%rem = %player.client.removeItem(%arrows,1);
		if(!%rem)
		{
			servercmdunequip(%player.client,1);
			%player.client.removeItem(%arrows,1);
		}
	}
	%vel = %player.getMuzzleVector(%slot);
	%point = %player.getMuzzlePoint(%slot);
	%scale = mClampF((%perc / 2 + %data.pull),14,200);
	%vel = vectorScale(%vel,%scale);

	%exp = %data.pull / 20;
	if(%exp > 0)
	{
		%player.client.addExp("archery",%exp*$DRPG::Prefs::ExpMultiplier["archery"]);
	}

	%p = new Projectile()
	{
		dataBlock = %projectile;
		initialVelocity = %vel;
		initialPosition = %point;
		sourceObject = %player;
		sourceSlot = %slot;
		client = %player.client;
	};
	MissionCleanup.add(%p);
	if(strPos(%data.getName(),"Blunderbuss") > -1)
		%player.playThread(2,activate);
}
function ShapeBaseImageData::onFinishedFiring(%data,%player,%slot)
{
	if(strPos(%data.getName(),"Blunderbuss") < 0)
		%player.playThread(1,"root");
}
// Items moved to content/items_weapons

package DRPGArchery
{
	function Player::updateArm(%player,%image)
	{
		Parent::updateArm(%player,%image);
		if(!%image.armReady)
		{
			%player.playThread(1,"root");
		}
	}
	function ProjectileData::damage(%this,%obj,%col,%fade,%pos,%normal)
	{
	if((%this.directDamageType == $DamageType::ArrowDirect || %this.directDamageType == $DamageType::DRPGGun)&& (%col.getClassName() $= "Player" || %col.getClassName() $= "AIPlayer"))

		{
			%level = 1;
			if(isObject(%obj.client))
				%level = %obj.client.getSkillLevel();
			%damage = %this.directDamage;
			%vel = vectorLen(%obj.getVelocity());
			%damage *= (%vel / 100);
			%lmulti = ((%level / 15) / 8.5) + 0.9;
			if(%lmulti < 1)
				%lmulti = 1;
			%damage *= %lmulti;
			%col.damage(%obj,%pos,%damage,%this.directDamageType);
			return;
		}
		Parent::damage(%this,%obj,%col,%fade,%pos,%normal);
	}
	function WeaponImage::onUnMount(%this,%obj,%slot,%a,%b)
	{
		Parent::onUnMount(%this,%obj,%slot,%a,%b);
		if(isObject(%obj.client))
		{
			commandtoclient(%obj.client,'DRPGAEndDraw');
		}
	}
};
activatepackage(DRPGArchery);

if(!isObject(oakBowImage))
{
datablock ShapeBaseImageData(oakBowImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = oakBowItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = oakBowItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 10;
	// The additional velocity scale on arrows from it

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;stateSequence[0]="Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "beginDraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]   = "drawRelease";
	stateTransitionOnTimeout[3]     = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]        = false;

	stateName[5]                    = "Wait";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkArrows";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(willowBowImage))
{
datablock ShapeBaseImageData(willowBowImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = willowBowItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = willowBowItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 20;
	// The additional velocity scale on arrows from it

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;stateSequence[0]="Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "beginDraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]   = "drawRelease";
	stateTransitionOnTimeout[3]     = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]        = false;

	stateName[5]                    = "Wait";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkArrows";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(MapleBowImage))
{
datablock ShapeBaseImageData(MapleBowImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = MapleBowItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = MapleBowItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 30;
	// The additional velocity scale on arrows from it

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;stateSequence[0]="Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "beginDraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]   = "drawRelease";
	stateTransitionOnTimeout[3]     = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]        = false;

	stateName[5]                    = "Wait";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkArrows";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(YewBowImage))
{
datablock ShapeBaseImageData(YewBowImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = YewBowItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = YewBowItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 40;
	// The additional velocity scale on arrows from it

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;stateSequence[0]="Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "beginDraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]   = "drawRelease";
	stateTransitionOnTimeout[3]     = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]        = false;

	stateName[5]                    = "Wait";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkArrows";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(MoonwellBowImage))
{
datablock ShapeBaseImageData(MoonwellBowImage)
{
   // Basic Item properties
   shapeFile = "Add-Ons/Weapon_Bow/bow.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
   rotation = eulerToMatrix( "0 0 10" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = MoonwellBowItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = MoonwellBowItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 60;
	// The additional velocity scale on arrows from it

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 1;stateSequence[0]="Fire";
	stateTransitionOnTimeout[0]       = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "beginDraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]        = false;
	stateTimeoutValue[3]            = 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]   = "drawRelease";
	stateTransitionOnTimeout[3]     = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]        = false;

	stateName[5]                    = "Wait";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkArrows";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
//Blunderbuss
if(!isObject(bronzeBlunderbussItem))
{
datablock ItemData(bronzeBlunderbussItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Bronze Blunderbuss";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.4 0.2 0 1";
	image = bronzeBlunderbussImage;
	canDrop = true;
};
}
if(!isObject(bronzeBlunderbussImage))
{
datablock ShapeBaseImageData(bronzeBlunderbussImage)
{
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = bronzeBlunderbussItem;
	ammo = " ";
	projectile = "";
	projectileType = "";
	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = bronzeBlunderbussItem.colorShiftColor;
	pull = 15;
	stateName[0]							= "Activate";
	stateTimeoutValue[0]				 = 1;
	stateSequence[0] 		 = "Fire";
	stateTransitionOnTimeout[0]		 = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]							= "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]			= true;

	stateName[2]						  = "beginDraw";
	stateTransitionOnTimeout[2]	  = "drawTick";
	stateTimeoutValue[2]				= 0.05;
	stateFire[2]						  = false;
	stateAllowImageChange[2]		  = false;
	stateScript[2]						= "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]		  = false;
	stateTimeoutValue[3]				= 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "drawRelease";
	stateTransitionOnTimeout[3]	  = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]		  = false;

	stateName[5]						  = "Wait";
	stateTransitionOnTimeout[5]	  = "Ready";
	stateTimeoutValue[5]				= 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]		  = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkBullets";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(ironBlunderbussItem))
{
datablock ItemData(ironBlunderbussItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Iron Blunderbuss";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.73 0.62 0.52 1";
	image = ironBlunderbussImage;
	canDrop = true;
};
}
if(!isObject(ironBlunderbussImage))
{
datablock ShapeBaseImageData(ironBlunderbussImage)
{
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = ironBlunderbussItem;
	ammo = " ";
	projectile = "";
	projectileType = "";
	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = ironBlunderbussItem.colorShiftColor;
	pull = 35;
	stateName[0]							= "Activate";
	stateTimeoutValue[0]				 = 1;
	stateSequence[0] 		 = "Fire";
	stateTransitionOnTimeout[0]		 = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]							= "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]			= true;

	stateName[2]						  = "beginDraw";
	stateTransitionOnTimeout[2]	  = "drawTick";
	stateTimeoutValue[2]				= 0.05;
	stateFire[2]						  = false;
	stateAllowImageChange[2]		  = false;
	stateScript[2]						= "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]		  = false;
	stateTimeoutValue[3]				= 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "drawRelease";
	stateTransitionOnTimeout[3]	  = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]		  = false;

	stateName[5]						  = "Wait";
	stateTransitionOnTimeout[5]	  = "Ready";
	stateTimeoutValue[5]				= 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]		  = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkBullets";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(dermiteBlunderbussItem))
{
datablock ItemData(dermiteBlunderbussItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Dermite Blunderbuss";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.5 0.3 0.3 1";
	image = dermiteBlunderbussImage;
	canDrop = true;
};
}
if(!isObject(dermiteBlunderbussImage))
{
datablock ShapeBaseImageData(dermiteBlunderbussImage)
{
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = dermiteBlunderbussItem;
	ammo = " ";
	projectile = "";
	projectileType = "";
	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = dermiteBlunderbussItem.colorShiftColor;
	pull = 25;
	stateName[0]							= "Activate";
	stateTimeoutValue[0]				 = 1;
	stateSequence[0] 		 = "Fire";
	stateTransitionOnTimeout[0]		 = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]							= "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]			= true;

	stateName[2]						  = "beginDraw";
	stateTransitionOnTimeout[2]	  = "drawTick";
	stateTimeoutValue[2]				= 0.05;
	stateFire[2]						  = false;
	stateAllowImageChange[2]		  = false;
	stateScript[2]						= "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]		  = false;
	stateTimeoutValue[3]				= 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "drawRelease";
	stateTransitionOnTimeout[3]	  = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]		  = false;

	stateName[5]						  = "Wait";
	stateTransitionOnTimeout[5]	  = "Ready";
	stateTimeoutValue[5]				= 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]		  = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkBullets";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(steelBlunderbussItem))
{
datablock ItemData(steelBlunderbussItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Steel Blunderbuss";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.8 0.8 0.8 1";
	image = steelBlunderbussImage;
	canDrop = true;
};
}
if(!isObject(steelBlunderbussImage))
{
datablock ShapeBaseImageData(steelBlunderbussImage)
{
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = steelBlunderbussItem;
	ammo = " ";
	projectile = "";
	projectileType = "";
	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = steelBlunderbussItem.colorShiftColor;
	pull = 45;
	stateName[0]							= "Activate";
	stateTimeoutValue[0]				 = 1;
	stateSequence[0] 		 = "Fire";
	stateTransitionOnTimeout[0]		 = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]							= "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]			= true;

	stateName[2]						  = "beginDraw";
	stateTransitionOnTimeout[2]	  = "drawTick";
	stateTimeoutValue[2]				= 0.05;
	stateFire[2]						  = false;
	stateAllowImageChange[2]		  = false;
	stateScript[2]						= "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]		  = false;
	stateTimeoutValue[3]				= 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "drawRelease";
	stateTransitionOnTimeout[3]	  = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]		  = false;

	stateName[5]						  = "Wait";
	stateTransitionOnTimeout[5]	  = "Ready";
	stateTimeoutValue[5]				= 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]		  = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkBullets";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(destiniteBlunderbussItem))
{
datablock ItemData(destiniteBlunderbussItem)
{
	category = "Weapon";
	className = "Weapon";
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Destinite Blunderbuss";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.8 0.2 0.5 1";
	image = destiniteBlunderbussImage;
	canDrop = true;
};
}
if(!isObject(destiniteBlunderbussImage))
{
datablock ShapeBaseImageData(destiniteBlunderbussImage)
{
	shapeFile = "Add-Ons/Server_DRPG/shapes/blunderbuss/blunderbuss2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = 0;
	rotation = eulerToMatrix( "0 0 0" );
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = destiniteBlunderbussItem;
	ammo = " ";
	projectile = "";
	projectileType = "";
	melee = false;
	armReady = true;
	doColorShift = true;
	colorShiftColor = destiniteBlunderbussItem.colorShiftColor;
	pull = 60;
	stateName[0]							= "Activate";
	stateTimeoutValue[0]				 = 1;
	stateSequence[0] 		 = "Fire";
	stateTransitionOnTimeout[0]		 = "Ready";
	stateSound[0]			= weaponSwitchSound;

	stateName[1]							= "Ready";
	stateTransitionOnTriggerDown[1]  = "checkArrows";
	stateAllowImageChange[1]			= true;

	stateName[2]						  = "beginDraw";
	stateTransitionOnTimeout[2]	  = "drawTick";
	stateTimeoutValue[2]				= 0.05;
	stateFire[2]						  = false;
	stateAllowImageChange[2]		  = false;
	stateScript[2]						= "beginDraw";
	stateWaitForTimeout[2]		= true;

	stateName[3]			= "drawTick";
	stateAllowImageChange[3]		  = false;
	stateTimeoutValue[3]				= 0.1;
	stateWaitForTimeout[3]		= true;
	stateTransitionOnTriggerUp[3]	= "drawRelease";
	stateTransitionOnTimeout[3]	  = "drawTick";

	stateName[4]			= "drawRelease";
	stateScript[4]			= "onRelease";
	stateTimeoutValue[4]		= 0.2;
	stateWaitForTimeout[4]		= true;
	stateTransitionOnTimeout[4]	= "Wait";
	stateSequence[4]		= "Fire";
	stateAllowImageChange[4]		  = false;

	stateName[5]						  = "Wait";
	stateTransitionOnTimeout[5]	  = "Ready";
	stateTimeoutValue[5]				= 0.5;
	stateScript[5]			= "onFinishedFiring";
	stateAllowImageChange[5]		  = false;
	stateWaitForTimeout[5]		= true;

	stateName[6]			= "checkArrows";
	stateScript[6]			= "checkBullets";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkArrows2";

	stateName[7] = "checkArrows2";
	stateTransitionOnAmmo[7]	= "beginDraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}