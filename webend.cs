if(!isObject(TQSWebserver))
{
	new TCPObject(TQSWebserver);
	TQSWebserver.listen(28650);
}

function TQSWebserver::onConnectRequest(%this,%addr,%id)
{
	%cl    = new TCPObject(TQSWebclient,%id);
	%cl.ip = getWord(strReplace(%addr,":"," "),1);
	
	%cl.timeout = %cl.schedule(1000,disconnect);
}

function TQSWebclient::onLine(%this,%line)
{
	cancel(%this.timeout);
	
	%this.packet  = %this.packet @ %line @ "\r\n";
	%this.timeout = %this.schedule(500,finish);
}

function TQSWebclient::finish(%this)
{
	%lines   = strReplace(%this.packet,"\r\n","\t");
	%request = getField(%lines,0);
	
	%command = getWord(%request,0);
	%page    = getWord(%request,1);
	%version = getWord(%request,2);
	
	$IP  = %this.ip;
	%pos = strPos(%page,"?");
	
	deleteVariables("$GET*");
	deleteVariables("$POST*");

	if(%command $= "GET" && %pos != -1)
	{
		%args = getSubStr(%page,%pos + 1,strLen(%page));
		%page = getSubStr(%page,0,%pos);
		
		%args = strReplace(%args,"&","\t");
		%num  = getFieldCount(%args);
		
		for(%i = 0; %i < %num; %i++)
		{
			%arg = getField(%args,%i);
			%arg = strReplace(%arg,"=","\t");
			
			$GET[getField(%arg,0)] = getFields(%arg,1);
		}
	}
	else if(%command $= "POST")
	{
		// totally untested ok
		%args = getField(%lines,getFieldCount(%lines) - 1);
		%args = strReplace(%args,"&","\t");
		%cnt  = getFieldCount(%args);
		
		for(%i = 0; %i < %cnt; %i++)
		{
			%arg = getField(%args,%i);
			%arg = strReplace(%arg,"=","\t");
			
			$POST[getField(%arg,0)] = getFields(%arg,1);

		}
	}
	
	if(%page $= "/")
		%page = "/index.tqs";
	
	%page = "config/web" @ %page;
	
	if(isFile(%page))
	{
		%file = new FileObject();
		%file.openForRead(%page);
		
		while(!%file.isEOF())
			%body = %body @ %file.readLine() @ "\n";
		
		%file.close();
		%file.delete();
		
		%body = getSubStr(%body,0,strLen(%body) - 1);
		
		while(strPos(%body,"<?tqs") != -1)
			%body = parseTQS(%body);
		
		%this.send("HTTP/1.1 200 OK\r\n");
		%this.send("Content-Length: " @ strLen(%body) @ "\r\n");
		%this.send("Content-Type: text/html; charset=UTF-8\r\n");
		%this.send("Connection: close\r\n");
		%this.send("\r\n");
		%this.send(%body @ "\r\n");
	}
	else
	{
		%this.send("HTTP/1.1 404 Not Found\r\n");
		%this.send("Connection: close\r\n");
		%this.send("\r\n");
	}
	
	%this.disconnect();
}

function parseTQS(%body)
{
	%pos   = strPos(%body,"<?tqs");
	%rest  = getSubStr(%body,%pos + 5,strLen(%body));
	%body  = getSubStr(%body,0,%pos);
	%pos   = strPos(%rest,"?>");
	%eval  = getSubStr(%rest,0,%pos);
	$cache = "";
	
	eval(%eval);
	
	return %body @ $cache @ getSubStr(%rest,%pos + 2,strLen(%rest));
}

function print(%string)
{
	// Basically announce except for web interface.
	$cache = $cache @ %string;
}