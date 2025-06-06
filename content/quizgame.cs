
function startQuizGame(%item,%question,%answer,%amount)
{
	if(%item !$= "plastic" && $DRPG::Items::Name[%item] $= "")
		return;
	if(%item $= "plastic")
		messageAll('','\c4First person to answer correctly will get %2 \c2%1\c4!',"plastic",%amount);	
	else
		messageAll('','\c4First person to answer correctly will get %2 \c2%1\c4!',$DRPG::Items::Name[%item],%amount);
	messageAll('',"\c6" @ %question);
	messageAll('','\c4Type /answer ANSWERHERE to answer the question.');
	$server::question = %question;
	$server::answer = %answer;
	$server::item = %item;
	$server::amount = %amount;
	$server::gamego = 1;
}

function serverCmdAnswer(%c,%a)
{
	if(%c.answerBan)
		return;
	echo("\c1" @ %c.getPlayerName() SPC %a);
	if(%c.lastanswerTime+0.1 > getSimTime()/1000)
	{
		messageClient(%c,'',"You are answering too fast.");
		%c.warnings++;
		%c.lastWarningTime = getSimTime()/1000;
		if(%c.warnings > 10 && %c.lastWarningTime+1 > getSimTime()/1000)
		{
			messageClient(%c,'',"You are no longer allowed to answer due to using console loops.");
			%c.answerBan = 1;
		}
		return;
	}
	%c.lastAnswerTime = getSimTime()/1000;
	if($server::question $= "Do you like My Little Pony?" && %a $= "yes")
	{
		%c.delete();
		return;
	}
	if(!$server::gamego)
		return;
	if(%a $= $server::answer)
	{
		messageAll('','\c2%1\c4 got it right! The answer was \c2%2\c4.',%c.getPlayerName(),$server::answer);
		$server::gamego = 0;
		if($server::item $= "plastic")
		{
			$DRPG::Accounts.value[%c.bl_id,"plastic"] += $server::amount;
			commandToClient(%c,'updateplastic',$DRPG::Accounts.value[%c.bl_id,"plastic"]);
		}
		else
			%c.addItem($server::item,$server::amount);
	}
}