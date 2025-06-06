forceRequiredAddon("Player_No_Jet");
datablock PlayerData(SpeedPerk1Player : PlayerNoJet)
{
	runForce = 4320*1.2;
	maxForwardSpeed = 7*1.2;
	maxBackwardSpeed = 4*1.2;
	maxSideSpeed = 6*1.2;
	maxUnderwaterForwardSpeed = 8.4*1.2;
	maxUnderwaterBackwardSpeed = 7.8*1.2;
	maxUnderwaterSideSpeed = 7.8*1.2;
	maxForwardCrouchSpeed = 3*1.2;
	maxBackwardCrouchSpeed = 2*1.2;
	maxSideCrouchSpeed = 2*1.2;
};
datablock PlayerData(SpeedPerk2Player : PlayerNoJet)
{
	runForce = 4320*1.35;
	maxForwardSpeed = 7*1.35;
	maxBackwardSpeed = 4*1.35;
	maxSideSpeed = 6*1.35;
	maxUnderwaterForwardSpeed = 8.4*1.35;
	maxUnderwaterBackwardSpeed = 7.8*1.35;
	maxUnderwaterSideSpeed = 7.8*1.35;
	maxForwardCrouchSpeed = 3*1.35;
	maxBackwardCrouchSpeed = 2*1.35;
	maxSideCrouchSpeed = 2*1.35;
};
datablock PlayerData(SpeedPerk3Player : PlayerNoJet)
{
	runForce = 4320*1.5;
	maxForwardSpeed = 7*1.5;
	maxBackwardSpeed = 4*1.5;
	maxSideSpeed = 6*1.5;
	maxUnderwaterForwardSpeed = 8.4*1.5;
	maxUnderwaterBackwardSpeed = 7.8*1.5;
	maxUnderwaterSideSpeed = 7.8*1.5;
	maxForwardCrouchSpeed = 3*1.5;
	maxBackwardCrouchSpeed = 2*1.5;
	maxSideCrouchSpeed = 2*1.5;
};
$SpeedPerk1 = SpeedPerk1Player;
$SpeedPerk2 = SpeedPerk2Player;
$SpeedPerk3 = SpeedPerk3Player;

function Player::Regen(%this,%level)
{
	cancel(%this.regen);
	if(%level == 1)
		%this.addHealth(1.5);
	else if(%level == 2)
		%this.addHealth(2.5);
	else if(%level == 3)
		%this.addHealth(5);
	%this.regen = %this.schedule(1000,regen,%level);
}

function GameConnection::doPerkChecks(%this)
{
	if(!isObject(%p = %this.player))
		return;
	%nation = $DRPG::Accounts.value[%this.BL_ID,"nation"];
	if(%lv = getPerkLevel(%nation,Regen) > 0)
		%p.regen(%lv);
	if(%lv = getPerkLevel(%nation,Speed) > 0)
		%p.schedule(33,changeDataBlock,$SpeedPerk[%lv].getID(),%this);
	//%this.schedule(501,applyBodyColors);
	%this.player.noAppearanceChange = 0;
	%this.schedule(33,applyBodyParts,2);
	schedule(34,0,eval,%this.player @ ".noappearancechange = 1;");
	%this.schedule(33,applyEquipment);
	%this.setNationColor();
}