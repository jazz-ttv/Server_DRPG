addDamageType("Melee",'<bitmap:add-ons/Weapon_Sword/CI_sword> %1','%2 <bitmap:add-ons/Weapon_Sword/CI_sword> %1',0.75,1);
if(!isObject(toolExplosionParticle))
{
datablock ParticleData(toolExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 1.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   spinRandomMin = -90;
   spinRandomMax = 90;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 300;
   textureName          = "base/data/particles/star1.png";
   colors[0]     = "0.7 0.7 0.9 0.9";
   colors[1]     = "0.9 0.9 0.9 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.25;
};
}

if(!isObject(toolExplosionEmitter))
{
datablock ParticleEmitterData(toolExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 8;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "toolExplosionParticle";
};
}
if(!isObject(toolExplosion))
{
datablock ExplosionData(toolExplosion)
{
   soundProfile = hammerHitSound;
   lifeTimeMS = 500;
   particleEmitter = toolExplosionEmitter;
   particleDensity = 10;
   particleRadius = 0.2;
   faceViewer     = true;
   explosionScale = "1 1 1";
   shakeCamera = false;
   lightStartRadius = 3;
   lightEndRadius = 0;
   lightStartColor = "00.0 0.2 0.6";
   lightEndColor = "0 0 0";
};
}
if(!isObject(bronzeShortSwordProjectile))
{
datablock ProjectileData(bronzeShortSwordProjectile)
{
   directDamage        = 2;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(bronzeShortSwordItem))
{
datablock ItemData(bronzeShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Bronze Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.4 0.2 0 1";
	image = bronzeShortSwordImage;
	canDrop = true;
};
}
if(!isObject(bronzeShortSwordImage))
{
datablock ShapeBaseImageData(bronzeShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = bronzeShortSwordItem;
   ammo = " ";
   projectile = bronzeShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0.4 0.2 0 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
if(!isObject(ironShortSwordProjectile))
{
datablock ProjectileData(ironShortSwordProjectile)
{
   directDamage        = 5;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(ironShortSwordItem))
{
datablock ItemData(ironShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Iron Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.73 0.62 0.52 1";
	image = ironShortSwordImage;
	canDrop = true;
};
}
if(!isObject(ironShortSwordImage))
{
datablock ShapeBaseImageData(ironShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = ironShortSwordItem;
   ammo = " ";
   projectile = ironShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0.73 0.62 0.52 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
if(!isObject(dermiteShortSwordProjectile))
{
datablock ProjectileData(dermiteShortSwordProjectile)
{
   directDamage        = 7;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(dermiteShortSwordItem))
{
datablock ItemData(dermiteShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Dermite Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.5 0.3 0.3 1";
	image = dermiteShortSwordImage;
	canDrop = true;
};
}
if(!isObject(dermiteShortSwordImage))
{
datablock ShapeBaseImageData(dermiteShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = dermiteShortSwordItem;
   ammo = " ";
   projectile = dermiteShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0.5 0.3 0.3 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
if(!isObject(steelShortSwordProjectile))
{
datablock ProjectileData(steelShortSwordProjectile)
{
   directDamage        = 9;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(steelShortSwordItem))
{
datablock ItemData(steelShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Steel Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.8 0.8 0.8 1";
	image = steelShortSwordImage;
	canDrop = true;
};
}
if(!isObject(steelShortSwordImage))
{
datablock ShapeBaseImageData(steelShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = steelShortSwordItem;
   ammo = " ";
   projectile = steelShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0.8 0.8 0.8 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
if(!isObject(destiniteShortSwordProjectile))
{
datablock ProjectileData(destiniteShortSwordProjectile)
{
   directDamage        = 11;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(destiniteShortSwordItem))
{
datablock ItemData(destiniteShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Destinite Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.8 0.2 0.5 1";
	image = destiniteShortSwordImage;
	canDrop = true;
};
}
if(!isObject(destiniteShortSwordImage))
{
datablock ShapeBaseImageData(destiniteShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = destiniteShortSwordItem;
   ammo = " ";
   projectile = destiniteShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0.8 0.2 0.5 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
if(!isObject(bironShortSwordProjectile))
{
datablock ProjectileData(bironShortSwordProjectile)
{
   directDamage        = 13;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(bironShortSwordItem))
{
datablock ItemData(bironShortSwordItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/shortSword2.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Black Iron Shortsword";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0 0 0 1";
	image = bironShortSwordImage;
	canDrop = true;
};
}
if(!isObject(bironShortSwordImage))
{
datablock ShapeBaseImageData(bironShortSwordImage)
{
   shapeFile = "./shapes/shortSword2.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = bironShortSwordItem;
   ammo = " ";
   projectile = bironShortSwordProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = true;
   colorShiftColor = "0 0 0 1";
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
function bronzeShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function bronzeShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function silverShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function silverShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function goldShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function ironShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function ironShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function steelShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function steelShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function goldShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function destiniteShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function destiniteShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function bironShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function bironShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function dermiteShortSwordImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
function dermiteShortSwordImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
if(!isObject(axeProjectile))
{
datablock ProjectileData(axeProjectile)
{
   directDamage        = 0;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(axeItem))
{
datablock ItemData(axeItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/axe.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Axe";
	iconName = "./shapes/icons/axe";
	doColorShift = true;
		colorShiftColor = "0.9 0.9 0.9 1";
	image = axeImage;
	canDrop = true;
};
}
if(!isObject(axeImage))
{
datablock ShapeBaseImageData(axeImage)
{
   shapeFile = "./shapes/axe.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = axeItem;
   ammo = " ";
   projectile = axeProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = false;
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
function axeImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function axeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
if(!isObject(sickleProjectile))
{
datablock ProjectileData(sickleProjectile)
{
   directDamage        = 0;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(sickleItem))
{
datablock ItemData(sickleItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/sickle.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Sickle";
	iconName = "./shapes/icons/sickle";
	doColorShift = false;
	image = sickleImage;
	canDrop = true;
};
}
if(!isObject(sickleImage))
{
datablock ShapeBaseImageData(sickleImage)
{
   shapeFile = "./shapes/sickle.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = sickleItem;
   ammo = " ";
   projectile = sickleProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = false;
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
function sickleImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function sickleImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
if(!isObject(pickaxeProjectile))
{
datablock ProjectileData(pickaxeProjectile)
{
   directDamage        = 0;
   explosion           = toolExplosion;
   muzzleVelocity      = 50;
   velInheritFactor    = 1;
   armingDelay         = 0;
   lifetime            = 100;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;directDamageType=$DamageType::Melee;
};
}
if(!isObject(pickaxeItem))
{
datablock ItemData(pickaxeItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system
	shapeFile = "./shapes/pickaxe.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;
	uiName = "Pickaxe";
	iconName = "./shapes/icons/pickaxe";
	doColorShift = false;
	image = pickaxeImage;
	canDrop = true;
};
}
if(!isObject(pickaxeImage))
{
datablock ShapeBaseImageData(pickaxeImage)
{
   shapeFile = "./shapes/pickaxe.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0"; 
   correctMuzzleVector = false;
   className = "WeaponImage";
   item = pickaxeItem;
   ammo = " ";
   projectile = pickaxeProjectile;
   projectileType = Projectile;
   melee = true;
   doRetraction = false;
   armReady = true;
   doColorShift = false;
	stateName[0]                     = "Activate";
	stateTimeoutValue[0]             = 0.5;
	stateTransitionOnTimeout[0]      = "Ready";

	stateName[1]                     = "Ready";
	stateTransitionOnTriggerDown[1]  = "PreFire";
	stateAllowImageChange[1]         = true;

	stateName[2]			= "PreFire";
	stateScript[2]                  = "onPreFire";
	stateAllowImageChange[2]        = false;
	stateTimeoutValue[2]            = 0.1;
	stateTransitionOnTimeout[2]     = "Fire";

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "CheckFire";
	stateTimeoutValue[3]            = 0.2;
	stateFire[3]                    = true;
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateScript[3]                  = "onFire";
	stateWaitForTimeout[3]		= true;

	stateName[4]			= "CheckFire";
	stateTransitionOnTriggerUp[4]	= "StopFire";
	stateTransitionOnTriggerDown[4]	= "Fire";

	
	stateName[5]                    = "StopFire";
	stateTransitionOnTimeout[5]     = "Ready";
	stateTimeoutValue[5]            = 0.2;
	stateAllowImageChange[5]        = false;
	stateWaitForTimeout[5]		= true;
	stateSequence[5]                = "StopFire";
	stateScript[5]                  = "onStopFire";
};
}
function pickaxeImage::onPreFire(%this, %obj, %slot)
{
	%obj.playthread(2, armattack);
}
function pickaxeImage::onStopFire(%this, %obj, %slot)
{	
	%obj.playthread(2, root);
}
