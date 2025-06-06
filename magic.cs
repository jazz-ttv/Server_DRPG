if(!isObject(oakStaffItem))
{
datablock ItemData(oakStaffItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/staff.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Oak Staff";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.400 0.196 0 1.000";

	 // Dynamic properties defined by the scripts
	image = oakStaffImage;
	canDrop = true;
};
}
if(!isObject(willowStaffItem))
{
datablock ItemData(willowStaffItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/staff.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Willow Staff";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0 0.5 0.25";

	 // Dynamic properties defined by the scripts
	image = willowStaffImage;
	canDrop = true;
};
}
if(!isObject(mapleStaffItem))
{
datablock ItemData(mapleStaffItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/staff.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Maple Staff";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.9 0.341 0.078";

	 // Dynamic properties defined by the scripts
	image = mapleStaffImage;
	canDrop = true;
};
}
if(!isObject(yewStaffItem))
{
datablock ItemData(yewStaffItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/staff.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Yew Staff";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.9 0.9 0";

	 // Dynamic properties defined by the scripts
	image = yewStaffImage;
	canDrop = true;
};
}
if(!isObject(moonwellStaffItem))
{
datablock ItemData(moonwellStaffItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./shapes/staff.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Moonwell Staff";
	iconName = "";
	doColorShift = true;
	colorShiftColor = "0.9 0.9 0.9";

	 // Dynamic properties defined by the scripts
	image = moonwellStaffImage;
	canDrop = true;
};
}

function ShapeBaseImageData::checkSpells(%data,%player,%slot)
{
	if(!isObject(%player.client))
	{
		%player.setImageAmmo(%slot,1);
	}
	%spells = getField(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),1);
	if(!isObject(%projectile = $DRPG::Items::AmmoProjectile[%spells]) || $DRPG::Items::AmmoType[%spells] !$= "STAFF")
	{
		centerprint(%player.client,"You have no spells ready to be casted!",2);
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
	%spells = getField(strreplace($DRPG::Accounts.value[%player.client.bl_id,"equipment"],"|","\t"),1);
	if(!isObject(%projectile = $DRPG::Items::AmmoProjectile[%spells]))
	{
		return;
	}
	if(isObject(%player.client))
	{
		commandToClient(%player.client,'DRPGAEndDraw');
		%rem = %player.client.removeItem(%spells,1);
		if(!%rem)
		{
			 unequip(%player.client,1);
			%player.client.removeItem(%spells,1);
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
}

//---------------------------------------------------------------------------------------\\

if(!isObject(oakstaffImage))
{
datablock ShapeBaseImageData(oakstaffImage)
{
   // Basic Item properties
   shapeFile = "./shapes/staff.dts";
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
   item = oakstaffItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = oakstaffItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 10;
	// The additional velocity scale on Spells from it

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
	stateTransitionOnTriggerDown[1]  = "checkSpells";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "begindraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "begindraw";
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

	stateName[6]			= "checkSpells";
	stateScript[6]			= "checkSpells";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkSpells2";

	stateName[7] = "checkSpells2";
	stateTransitionOnAmmo[7]	= "begindraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(willowstaffImage))
{
datablock ShapeBaseImageData(willowstaffImage)
{
   // Basic Item properties
   shapeFile = "./shapes/staff.dts";
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
   item = willowstaffItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = willowstaffItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 20;
	// The additional velocity scale on Spells from it

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
	stateTransitionOnTriggerDown[1]  = "checkSpells";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "begindraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "begindraw";
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

	stateName[6]			= "checkSpells";
	stateScript[6]			= "checkSpells";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkSpells2";

	stateName[7] = "checkSpells2";
	stateTransitionOnAmmo[7]	= "begindraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(MaplestaffImage))
{
datablock ShapeBaseImageData(MaplestaffImage)
{
   // Basic Item properties
   shapeFile = "./shapes/staff.dts";
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
   item = MaplestaffItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = MaplestaffItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 30;
	// The additional velocity scale on Spells from it

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
	stateTransitionOnTriggerDown[1]  = "checkSpells";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "begindraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "begindraw";
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

	stateName[6]			= "checkSpells";
	stateScript[6]			= "checkSpells";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkSpells2";

	stateName[7] = "checkSpells2";
	stateTransitionOnAmmo[7]	= "begindraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}
if(!isObject(YewstaffImage))
{
datablock ShapeBaseImageData(YewstaffImage)
{
   // Basic Item properties
   shapeFile = "./shapes/staff.dts";
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
   item = YewstaffItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = YewstaffItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 40;
	// The additional velocity scale on Spells from it

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
	stateTransitionOnTriggerDown[1]  = "checkSpells";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "begindraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "begindraw";
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

	stateName[6]			= "checkSpells";
	stateScript[6]			= "checkSpells";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkSpells2";

	stateName[7] = "checkSpells2";
	stateTransitionOnAmmo[7]	= "begindraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

if(!isObject(MoonwellstaffImage))
{
datablock ShapeBaseImageData(MoonwellstaffImage)
{
   // Basic Item properties
   shapeFile = "./shapes/staff.dts";
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
   item = MoonwellstaffItem;
   ammo = " ";
   projectile = "";
   projectileType = "";

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = MoonwellstaffItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

	pull = 60;
	// The additional velocity scale on Spells from it

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
	stateTransitionOnTriggerDown[1]  = "checkSpells";
	stateAllowImageChange[1]         = true;

	stateName[2]                    = "begindraw";
	stateTransitionOnTimeout[2]     = "drawTick";
	stateTimeoutValue[2]            = 0.05;
	stateFire[2]                    = false;
	stateAllowImageChange[2]        = false;
	stateScript[2]                  = "begindraw";
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

	stateName[6]			= "checkSpells";
	stateScript[6]			= "checkSpells";
	stateTimeoutValue[6] = 0.01;
	stateWaitForTimeout[6] = true;
	stateTransitionOnTimeout[6] = "checkSpells2";

	stateName[7] = "checkSpells2";
	stateTransitionOnAmmo[7]	= "begindraw";
	stateTransitionOnNoAmmo[7]	= "Activate";
};
}

datablock ParticleData(IceTrailParticle)
{
	  gravityCoefficient   = 0;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 400;
	lifetimeVarianceMS	= 100;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;



	textureName		= "./shapes/Spark3";

	//Interpolation variables
	colors[0]	= "0.2 0.2 0.8 1";
	colors[1]	= "0.3 0.3 0.6 1";
	colors[2]	= "0.1 0.1 0.5 1";

	sizes[0]	= 0.7;
	sizes[1]	= 1.1;
	sizes[2]	= 0.1;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(IceTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 1;
   ejectionVelocity = 0.1;
   ejectionOffset   = 0.3;
   velocityVariance = 0.1;
   thetaMin         = 0;
   thetaMax         = 45;
   phiReferenceVel  = 36000;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "IceTrailParticle";
   uiName = "IceTrail";

};

datablock ParticleData(IceExplosionParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 10.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "./shapes/Spark3";
	//Interpolation variables
	colors[0]	= "0.5 0.5 0.8 1";
	colors[1]	= "0.4 0.4 0.6 1";
	colors[2]	= "0.2 0.2 0.5 1";

	sizes[0]	= 1.5;
	sizes[1]	= 0.7;
	sizes[2]	= 0.00;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(IceExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 500;
   ejectionVelocity = 0;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "IceExplosionParticle";

   useEmitterColors = true;
   uiName = "IceExplode";
};

datablock ExplosionData(IceExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;


   emitter[0] = IceExplosionEmitter;
   //particleDensity = 30;
   //particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "9 9 9";
   camShakeAmp = "9 9 9";
   camShakeDuration = 0.2;
   camShakeRadius = 5.0;

   // Dynamic light
   lightStartRadius = 4;
   lightEndRadius = 3;
   lightStartColor = "0 0 0.5";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3;
   impulseForce = 400;

   //radius damage
   radiusDamage        = 5;
   damageRadius        = 2;
};


datablock ProjectileData(IceProjectile)
{
   projectileShapeName ="base/data/shapes/empty.dts";
   directDamage        = 50;
 //  directDamageType  = $DamageType::MagicDirect;
   impactImpulse	   = 1000;
   verticalImpulse	   = 700;
   explosion           = IceExplosion;
   particleEmitter     = IceTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;
   brickExplosionMaxVolume = 200;
   brickExplosionMaxVolumeFloating = 200;

   muzzleVelocity      = 60;
   velInheritFactor    = 1;

   armingDelay         = 500;
   lifetime            = 5000;
   fadeDelay           = 4500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Ice";
};

datablock ParticleData(FireTrailParticle)
{
	  gravityCoefficient   = 0;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 400;
	lifetimeVarianceMS	= 100;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;


	textureName		= "./shapes/Spark3";

	//Interpolation variables
	colors[0]	= "1 0.5 0 1";
	colors[1]	= "1 0.4 0 1";
	colors[2]	= "1 0.2 0 1";
	sizes[0]	= 0.7;
	sizes[1]	= 1.1;
	sizes[2]	= 0.1;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(FireTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 1;
   ejectionVelocity = 0.1;
   ejectionOffset   = 0.3;
   velocityVariance = 0.1;
   thetaMin         = 0;
   thetaMax         = 45;
   phiReferenceVel  = 36000;
   phiVariance      = 30;
   overrideAdvance = false;
   particles = "FireTrailParticle";
   uiName = "FireTrail";

};

datablock ParticleData(FireExplosionParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 10.0;
	lifetimeMS		= 600;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "./shapes/Spark3";
	//Interpolation variables
	colors[0]	= "1 0.5 0 1";
	colors[1]	= "1 0.4 0 1";
	colors[2]	= "1 0.2 0 1";

	sizes[0]	= 6.5;
	sizes[1]	= 3.3;
	sizes[2]	= 0.00;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(FireExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 2;
   lifetimeMS       = 500;
   ejectionVelocity = 2;
   velocityVariance = 1.0;
   ejectionOffset   = 1.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "FireExplosionParticle";

   useEmitterColors = true;
   uiName = "FireExplode";
};

datablock ExplosionData(FireExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 200;


   emitter[0] = FireExplosionEmitter;
   particleDensity = 30;
   particleRadius = 1.0;
soundProfile = FireExplodeSound;
   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.5;
   camShakeRadius = 15.0;

   // Dynamic light
   lightStartRadius = 4;
   lightEndRadius = 3;
   lightStartColor = "1 0.2 0.0";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 2000;

   //radius damage
   radiusDamage        = 10;
   damageRadius        = 1.5;
};


datablock ProjectileData(FireProjectile)
{
   projectileShapeName ="base/data/shapes/empty.dts";
   directDamage        = 80;
 //  directDamageType  = $DamageType::MagicDirect;
   impactImpulse	   = 1000;
   verticalImpulse	   = 1000;
   explosion           = FireExplosion;
   particleEmitter     = FireTrailEmitter;

   brickExplosionRadius = 3;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;
   brickExplosionMaxVolume = 200;
   brickExplosionMaxVolumeFloating = 200;

   muzzleVelocity      = 60;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 20000;
   fadeDelay           = 19500;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 0.20;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Fire Projectile";
};

datablock ParticleData(DarknessTrailParticle)
{
	  gravityCoefficient   = -1;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "./shapes/spark1";

	//Interpolation variables
	colors[0]	= ".596 .161 .392 1";
	colors[1]	= ".878 .561 .957 1";
	colors[2]	= ".85 .85 .85 1";
	sizes[0]	= 1.5;
	sizes[1]	= 0.5;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(DarknessTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 1;
   ejectionVelocity = 1;
   ejectionOffset   = 0.1;
   velocityVariance = 1;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 12000;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "DarknessTrailParticle";
   uiName = "DarknessTrail";

};

datablock ParticleData(DarknessExplosionParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 10.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "./shapes/spark1";
	//Interpolation variables
	colors[0]	= ".596 .161 .392 1";
	colors[1]	= ".878 .561 .957 1";
	colors[2]	= ".85 .85 .85 1";

	sizes[0]	= 6;
	sizes[1]	= 5;
	sizes[2]	= 0.00;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(DarknessExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 1;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "DarknessExplosionParticle";

   useEmitterColors = true;
   uiName = "DarknessExplode";
};

datablock ExplosionData(DarknessExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

soundProfile = "";
   emitter[0] = DarknessExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.0;
   camShakeRadius = 0.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = ".596 .161 .392";
   lightEndColor = ".85 .85 .85";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 500;

   //radius damage
   radiusDamage        = 0.5;
   damageRadius        = 1;
};


datablock ProjectileData(DarknessProjectile)
{
   projectileShapeName ="base/data/shapes/empty.dts";
   directDamage        = 70;
 //  directDamageType  = $DamageType::MagicDirect;
   impactImpulse	   = 10;
   verticalImpulse	   = 10;
   explosion           = DarknessExplosion;
   particleEmitter     = DarknessTrailEmitter;

   brickExplosionRadius = 0.1;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 0.1;
   brickExplosionMaxVolume = 20;
   brickExplosionMaxVolumeFloating = 20;

   muzzleVelocity      = 70;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 850;
   fadeDelay           = 650;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 10;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = ".596 .161 .392";

   uiName = "Darkness Projectile";
};


datablock ParticleData(DiseaseTrailParticle)
{
	  gravityCoefficient   = -1;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "./shapes/spark2";

	//Interpolation variables
	colors[0]	= "0 .5 .25 1";
	colors[1]	= "0 0.25 0 1";
	colors[2]	= ".85 .85 .85 1";
	sizes[0]	= 1.5;
	sizes[1]	= 0.5;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(DiseaseTrailEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 1;
   ejectionVelocity = 1;
   ejectionOffset   = 0.1;
   velocityVariance = 1;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 12000;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "DiseaseTrailParticle";
   uiName = "DiseaseTrail";

};

datablock ParticleData(DiseaseExplosionParticle)
{
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0;
	constantAcceleration	= 10.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 180;
	useInvAlpha		= false;
	animateTexture		= false;

	textureName		= "./shapes/spark2";
	//Interpolation variables
	colors[0]	= "0 .5 .25 1";
	colors[1]	= "0 0.25 0 1";
	colors[2]	= ".85 .85 .85 1";

	sizes[0]	= 6;
	sizes[1]	= 5;
	sizes[2]	= 0.00;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(DiseaseExplosionEmitter)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   lifetimeMS       = 7;
   ejectionVelocity = 1;
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "DiseaseExplosionParticle";

   useEmitterColors = true;
   uiName = "DiseaseExplode";
};

datablock ExplosionData(DiseaseExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

soundProfile = "";
   emitter[0] = DiseaseExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.0;
   camShakeRadius = 0.0;

   // Dynamic light
   lightStartRadius = 3;
   lightEndRadius = 1;
   lightStartColor = ".596 .161 .392";
   lightEndColor = ".85 .85 .85";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 500;

   //radius damage
   radiusDamage        = 0.5;
   damageRadius        = 1;
};


datablock ProjectileData(DiseaseProjectile)
{
   projectileShapeName ="base/data/shapes/empty.dts";
   directDamage        = 60;
 //  directDamageType  = $DamageType::MagicDirect;
   impactImpulse	   = 10;
   verticalImpulse	   = 10;
   explosion           = DiseaseExplosion;
   particleEmitter     = DiseaseTrailEmitter;

   brickExplosionRadius = 0.1;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 0.1;
   brickExplosionMaxVolume = 20;
   brickExplosionMaxVolumeFloating = 20;

   muzzleVelocity      = 70;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 850;
   fadeDelay           = 650;
   bounceElasticity    = 0;
   bounceFriction      = 0;
   isBallistic         = true;
   gravityMod = 10;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.5 0.25";

   uiName = "Disease Projectile";
};

datablock ParticleData(LightningTrailParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.15;
	constantAcceleration = 0;
	lifetimeMS = 1000;
	lifetimeVarianceMS = 500;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 10;
	spinRandomMin = -500;
	spinRandomMax = 500;
	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 1;
	sizes[1] = 0;
};
datablock ParticleEmitterData(LightningTrailEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 1.5;
	ejectionOffset = 0.2;
	thetaMin = 115;
	thetaMax = 175;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "LightningTrailParticle";

	uiName = "Lightning - Trail";
};

datablock ParticleData(LightningExplosionParticle)
{
	dragCoefficient = 5;
	gravityCoefficient = 0;
	inheritedVelFactor = 0.15;
	constantAcceleration = 0;
	lifetimeMS = 2500;
	lifetimeVarianceMS = 1000;
	textureName = "Add-Ons/Projectile_Radio_Wave/bolt";
	spinSpeed = 0;
	spinRandomMin = -900;
	spinRandomMax = 900;
	colors[0] = "1 1 0 1";
	colors[1] = "1 1 0 0";
	sizes[0] = 10;
	sizes[1] = 15;
};
datablock ParticleEmitterData(LightningExplosionEmitter)
{
	ejectionPeriodMS = 150;
	periodVarianceMS = 50;
	ejectionVelocity = 2;
	velocityVariance = 1.5;
	ejectionOffset = 0.2;
	thetaMin = 115;
	thetaMax = 175;
	phiReferenceVel = 0;
	phiVariance = 360;
	overrideAdvance = false;
	particles = "LightningTrailParticle";

	uiName = "Lightning - Explosion";
};

datablock ExplosionData(LightningExplosion)
{
	soundProfile = LightningZapSound;

	lifeTimeMS = 250;

	particleEmitter = LightningExplosionEmitter;
	particleDensity = 50;
	particleRadius = 0.5;

	faceViewer = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "30 30 30";
	camShakeAmp = "7 2 7";
	camShakeDuration = 0.6;
	camShakeRadius = 2.5;

	lightStartRadius = 10;
	lightEndRadius = 0;
	lightStartColor = "1 1 0";
	lightEndColor = "1 1 0";

	damageRadius = 3;
	radiusDamage = 25;

	uiName = "Lightning Zap";
};

datablock ProjectileData(LightningProjectile)
{
	projectileShapeName = "base/data/shapes/empty.dts";
	directDamage = 90;
	directDamageType = $DamageType::MagicDirect;

	brickExplosionRadius = 4;
	brickExplosionImpact = true;
	brickExplosionForce = 1;
	brickExplosionMaxVolume = 20;
	brickExplosionMaxVolumeFloating = 35;

	impactImpulse = 400;
	verticalImpulse = 400;
	explosion = LightningExplosion;
	particleEmitter = LightningTrailEmitter;
	sound = LightningLoopSound;

	muzzleVelocity = 150;
	velInheritFactor = 0;

	armingDelay = 0;
	lifetime = 4000;
	fadeDelay = 4000;
	bounceElasticity = 0.5;
	bounceFriction = 0.5;
	isBallistic = false;

	hasLight = true;
	lightRadius = 5;
	lightColor = "1 1 0";

	uiName = "Lightning Bolt";
};

