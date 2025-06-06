function wandProjectile::onCollision(%this,%obj,%col,%fade,%pos,%normal)
{
	return;
}
package DRPG_Server_Main
{
	function GameConnection::onDeath(%this,%so,%sc,%type,%area)
	{
		%sn = $DRPG::Accounts.value[%sc.bl_id,"nation"];
		%tn = $DRPG::Accounts.value[%this.bl_id,"nation"];
		%color = "\c7";
		if(%tn !$= "")
			%color = $DRPG::Nations::ColorText[%tn];
		%director = %so.director;
		if(!isObject(%director))
			%director = %so.sourceObject.director;
		if(isObject(%sc) && %sc != %this && !isObject(%director))
		{
			%scColor = "\c7";
			if(%sn !$= "")
				%scColor = $DRPG::Nations::ColorText[%sn];
			%msg = getTaggedString($DeathMessage_Murder[%type]);
			%one = strPos(%msg,"%2");
			%two = strPos(%msg,"%1");
			if(%one != -1 && %two != -1)
			{
				%ciBitmap = getSubStr(%msg,%one+2,(%two-%one)-2);
				$DRPG::Minigame.messageAll('',%scColor @ %sc.getPlayerName() SPC %ciBitmap SPC %color @ %this.getPlayerName());
			}
		}
		else if(isObject(%director) || %so.mobExplosion)
		{
			if(%so.mobExplosion)
				%name = %so.mobExplosionName;
			else
				%name = %director.mobType[%so.sourceObject.mobType,"NAME"];
			%scColor = "\c7";
			%msg = getTaggedString($DeathMessage_Murder[%type]);
			%one = strPos(%msg,"%2");
			%two = strPos(%msg,"%1");
			if(%one != -1 && %two != -1)
			{
				%ciBitmap = getSubStr(%msg,%one+2,(%two-%one)-2);
				$DRPG::Minigame.messageAll('',%scColor @ %name SPC %ciBitmap SPC %color @ %this.getPlayerName());
			}
		}
		else
		{
			%msg = getTaggedString($DeathMessage_Suicide[%type]);
			%one = strPos(%msg,"%1");
			if(%one != -1)
			{
				%ciBitmap = getSubStr(%msg,0,%one);
				$DRPG::Minigame.messageAll('',%ciBitmap SPC %color @ %this.getPlayerName());
			}
		}
		%type = 0;
		%this.player.lastDirectDamageType = 0;
		if(isObject(%this.player) && %this.player.inBattlefield && %sc.player.inBattlefield && !isObject(%sc.player.director) && !isObject(%this.player.director))
		{
			%inventory = strReplace($DRPG::Accounts.value[%this.BL_ID,"inventory"],"|","\t");
			for(%i=0;%i<30;%i++)
			{
				if(getRandom(0,4) == 4) //Only drop a small portion of their items
					continue;
				%slot = getField(%inventory,%i);
				if(%slot $= "")
					continue;
				%item = getWord(%slot,0);
				%amount = getWord(%slot,1);
				if(%amount < 1 || $DRPG::Items::Name[%item] $= "")
					continue;
				%staticitem = new Item()
				{
					dataBlock = chestItem;
				};
				%staticitem.setShapeName($DRPG::Items::Name[%item] @ " x" @ %amount);
				%staticitem.gives = %item;
				%staticitem.amount = %amount;
				%staticitem.refuse = %this.player;
				%staticitem.setCollisionTimeout(%this.player);
				%staticitem.setTransform(vectorAdd(posFromTransform(%this.player.getTransform()),getRandom(-1,1) SPC getRandom(-1,1) SPC 0.3));
			}
			%equips = strreplace($DRPG::Accounts.value[%this.bl_id,"equipment"],"|","\t");
			for(%i=0;%i<11;%i++)
			{
				if(getRandom(0,4) == 4) //Only drop a small portion of their items
					continue;
				%slot = getField(%equips,%i);
				if(%slot $= "")
					continue;
				%item = getWord(%slot,0);
				if($DRPG::Items::Name[%item] $= "")
					continue;
				%staticitem = new Item()
				{
					dataBlock = chestItem;
				};
				%staticitem.setShapeName($DRPG::Items::Name[%item] @ " x1");
				%staticitem.gives = %item;
				%staticitem.amount = 1;
				%staticitem.refuse = %this.player;
				%staticitem.setCollisionTimeout(%this.player);
				%staticitem.setTransform(vectorAdd(posFromTransform(%this.player.getTransform()),getRandom(-1,1) SPC getRandom(-1,1) SPC 0.3));
			}
			%this.removePlastic($DRPG::Accounts.value[%this.bl_id,"plastic"]);
		}
		%rt = Parent::onDeath(%this,%so,%sc,%type,%area);
		if(%sn !$= "" && %tn !$= "" && %sn !$= %tn && %sc != %this)
		{
			if(hasItemOnList($DRPG::Nations::War[%sn],%tn))
			{
				%profit = mFloor(($DRPG::Nations::Wealth[%tn] * 0.001) / 2);
				if(%profit > 0)
				{
					$DRPG::Nations::Wealth[%sn] += %profit;
					%scProfit = mFloor(%profit / 4);
					if(%scProfit > 0)
						%sc.addPlastic(%scProfit);
				}
			}
		}
		return %rt;
	}
	function fxDtsBrick::onDeath(%brick)
	{
		if(isObject(%brick.drpgZone))
			%brick.drpgZone.delete();
		if(isObject(%brick.rift))
			%brick.rift.delete();
		Parent::onDeath(%brick);
	}
	function MinigameSO::pickSpawnPoint(%this,%client)
	{
		%nation = $DRPG::Accounts.value[%client.BL_ID,"nation"];
		if(%nation $= "Derma" && DermaSpawns.getCount() == 0)
			Parent::pickspawnpoint(%this,%client);
		else if(%nation $= "Derma")
			return DermaSpawns.getObject(getRandom(0,DermaSpawns.getCount()-1)).getSpawnTransform();
		if(%nation $= "Oloni" && OloniSpawns.getCount() == 0)
			return Parent::pickspawnpoint(%this,%client);
		else if(%nation $= "Oloni")
			return OloniSpawns.getObject(getRandom(0,OloniSpawns.getCount()-1)).getSpawnTransform();
		if(%nation $= "Elrad" && ElradSpawns.getCount() == 0)
			return Parent::pickspawnpoint(%this,%client);
		else if(%nation $= "Elrad")
			return ElradSpawns.getObject(getRandom(0,ElradSpawns.getCount()-1)).getSpawnTransform();
		if(%nation $= "Vanote" && VanoteSpawns.getCount() == 0)
			return Parent::pickspawnpoint(%this,%client);
		else if(%nation $= "Vanote")
			return VanoteSpawns.getObject(getRandom(0,VanoteSpawns.getCount()-1)).getSpawnTransform();
		else
			return Parent::pickspawnpoint(%this,%client);
	}
		function GameConnection::onConnectRequest(%this,%a,%b,%c,%d,%e,%f,%g,%h,%i)
	{
		echo("\n" @ %c SPC "trying to connect.");
		if(%f !$= "")
		{
			%this.hasRTB = 1;
			%this.rtbVersion = %f;
		}
		if(getField(%h,1) $= "DRPG")
		{
			echo(%c SPC "trying to connect with DRPG v" @ getField(%h,2) @ ".");
			if(trim(getField(%h,2)) >= trim($DRPG::ClientVersion))
			{
				%this.hasDRPG = 1;
				%this.drpgVersion = getField(%h,2);
				Parent::onConnectRequest(%this,%a,%b,%c,%d,%e,%f,%g,%h,%i);
			}
			else
			{
				echo(%C SPC "needs to download a new version of DRPG.");
				%this.schedule(0,"delete", "You need the latest <a:https://www.dropbox.com/scl/fi/x4pzrsiqoe8qghtx0f3bm/Client_DRPG.zip?rlkey=x3rk5tlopxyspl7lug71ddqu9&st=cnvjc5pz&dl=1>DRPG Client</a> (v" @ $DRPG::ClientVersion @ " ) to play on this server.");
				return;
			}
		}
		else
		{
			echo(%c SPC "trying to connect without DRPG.");
			%this.schedule(0,"delete", "You need the latest <a:https://www.dropbox.com/scl/fi/x4pzrsiqoe8qghtx0f3bm/Client_DRPG.zip?rlkey=x3rk5tlopxyspl7lug71ddqu9&st=cnvjc5pz&dl=1>DRPG>DRPG Client</a> (v" @ $DRPG::ClientVersion @ " ) to play on this server.");
		}
	}
	function GameConnection::autoAdminCheck(%client)
	{
		$DRPG::Accounts.addKey(%client.BL_ID);
		//$VARSAVE::Accounts[%client.BL_ID,"Quests"] = "";
		%client.updateInventory();
		%client.updateCrafting();
		%client.updateStats();
		%client.updateEquipment();
		if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= "")
		{
			commandtoclient(%client,'updateTax',25);
		}
		else
		{
			commandtoclient(%client,'updateTax',$DRPG::Nations::Tax[$DRPG::Accounts.value[%client.BL_ID,"nation"]] * 100);
		}
		%client.setScore(getWord($DRPG::Accounts.value[%client.bl_id,"level"],0));
		return Parent::autoAdminCheck(%client);
	}
	function serverCmdCreateMinigame(%cl,%a,%b,%c,%d,%e,%f,%g)
	{
		return;
	}
	function serverCmdLeaveMinigame(%cl)
	{
		return;
	}
	function GameConnection::onClientEnterGame(%client)
	{
		Parent::onClientEnterGame(%client);
		$DRPG::Minigame.addMember(%client);
	}
	function serverCmdTeamMessageSent(%client,%message)
	{
		if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= "")
			return;
		%client.clanPrefix = "\c7[\c4" @ getWord($DRPG::Accounts.value[%client.BL_ID,"level"],0) @ "\c7]";
		%client.clanSuffix = "";
		if($DRPG::Accounts.value[%client.BL_ID,"nation"] !$= "")
			%client.clanSuffix = "\c7[" @ $DRPG::Nations::ColorText[$DRPG::Accounts.value[%client.bl_id,"nation"]] @ $DRPG::Accounts.value[%client.bl_id,"nation"] @ "\c7]";
		for(%i=0;%i<ClientGroup.getCount();%i++)
		{
			%cl = ClientGroup.getObject(%i);
			if($DRPG::Accounts.value[%client.BL_ID,"nation"] $= $DRPG::Accounts.value[%cl.BL_ID,"nation"])
			{
				messageClient(%cl,'',%client.clanPrefix @ "\c3" @ %client.getPlayerName() @ %client.clanSuffix @ "\c5: " @ %message);
			}
		}
	}
	function serverCmdMessageSent(%client,%message)
	{
		%client.clanPrefix = "\c7[\c4" @ getWord($DRPG::Accounts.value[%client.BL_ID,"level"],0) @ "\c7]";
		%client.clanSuffix = "";
		if($DRPG::Accounts.value[%client.BL_ID,"nation"] !$= "")
		{
			%l = $DRPG::Nations::Leader[$DRPG::Accounts.value[%client.BL_ID,"nation"]] == %client.bl_id ? " L" : "";
			%client.clanSuffix = "\c7[" @ $DRPG::Nations::ColorText[$DRPG::Accounts.value[%client.bl_id,"nation"]] @ $DRPG::Accounts.value[%client.bl_id,"nation"] @ %l @ "\c7]";
		}
		Parent::serverCmdMessageSent(%client,%message);
	}
	function ServerLoadSaveFile_Start(%a,%b,%c)
	{
		$DRPG::isLoading = 1; 
		Parent::ServerLoadSaveFile_Start(%a,%b,%c);
	}
	function ServerLoadSaveFile_End(%a,%b,%c)
	{
		$DRPG::isLoading = 0;
		Parent::ServerLoadSaveFile_End(%a,%b,%c);
	}
	function fxDtsBrickData::onTrustCheckFinished(%db,%brick)
	{
		Parent::onTrustCheckFinished(%db,%brick);
		%id = %brick.getGroup().bl_id;
		%client = %brick.getGroup().client;
		if($DRPG::isLoading)
		{
			if(%brick.dataBlock.DermaSpawn)
			{
				DermaSpawns.add(%brick);
				return;
			}
			else if(%brick.dataBlock.OloniSpawn)
			{
				OloniSpawns.add(%brick);
				return;
			}
			else if(%brick.dataBlock.ElradSpawn)
			{
				ElradSpawns.add(%brick);
				return;
			}
			else if(%brick.dataBlock.VanoteSpawn)
			{
				VanoteSpawns.add(%brick);
				return;
			}
			else if(%brick.dataBlock.battleField || %brick.dataBlock.safeField)
			{
				return;
			}
			return;
		}
		if($DRPG::Nations::Leader["Derma"] == %id && %brick.dataBlock.DermaSpawn)
		{
			DermaSpawns.add(%brick);
			%DermaSpawn = 1;
		}
		else if(%brick.dataBlock.DermaSpawn)
		{
			%brick.schedule(0,delete);
			return;
		}
		else if($DRPG::Nations::Leader["Oloni"] == %id && %brick.dataBlock.OloniSpawn)
		{
			OloniSpawns.add(%brick);
			%OloniSpawn = 1;
		}
		else if(%brick.dataBlock.OloniSpawn)
		{
			%brick.schedule(0,delete);
			return;
		}
		else if($DRPG::Nations::Leader["Elrad"] == %id && %brick.dataBlock.ElradSpawn)
		{
			ElradSpawns.add(%brick);
			%ElradSpawn = 1;
		}
		else if(%brick.dataBlock.ElradSpawn)
		{
			%brick.schedule(0,delete);
			return;
		}
		else if($DRPG::Nations::Leader["Vanote"] == %id && %brick.dataBlock.VanoteSpawn)
		{
			VanoteSpawns.add(%brick);
			%VanoteSpawn = 1;
		}
		else if(%brick.dataBlock.VanoteSpawn)
		{
			%brick.schedule(0,delete);
			return;
		}
		%level = %client.getSkillLevel("building");
		%cost = mCeil(%brick.dataBlock.getVolume() / (3 * %level));
		if(!%brick.getGroup().client.isSuperAdmin && (%brick.dataBlock.safeField || %brick.dataBlock.battleField))
		{
			%brick.schedule(0,delete);
			return;
		}
		if(!%brick.getGroup().client.isSuperAdmin && (%brick.dataBlock.DRPG_isOre || %brick.dataBlock.DRPG_isWood))
		{
			%brick.schedule(0,delete);
			return;
		}
		if(%brick.getGroup().client.isSuperAdmin && (%brick.dataBlock.safeField || %brick.dataBlock.battleField))
		{
			%brick.setRendering(0);
			%brick.setRaycasting(0);
			%brick.setColliding(0);
			%brick.schedule(100,"setupDRPGZone");
			return;
		}
		if(%brick.getGroup().client.isSuperAdmin && (%brick.dataBlock.DRPG_isOre || %brick.dataBlock.DRPG_isWood))
			return;
		if($DRPG::Nations::Leader[$DRPG::Accounts.value[%client.BL_ID,"nation"]] == %client.BL_ID)
		{
			if($DRPG::Nations::Wealth[$DRPG::Accounts.value[%client.BL_ID,"nation"]] >= %cost)
			{
				$DRPG::Nations::Wealth[$DRPG::Accounts.value[%client.BL_ID,"nation"]] -= %cost;
				bottomPrint(%client,"-\c6" @ %cost @ " Plastic from " @ $DRPG::Accounts.value[%client.BL_ID,"nation"] @ "'s wealth pool.",3);
				%exp = (%cost / 90);
				if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 1)
					%exp *= 1.25;
				else if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 2)
					%exp *= 1.40;
				else if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 3)
					%exp *= 2;

				%client.addExp("building",%exp);
				return;
			}
		}
		if(!%client.hasPlastic(%cost))
		{
			centerPrint(%client,"You need \c6" @ %cost @ "\c0 Plastic to place that brick.",3);
			%brick.schedule(0,"delete");
			if(%DermaSpawn)
				DermaSpawns.remove(%brick);
			else if(%OloniSpawn)
				OloniSpawns.remove(%brick);
			else if(%ElradSpawn)
				ElradSpawns.remove(%brick);
			else if(%VanoteSpawn)
				VanoteSpawns.remove(%brick);
			return;
		}
		%client.removePlastic(%cost);
		%exp = (%cost / 15);
		if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 1)
			%exp *= 1.25;
		else if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 2)
			%exp *= 1.40;
		else if(getPerkLevel($DRPG::Accounts.value[%id,nation],BuildingEXP) == 3)
			%exp *= 2;

		%client.addExp("building",%exp*$DRPG::Prefs::ExpMultiplier["building"]);
	}
	function GameConnection::createplayer(%this,%trans)
	{
		Parent::createPlayer(%this,%trans);
		%this.schedule(100,doperkchecks);
	}
	function Armor::onImpact(%data,%obj,%col,%vec,%vel)
	{
		if(%lv = getPerkLevel($DRPG::Accounts.value[%obj.client.BL_ID,nation],FallingSafety) == 0)
			return Parent::onImpact(%data,%obj,%col,%vec,%vel);
		if(%lv == 1)
		{
			%min = 32;
			%scale = 2.9;
		}
		else if(%lv == 2)
		{
			%min = 36;
			%scale = 2.4;
		}
		else if(%lv == 3)
		{
			%min = 45;
			%scale = 1.5;
		}
		if(%vel < %min)
			return %min;
		else if(vectorLen(getWords(%vec,0,1) SPC 0) > getword(%vec,2))
			%obj.damage(%obj,%obj,%obj.position,%vel*%scale,$DamageType::Impact);
		else
			%obj.damage(%obj,%obj,%obj.position,%vel*%scale,$DamageType::Fall);
		return %min;
	}
	function Armor::damage(%data,%obj,%source,%pos,%damage,%type)
	{
		if(%obj.client != %source.client && !isObject(%obj.spawnBrick) && isObject(%source.client) && isObject(%obj.client) && !isObject(%obj.director) && !isObject(%source.sourceObject.director))
		{
			if(%attacker.client.atkpotion)
			{
				%damage *= 1.5;
			}
			else if(%player.client.defpotion)
			{
				%damage /= 0.5;
			}
		
			%objl = getWord($DRPG::Accounts.value[%obj.client.bl_id,"level"],0);
			%sourcel = getWord($DRPG::Accounts.value[%source.client.bl_id,"level"],0);
			%objn = $DRPG::Accounts.value[%obj.client.bl_id,"nation"];
			%sourcen = $DRPG::Accounts.value[%source.client.bl_id,"nation"];
			if(%sourcen $= %objn && %objn !$= "")
			{
				%source.client.centerPrint("You can not kill other players who are in your nation.",2);
				return Parent::damage(%data,%obj,%source,%pos,0,%type);
			}
			if(%obj.inSafeField)
			{
				%source.client.centerPrint("You can not kill other players inside this town.",2);
				return Parent::damage(%data,%obj,%source,%pos,0,%type);
			}
			if(%type == $DamageType::Melee)
			{
				%attack = %source.client.getSkillLevel("melee");
				%mult = 1 + mClampF((%attack - 20) / 100,-0.2,0.8);
				%damage *= %mult;
			}
			if(%lv = getPerkLevel($DRPG::Accounts.value[%source.client.BL_ID],Damage) == 1)
				%damage *= 1.1;
			else if(%lv == 2)
				%damage *= 1.18;
			else if(%lv == 3)
				%damage *= 1.3;

			if(%type == $DamageType::Melee)
			{
				%exp = %damage / 25;
				if(%exp > 0)
				{
					%source.client.addExp("melee",%exp*$DRPG::Prefs::ExpMultiplier["melee"]);
				}
			}
		}
		else if(isObject(%source.sourceObject.director) && (!isObject(%obj.director) && isObject(%obj.client)) && !isObject(%source.sourceObject.spawnBrick))
		{
			%mob = %source.sourceObject;
			%sourcel = %mob.director.mobType[%mob.mobType,"MELEE"];
			if(%type == $DamageType::Melee)
			{
				%mult = 1 + mClampF((%sourcel - 20) / 100,-0.2,0.8);
				%damage *= %mult;
			}
		}
		else if(isObject(%obj.director) && (!isObject(%source.sourceObject.director) && isObject(%source.client)) && !isObject(%obj.spawnBrick))
		{
			%attack = %source.client.getSkillLevel("melee");
			if(%type == $DamageType::Melee)
			{
				%mult = 1 + mClampF((%attack - 20) / 100,-0.2,0.8);
				%damage *= %mult;
				%exp = %damage / 25;
				if(%exp > 0)
				{
					%source.client.addExp("melee",%exp*$DRPG::Prefs::ExpMultiplier["melee"]);
				}
			}
		}
		%k = Parent::damage(%data,%obj,%source,%pos,%damage,%type);
		if(%obj.getState() $= "Dead" && isObject(%obj.director) && isObject(%source.client))
		{
			%drops = %obj.director.mobType[%obj.mobType,"DROPS"];
			if(%drops !$= "")
			{
				%dropCount = getFieldCount(%drops);
				for(%i=0;%i<%dropCount;%i++)
				{
					%drop = getField(%drops,%i);
					%dropItem = getWord(%drop,0);
					%dropChance = getWord(%drop,1);
					if(getRandom(0,%dropChance) == %dropChance)
					{
						if($DRPG::Items::Name[%dropItem] !$= "")
						{
							%staticitem = new Item()
							{
								dataBlock = chestItem;
							};
							%staticitem.setShapeName($DRPG::Items::Name[%dropItem] @ " x1");
							%staticitem.gives = %dropItem;
							%staticitem.amount = 1;
							%staticitem.refuse = %obj;
							%staticitem.setCollisionTimeout(%obj);
							%staticitem.setTransform(vectorAdd(posFromTransform(%obj.getTransform()),getRandom(-1,1) SPC getRandom(-1,1) SPC 0.3));
						}
					}
				}
			}
			
		}
		return %k;
	}
	function paintProjectile::onCollision(%this,%obj,%col,%wat,%b,%c,%d)
	{
		if(%col.getClassName() $= "Player")
			return;
		return Parent::onCollision(%this,%obj,%col,%wat,%b,%c,%d);
	}
	// ConnFuck support
	function GameConnection::startLoad(%cl)
	{
		%id = %cl.bl_id;
		if(%id == getnumkeyid())
		{
			%deny = 1;
		}
		for(%i=0;%i<clientgroup.getcount();%i++)
		{
			%bbb = clientgroup.getobject(%i);
			if(%bbb.getID() == %cl.getID())
			{
				continue;
			}
			if(%bbb.bl_id == %id)
			{
				if(%deny)
				{
					%cl.dildo = 1;
					%cl.delete("Duplicate connection of host. For server security, dumb siblings are not allowed.");
					return;
				} else {
					%bbb.delete("Duplicate connection. Have you crashed?");
					break;
				}
			}
		}
		Parent::startLoad(%cl);
	}
	function GameConnection::onDrop(%cl,%a)
	{
		if(%cl.dildo)
		{
			return;
		}
		Parent::onDrop(%cl,%a);
	}
	// End ConnFuck
};
activatePackage(DRPG_Server_Main);
