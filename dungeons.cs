//---Package
//---
package DRPG_Server_Dungeons
{
	function fxDtsBrick::killBrick(%this)
	{
		if(isObject(%this.room))
		{
			if(!%this.room.generator.spawned)
				Parent::killBrick(%this);
			return;
		}
		Parent::killBrick(%this);
	}
};
activatePackage(DRPG_Server_Dungeons);
registerOutputEvent(fxDtsBrick,"setDungeonRift","string 175 255",0);
function fxDtsBrick::setDungeonRift(%brick,%dungeon)
{
	if(isObject($DRPG::Dungeon[%dungeon]))
	{
		if(!isObject($DRPG::Dungeon[%dungeon].firstRoom) || !isObject($DRPG::Dungeon[%dungeon].lastRoom))
			return;
		if(isObject(%brick.rift))
		{
			$DRPG::Dungeon[%dungeon].firstRoom.updateRiftIn(%brick.rift);
			$DRPG::Dungeon[%dungeon].lastRoom.updateRiftOut(%brick.rift);
			%brick.rift.riftLink = $DRPG::Dungeon[%dungeon].firstRoom.riftIn;
		}
		else
		{
			%brick.rift = new Trigger()
			{
				dataBlock = DRPGDungeonRiftTriggerData;
				polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
				entrance = 1;
				dungeonName = $DRPG::Dungeon[%dungeon].name;
			};
			%boxMin = getWords(%brick.getWorldBox(),0,2);
			%boxMax = getWords(%brick.getWorldBox(),3,5);
			%boxDiff = vectorSub(%boxMax,%boxMin);
			%boxDiff = vectorAdd(%boxDiff,"0 0 0.2"); 
			%brick.rift.setScale(%boxDiff);
			%posA = %brick.getWorldBoxCenter();
			%posB = %brick.rift.getWorldBoxCenter();
			%posDiff = vectorSub(%posA,%posB);
			%posDiff = vectorAdd(%posDiff,"0 0 0.1");
			%brick.rift.setTransform(%posDiff);
			%brick.rift.brick = %brick;
			$DRPG::Dungeon[%dungeon].firstRoom.updateRiftIn(%brick.rift);
			$DRPG::Dungeon[%dungeon].lastRoom.updateRiftOut(%brick.rift);
			%brick.rift.riftLink = $DRPG::Dungeon[%dungeon].firstRoom.riftIn;
		}
	}
}
//---Misc.
//---
function serverCmdSaveDungeonRoom(%client,%base,%file)
{
	if(!isObject(%base) || !%client.isAdmin || !%base.isBaseplate())
		return;
	%c = 0;
	%w = new FileObject();
	%w.openForWrite(%file);
	%basePos = getWords(%base.getTransform(),0,2);
	%baseX = getWord(%basePos,0);
	%baseY = getWord(%basePos,1);
	%baseZ = getWord(%basePos,2);
	%c = %w.writeDungeonRoomBrick(%base,%baseX,%baseY,%baseZ);
	%w.close();
	%w.delete();
	if(isFile(%file))
		messageClient(%client,'',%c @ " bricks in the dungeon room were saved successfully.");
}
function FileObject::writeDungeonRoomBrick(%w,%brick,%baseX,%baseY,%baseZ)
{
	if(%w.doneBrick[%brick])
		return 0;
	%c = 0;
	%brickPos = getWords(%brick.getTransform(),0,2);
	%brickX = getWord(%brickPos,0);
	%brickY = getWord(%brickPos,1);
	%brickZ = getWord(%brickPos,2);
	%w.writeLine("B" TAB %brick.getDatablock().getName() TAB %brickX - %baseX TAB %brickY - %baseY TAB %brickZ - %baseZ TAB %brick.getAngleID() TAB %brick.isBaseplate() TAB %brick.getColorID() TAB %brick.getPrintID() TAB %brick.getColorFXID() TAB %brick.getShapeFXID() TAB %brick.isRaycasting() TAB %brick.isColliding() TAB %brick.isRendering());
	for(%z=0;%z<%brick.numEvents;%z++)
		%w.writeLine("E" TAB %brick.eventDelay[%z] TAB %brick.eventEnabled[%z] TAB %brick.eventInput[%z] TAB %brick.eventTarget[%z] TAB %brick.eventOutput[%z] TAB %brick.eventOutputParameter[%z,1] TAB %brick.eventOutputParameter[%z,2] TAB %brick.eventOutputParameter[%z,3] TAB %brick.eventOutputParameter[%z,4]);
	if(isObject(%brick.emitter))
		%w.writeLine("EM" TAB %brick.emitter.emitter.getName() TAB %brick.emitterDirection);
	if(isObject(%brick.light))
		%w.writeLine("LI" TAB %brick.light.getDatablock().getName());
	if(isObject(%brick.audioEmitter))
		%w.writeLine("MU" TAB %brick.audioEmitter.profile.getName());
	if(strLen(%brick.getName()) > 1)
		%w.writeLine("N" TAB getSubStr(%brick.getName(),1,strLen(%brick.getName()) - 1));
	%c++;
	%w.doneBrick[%brick] = 1;
	%uc = %brick.getNumUpBricks();
	for(%u=0;%u<%uc;%u++)
	{
		%c += %w.writeDungeonRoomBrick(%brick.getUpBrick(%u),%baseX,%baseY,%baseZ);
	}
	return %c;
}
//---Rifts
//A fancy name for a portal/teleporter.
//Basically each dungeon room has a rift-in and a rift-out. Obviously the rift-in is linked to another room's rift-out, and vise versa.
//NOTES: Dungeon rooms do not need rifts. You can handle teleporting in a custom way if you'd like.
//---
if(!isObject(DRPGDungeonRiftTriggerData))
{
	datablock TriggerData(DRPGDungeonRiftTriggerData)
	{
		tickPeriodMS = 100;
	};
}
function DRPGDungeonRiftTriggerData::onEnterTrigger(%this,%trigger,%user)
{
	if(%user.rifted || !isObject(%trigger.riftLink) || !isObject(%trigger.riftLink.brick) || !(%user.getType() & $TypeMasks::PlayerObjectType) || isObject(%user.director) || isObject(%user.spawnBrick))
		return;
	if(isObject(%user.client) && !isObject(%user.director) && getWord($DRPG::Accounts.value[%user.client.bl_id,"level"],0) < 10)
		return;
	if(%trigger.entrance && isObject(%user.client))
		%user.client.bottomPrint("\c0WARNING - You are entering a dungeon: \c6" @ %trigger.dungeonName,3);
	else if(%trigger.riftLink.entrance && isObject(%user.client))
	{
		%user.client.bottomPrint("\c0You are leaving a dungeon: \c6" @ %trigger.riftLink.dungeonName,3);
		%user.room = "";
	}
	%user.rifted = 1;
	%user.setTransform(vectorAdd(%trigger.riftLink.brick.getTransform(),"0 0 0.3"));
}
function DRPGDungeonRiftTriggerData::onLeaveTrigger(%this,%trigger,%user)
{
	if(!isObject(%trigger.riftLink) || !(%user.getType() & $TypeMasks::PlayerObjectType) || isObject(%user.director) || isObject(%user.spawnBrick))
		return;
	%user.rifted = 0;
}
//---Rooms (dungeonRoom)
//A dungeon room is a room, lol. It uses a room type from the generator, and rooms are spawned from the room type's file.
//Dungeon room types can have embedded scripts and basic room handlers :D
//---
if(!isObject(DRPGDungeonRoomTriggerData))
{
	datablock TriggerData(DRPGDungeonRoomTriggerData)
	{
		tickPeriodMS = 100;
	};
}
function DRPGDungeonRoomTriggerData::onEnterTrigger(%this,%trigger,%user)
{
	if(!isObject(%trigger.master) || !isObject(%trigger.room) || !isObject(%trigger.room.players) || !(%user.getType() & $TypeMasks::PlayerObjectType))
		return;
	%trigger.master.onEnterRoom(%user);
	if(!%trigger.room.players.isMember(%user) && !isObject(%user.director))
		%trigger.room.players.add(%user);
	%user.room = %trigger.room;
}
function DRPGDungeonRoomTriggerData::onLeaveTrigger(%this,%trigger,%user)
{
	if(!isObject(%trigger.master) || !isObject(%trigger.room) || !isObject(%trigger.room.players) || !(%user.getType() & $TypeMasks::PlayerObjectType))
		return;
	if(%trigger.room.players.isMember(%user) && !isObject(%user.director))
		%trigger.room.players.remove(%user);
	%trigger.master.onLeaveRoom(%user);
}
function DRPGDungeonRoomTriggerData::onTickTrigger(%this,%trigger)
{
	if(!isObject(%trigger.master) || !isObject(%trigger.room) || %trigger.room.players.getCount() < 1)
		return;
	%trigger.master.onRoomTick();
}
function dungeonRoom::onAdd(%this)
{
	if(!isObject(%this.generator))
	{
		%this.delete();
		return;
	}
	if(!isFile(%this.generator.roomType[%this.roomType]))
	{
		%this.delete();
		return;
	}
	if(%this.x $= "" || %this.y $= "")
	{
		%this.delete();
		return;
	}
	%this.master = new ScriptObject()
	{
		class = dungeonMaster;
		generator = %this.generator;
		room = %this;
		paused = 1;
	};
	%this.numMobs = 0;
	%this.players = new SimSet();
	%this.realX = %this.generator.startX + ((%this.generator.roomSize * %this.x) / 2);
	%this.realY = %this.generator.startY + ((%this.generator.roomSize * %this.y) / 2);
	%this.realZ = %this.generator.startZ;
	%this.file = %this.generator.roomType[%this.roomType];
	%r = new FileObject();
	%r.openForRead(%this.file);
	while(!%r.isEOF())
	{
		%line = %r.readLine();
		%cmd = getField(%line,0);
		%args = getFields(%line,1,getFieldCount(%line)-1);
		switch$(%cmd)
		{
			case "B":
				//B	datablock	offsetx	offsety	offsetz	angleid	baseplate	colorid	printid	colorfxid	shapefxid	raycasting	colliding	rendering
				%dataBlock = getField(%args,0);
				%offsetX = getField(%args,1);
				%offsetY = getField(%args,2);
				%offsetZ = getField(%args,3);
				%angleID = getField(%args,4);
				%isBaseplate = getField(%args,5);
				%colorID = getField(%args,6);
				%printID = getField(%args,7);
				%colorFxID = getField(%args,8);
				%shapeFxID = getField(%args,9);
				%isRaycasting = getField(%args,10);
				%isColliding = getField(%args,11);
				%isRendering = getField(%args,12);
				switch(%angleID)
				{
					case 0:
						%rot = "1 0 0 0";
					case 1:
						%rot = "0 0 1 90";
					case 2:
						%rot = "0 0 1 180";
					case 3: 
						%rot = "0 0 -1 90";
				}
				%pos = vectorAdd(%this.realX SPC %this.realY SPC %this.realZ,%offsetX SPC %offsetY SPC %offsetZ);
				%brick = new fxDTSBrick()
				{
					dataBlock = %dataBlock;
					position  = %pos;
					rotation  = %rot;
					isBaseplate = %isBaseplate;
					isPlanted = 1;
					numEvents = 0;
				};
				%brick.setTrusted(1);
				%brick.plant();
				%brick.setColor(%colorID);
				%brick.setPrint(%printID);
				%brick.setColorFX(%colorFxID);
				%brick.setShapeFX(%shapeFxID);
				%brick.setRaycasting(%isRaycasting);
				%brick.setColliding(%isColliding);
				%brick.setRendering(%isRendering);
				%brick.room = %this;
				%this.generator.bricks.add(%brick);
				%lastCmd = "B";
			case "N":
				//N	brickname
				if(isObject(%brick)) 
				{
					%brick.setNTObjectName(%args);
				}
				%lastCmd = "N";
			case "M":
				if(!isObject(%this.generator.mobDirector))
				{
					%this.generator.mobDirector = new ScriptObject()
					{
						class = mobDirector;
					};
					%this.generator.mobDirector.addMobType("CREEPER","Add-Ons/Server_DRPG/mobs/creeper.mob");
					%this.generator.mobDirector.addMobType("SKELETON_WARRIOR","Add-Ons/Server_DRPG/mobs/skeleton_warrior.mob");
					%this.generator.mobDirector.addMobType("SKELETON_ARCHER","Add-Ons/Server_DRPG/mobs/skeleton_archer.mob");
					%this.generator.mobDirector.addMobType("PIRATE","Add-Ons/Server_DRPG/mobs/pirate.mob");
					%this.generator.mobDirector.addMobType("PIRATE_KING","Add-Ons/Server_DRPG/mobs/pirate_king.mob");
				}
				%type = getField(%args,0);
				%brick = nameToID(getField(%args,1));
				if(!isObject(%brick))
					continue;
				%brick.setNTObjectName("");
				%mob = %this.generator.mobDirector.spawnMob(vectorAdd(%brick.getTransform(),"0 0 0.3"),%type,%this,0,0);
				%mob.dungeonSpawnBrick = %brick.getID();
				%lastCmd = "M";
			case "E":
				//E	0	1	onActivate	Client	ChatMessage	ok
				if(isObject(%brick))
				{
					%delay = getField(%args,0);
					%enabled = getField(%args,1);
					%inputName = getField(%args,2);
					%targetName = getField(%args,3);
					%outputName = getField(%args,4);
					%param1 = getField(%args,5);
					%param2 = getField(%args,6);
					%param3 = getField(%args,7);
					%param4 = getField(%args,8);
					if(%inputIdx[%inputName] $= "")
						%inputIdx[%inputName] = inputEvent_GetInputEventIdx(%inputName);
					%inputIdx = %inputIdx[%inputName];
					if(%targetIdx[%inputName,%targetName] $= "")
						%targetIdx[%inputName,%targetName] = inputEvent_GetTargetIndex(fxDtsBrick,%inputIdx,%targetName);
					%targetIdx = %targetIdx[%inputName,%targetName];
					if(%targetClass[%inputName,%targetName] $= "")
						%targetClass[%inputName,%targetName] = inputEvent_GetTargetClass(fxDtsBrick,%inputIdx,%targetIdx);
					%targetClass = %targetClass[%inputName,%targetName];
					if(%outputIdx[%targetClass,%outputName] $= "")
						%outputIdx[%targetClass,%outputName] = outputEvent_GetOutputEventIdx(%targetClass,%outputName);
					%outputIdx = %outputIdx[%targetClass,%outputName];
					if(%inputIdx < 0 || %outputIdx < 0)
						continue;
					%brick.eventDelay[%brick.numEvents] = %delay;
					%brick.eventEnabled[%brick.numEvents] = %enabled;
					%brick.eventInput[%brick.numEvents] = %inputName;
					%brick.eventInputIdx[%brick.numEvents] = %inputIdx;
					if(%targetIdx > -1)
					{
						%brick.eventTarget[%brick.numEvents] = %targetName;
						%brick.eventTargetIdx[%brick.numEvents] = %targetIdx;
					}
					else
					{
						%brick.eventTarget[%brick.numEvents] = -1;
						%brick.eventTargetIdx[%brick.numEvents] = -1;
						%brick.eventNT[%brick.numEvents] = getWord(%targetName,1);
					}
					%brick.eventOutput[%brick.numEvents] = %outputName;
					%brick.eventOutputIdx[%brick.numEvents] = %outputIdx;
					%brick.eventOutputParameter[%brick.numEvents,1] = %param1;
					%brick.eventOutputParameter[%brick.numEvents,2] = %param2;
					%brick.eventOutputParameter[%brick.numEvents,3] = %param3;
					%brick.eventOutputParameter[%brick.numEvents,4] = %param4;
					%brick.eventOutputAppendClient[%brick.numEvents] = $OutputEvent_AppendClient[%targetClass,%inputIdx];
					%brick.implicitCancelEvents = 0;
					%brick.numEvents++;
				}
				%lastCmd = "E";
			case "EM":
				if(isObject(%brick))
				{
					%brick.setEmitter(nameToID(getWord(%args,0)));
					%brick.setEmitterDirection(getWord(%args,1));
				}
			case "LI":
				if(isObject(%brick))
				{
					%brick.setLight(nameToID(%args));
				}
			case "MU":
				if(isObject(%brick))
				{
					%brick.setMusic(nameToID(%args));
				}
			case "S":
				%script = %args;
				%lastCmd = "S";
			case "RI":
				%riftIn = "_" @ %args;
				%lastCmd = "RI";
			case "RO":
				%riftOut = "_" @ %args;
				%lastCmd = "RO";
			case "H":
				%this.master.registerHandler(getField(%args,0),getField(%args,1));
				%lastCmd = "H";
			default:
				if(%line !$= "")
				{
					if(%lastCmd $= "S")
						%script = %script @ %line;
				}
		}
	}
	if(%script !$= "")
		eval(%script);
	%this.trigger = new Trigger()
	{
		dataBlock = DRPGDungeonRoomTriggerData;
		polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
		master = %this.master;
		room = %this;
		generator = %this.generator;
	};
	missionCleanup.add(%this.trigger);
	%this.trigger.setTransform(%this.realX - (%this.generator.roomSize / 4) SPC %this.realY + (%this.generator.roomSize / 4) SPC %this.realZ);
	%this.trigger.setScale(%this.generator.roomSize / 2 SPC %this.generator.roomSize / 2 SPC %this.generator.roomSize / 2);
	%riftIn = nameToID(%riftIn);
	%riftOut = nameToID(%riftOut);
	if(isObject(%riftIn) && isObject(%riftOut))
	{
		%this.riftIn = new Trigger()
		{
			dataBlock = DRPGDungeonRiftTriggerData;
			polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
			master = %this.master;
			room = %this;
			generator = %this.generator;
		};
		%boxMin = getWords(%riftIn.getWorldBox(),0,2);
		%boxMax = getWords(%riftIn.getWorldBox(),3,5);
		%boxDiff = vectorSub(%boxMax,%boxMin);
		%boxDiff = vectorAdd(%boxDiff,"0 0 0.2"); 
		%this.riftIn.setScale(%boxDiff);
		%posA = %riftIn.getWorldBoxCenter();
		%posB = %this.riftIn.getWorldBoxCenter();
		%posDiff = vectorSub(%posA,%posB);
		%posDiff = vectorAdd(%posDiff,"0 0 0.1");
		%this.riftIn.setTransform(%posDiff);
		%riftIn.setNTObjectName("");
		%this.riftIn.brick = %riftIn;
		%this.riftOut = new Trigger()
		{
			dataBlock = DRPGDungeonRiftTriggerData;
			polyhedron = "0 0 0 1 0 0 0 -1 0 0 0 1";
			master = %this.master;
			room = %this;
			generator = %this.generator;
		};
		%boxMin = getWords(%riftOut.getWorldBox(),0,2);
		%boxMax = getWords(%riftOut.getWorldBox(),3,5);
		%boxDiff = vectorSub(%boxMax,%boxMin);
		%boxDiff = vectorAdd(%boxDiff,"0 0 0.2"); 
		%this.riftOut.setScale(%boxDiff);
		%posA = %riftOut.getWorldBoxCenter();
		%posB = %this.riftOut.getWorldBoxCenter();
		%posDiff = vectorSub(%posA,%posB);
		%posDiff = vectorAdd(%posDiff,"0 0 0.1");
		%this.riftOut.setTransform(%posDiff);
		%riftOut.setNTObjectName("");
		%this.riftOut.brick = %riftOut;
	}
	%r.close();
	%r.delete();
}
function dungeonRoom::updateRiftIn(%this,%in)
{
	if(isObject(%this.riftIn))
		%this.riftIn.riftLink = %in;
}
function dungeonRoom::updateRiftOut(%this,%out)
{
	if(isObject(%this.riftOut))
		%this.riftOut.riftLink = %out;
}
function dungeonRoom::onRemove(%this)
{
	if(isObject(%this.trigger))
		%this.trigger.delete();
	if(isObject(%this.players))
		%this.players.delete();
	if(isObject(%this.mobs))
		%this.mobs.delete();
	if(isObject(%this.master))
		%this.master.delete();
	if(isObject(%this.riftIn))
		%this.riftIn.delete();
	if(isObject(%this.riftOut))
		%this.riftOut.delete();
}
//---Master (dungeonMaster)
//Each room has a dungeon master. The master controls events of the room:
//onSpawn, onEnterRoom, onRoomTick, onLeaveRoom, onDestroy
//And calls all of it's registered handlers.
//NOTES: More events could be added in the future, probably things like onPlayerDeath, onMobDeath etc.
//	 Originally there was only one master for the entire dungeon. I'm not sure if that or this way is better. It could be changed in the future.
//---
function dungeonMaster::onSpawn(%this)
{
	for(%i=0;%i<%this.numHandlers[onSpawn];%i++)
	{
		%handler = %this.handler[onSpawn,%i];
		if(isFunction(%handler))
			call(%handler,%this.room);
	}
}
function dungeonMaster::onEnterRoom(%this,%player)
{
	for(%i=0;%i<%this.numHandlers[onEnterRoom];%i++)
	{
		%handler = %this.handler[onEnterRoom,%i];
		if(isFunction(%handler))
			call(%handler,%this.room,%player);
	}
}
function dungeonMaster::onRoomTick(%this)
{
	cancel(%this.mobPause);
	%this.playAllMobs();
	for(%i=0;%i<%this.numHandlers[onRoomTick];%i++)
	{
		%handler = %this.handler[onRoomTick,%i];
		if(isFunction(%handler))
			call(%handler,%this.room);
	}
	if(getWordCount(%this.room.mobs) > 0)
		%this.mobPause = %this.schedule(200,"pauseAllMobs");
}
function dungeonMaster::pauseAllMobs(%this)
{
	if(%this.paused)
		return;
	%this.paused = 1;
	%c = getWordCount(%this.room.mobs);
	for(%i=0;%i<%c;%i++)
	{
		%m = getWord(%this.room.mobs,%i);
		%this.generator.mobDirector.pauseMob(%m);
	}
}
function dungeonMaster::playAllMobs(%this)
{
	if(!%this.paused)
		return;
	%this.paused = 0;
	%c = getWordCount(%this.room.mobs);
	for(%i=0;%i<%c;%i++)
	{
		%m = getWord(%this.room.mobs,%i);
		%this.generator.mobDirector.playMob(%m);
	}
}
function dungeonMaster::onLeaveRoom(%this,%player)
{
	for(%i=0;%i<%this.numHandlers[onLeaveRoom];%i++)
	{
		%handler = %this.handler[onLeaveRoom,%i];
		if(isFunction(%handler))
			call(%handler,%this.room,%player);
	}
}
function dungeonMaster::onDestroy(%this)
{
	for(%i=0;%i<%this.numHandlers[onDestroy];%i++)
	{
		%handler = %this.handler[onDestroy,%i];
		if(isFunction(%handler))
			call(%handler,%this.room);
	}
}
function dungeonMaster::registerHandler(%this,%type,%func)
{
	if(%this.numHandlers[%type] $= "")
		%this.numHandlers[%type] = 0;
	%this.handler[%type,%this.numHandlers[%type]] = %func;
	%this.numHandlers[%type]++;
	return 1;
}
//---Generator (dungeonGenerator)
//The dungeon generator is well, the generator. It generates a dungeon at a position with a certain amount of rooms and room sizes.
//It's pretty smoothly run, so you can destroy dungeons and recreate on the go basically.
//NOTES: I have been contemplating whether having the generator handle the core methods is the best way to do it.
//	 I may make a dungeon director in the future.
//---
function dungeonGenerator::onAdd(%this)
{
	%this.numRoomTypes = 0;
	%this.bricks = new SimGroup(BrickGroup_666)
	{
		name = "Dungeon";
		bl_id = 666;
	};
	$DRPG::Dungeon[%this.name] = %this;
}
function dungeonGenerator::onRemove(%this)
{
	$DRPG::Dungeon[%this.name] = "";
	if(%this.spawned)
	{
		%this.destroyDungeon();
		%this.bricks.delete();
		if(isObject(%this.mobDirector))
			%this.mobDirector.delete();
	}
}
function dungeonGenerator::spawnDungeon(%this,%position,%roomSize,%roomCount)
{
	if(%this.spawned)
		%this.destroyDungeon();
	%this.startX = getWord(%position,0);
	%this.startY = getWord(%position,1);
	%this.startZ = getWord(%position,2);
	%this.roomSize = %roomSize;
	%this.numRooms = 0;
	%this.lengthX = mFloor(mSqrt(%roomCount));
	%this.lengthY = %this.lengthX;
	for(%x=0;%x<%this.lengthX;%x++)
	{
		for(%y=0;%y<%this.lengthY;%y++)
		{
			%room = %this.addRoom(%x,%y,getRandom(0,%this.numRoomTypes-1));
			%this.roomAt[%x,%y] = %room;
			if(isObject(%this.lastAddedRoom.riftOut) && isObject(%room.riftIn))
			{
				%this.lastAddedRoom.updateRiftOut(%room.riftIn);
				%room.updateRiftIn(%this.lastAddedRoom.riftOut);
			}
			if(%x == 0 && %y == 0)
				%this.firstRoom = %room;
			else if(%x == %this.lengthX && %y == %this.lengthY)
				%this.lastRoom = %room;
			%this.lastAddedRoom = %room;
			%room.master.onSpawn();
		}
	}
	%this.spawned = 1;
}
function dungeonGenerator::destroyDungeon(%this)
{
	%this.spawned = 0;
	%c = %this.bricks.getCount();
	%this.bricks.deleteAll();
	for(%i=0;%i<%this.numRooms;%i++)
	{
		%this.room[%i].master.onDestroy();
		%this.room[%i].delete();
	}
	%this.numRooms = 0;
}
function dungeonGenerator::addRoom(%this,%x,%y,%type)
{
	%this.lastRoom = isObject(%this.room[%this.numRooms-1]) == 1 ? %this.room[%this.numRooms-1] : -1;
	%this.room[%this.numRooms] = new ScriptObject()
	{
		class = dungeonRoom;
		generator = %this;
		roomType = %type;
		x = %x;
		y = %y;
		index = %this.numRooms;
	};
	%this.numRooms++;
	return %this.room[%this.numRooms-1];
}
function dungeonGenerator::removeRoom(%this,%index)
{
	if(%this.room[%index] !$= "")
	{
		%this.room[%index].delete();
		%this.room[%index] = "";
		for(%i=%index+1;%i<%this.numRooms;%i++)
			%this.room[%i-1] = %this.room[%i];
		return 1;
	}
	return 0;
}
function dungeonGenerator::addRoomType(%this,%file)
{
	%this.roomType[%this.numRoomTypes] = %file;
	%this.numRoomTypes++;
	return 1;
}
function dungeonGenerator::removeRoomType(%this,%index)
{
	if(%this.roomType[%index] !$= "")
	{
		%this.roomType[%index] = "";
		for(%i=%index+1;%i<%this.numRoomTypes;%i++)
			%this.roomType[%i-1] = %this.roomType[%i];
		return 1;
	}
	return 0;
}
exec("./dungeons_setup.cs");