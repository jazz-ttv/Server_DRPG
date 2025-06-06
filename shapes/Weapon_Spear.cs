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
addDamageType("Melee",'<bitmap:add-ons/Weapon_Sword/CI_sword> %1','%2 <bitmap:add-ons/Weapon_Sword/CI_sword> %1',0.75,1);
datablock ProjectileData(woodSpearProjectile)
{
   directDamage        = 2;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(woodSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_woodSpear_Iron";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.276 0.224 0.182 1";

         // Dynamic properties defined by the scripts
        image = woodSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(woodSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = woodSpearItem;
   ammo = " ";
   projectile = woodSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.276 0.224 0.182 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function woodSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function woodSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function woodSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(bronzeSpearProjectile)
{
   directDamage        = 5;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(bronzeSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_bronzeSpear_Iron";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.4 0.2 0 1";

         // Dynamic properties defined by the scripts
        image = bronzeSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(bronzeSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = bronzeSpearItem;
   ammo = " ";
   projectile = bronzeSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.4 0.2 0 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function bronzeSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function bronzeSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function bronzeSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(ironSpearProjectile)
{
   directDamage        = 7;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(ironSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_ironSpear_Iron";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.73 0.62 0.52 1";

         // Dynamic properties defined by the scripts
        image = ironSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(ironSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = ironSpearItem;
   ammo = " ";
   projectile = ironSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.73 0.62 0.52 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function ironSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function ironSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function ironSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(dermiteSpearProjectile)
{
   directDamage        = 9;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(dermiteSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_dermiteSpear_dermite";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.5 0.3 0.3 1";

         // Dynamic properties defined by the scripts
        image = dermiteSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(dermiteSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = dermiteSpearItem;
   ammo = " ";
   projectile = dermiteSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.5 0.3 0.3 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function dermiteSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function dermiteSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function dermiteSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(steelSpearProjectile)
{
   directDamage        = 11;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(steelSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_steelSpear_steel";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.8 0.8 0.8 1";

         // Dynamic properties defined by the scripts
        image = steelSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(steelSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = steelSpearItem;
   ammo = " ";
   projectile = steelSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.8 0.8 0.8 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function steelSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function steelSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function steelSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(destiniteSpearProjectile)
{
   directDamage        = 13;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(destiniteSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_destiniteSpear_destinite";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0.8 0.2 0.5 1";

         // Dynamic properties defined by the scripts
        image = destiniteSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(destiniteSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = destiniteSpearItem;
   ammo = " ";
   projectile = destiniteSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0.8 0.2 0.5 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function destiniteSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function destiniteSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function destiniteSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}
datablock ProjectileData(bironSpearProjectile)
{
   directDamage        = 15;
   directDamageType  = $DamageType::Melee;
   explosion           = toolExplosion;
   //particleEmitter     = as;

   muzzleVelocity      = 50;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 130;
   fadeDelay           = 70;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = false;
   gravityMod = 0.0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   //uiName = "";
};

datablock ItemData(bironSpearItem)
{
        category = "Weapon";  // Mission editor category
        className = "Weapon"; // For inventory system

         // Basic Item Properties
        shapeFile = "./spear.dts";
        mass = 1;
        density = 0.2;
        elasticity = 0.2;
        friction = 0.6;
        emap = true;


        //gui stuff
        uiName = "RPG_bironSpear_biron";
        iconName = "./icon_Spear";
        doColorShift = true;
        colorShiftColor = "0 0 0 1";

         // Dynamic properties defined by the scripts
        image = bironSpearImage;
        canDrop = true;
};

datablock ShapeBaseImageData(bironSpearImage)
{
   // Basic Item properties
   shapeFile = "./Spear.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = false;

   eyeOffset = "0 0 0";

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = bironSpearItem;
   ammo = " ";
   projectile = bironSpearProjectile;
   projectileType = Projectile;

   //melee particles shoot from eye node for consistancy
   melee = true;
   doRetraction = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";
   doColorShift = true;
   colorShiftColor = "0 0 0 1";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]			= "Activate";
	stateTimeoutValue[0]		= 0.1;
	stateTransitionOnTimeout[0]	= "Ready";
	stateSequence[0]		= "ready";

	stateName[1]			= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]	= true;
	
	stateName[2]                    = "Charge";
	stateTransitionOnTimeout[2]	= "Armed";
	stateTimeoutValue[2]            = 0.7;
	stateWaitForTimeout[2]		= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]                  = "onCharge";
	stateAllowImageChange[2]        = false;
	
	stateName[3]			= "AbortCharge";
	stateTransitionOnTimeout[3]	= "Ready";
	stateTimeoutValue[3]		= 0.3;
	stateWaitForTimeout[3]		= true;
	stateScript[3]			= "onAbortCharge";
	stateAllowImageChange[3]	= false;

	stateName[4]			= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]	= false;

	stateName[5]			= "Fire";
	stateTransitionOnTimeout[5]	= "Ready";
	stateTimeoutValue[5]		= 0.5;
	stateFire[5]			= true;
	stateSequence[5]		= "fire";
	stateScript[5]			= "onFire";
	stateWaitForTimeout[5]		= true;
	stateAllowImageChange[5]	= false;
        
};

function bironSpearImage::onCharge(%this, %obj, %slot)
{
	%obj.playthread(2, spearReady);
}

function bironSpearImage::onAbortCharge(%this, %obj, %slot)
{
	%obj.playthread(2, root);
}

function bironSpearImage::onFire(%this, %obj, %slot)
{
	%obj.playthread(2, spearThrow);
	Parent::onFire(%this, %obj, %slot);
}