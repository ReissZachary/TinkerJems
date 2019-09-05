param(
	[string]$dbName="TinkerJems"
)

install-module sqlserver -Scope CurrentUser


$mdffilename=(get-item ".\$dbName.mdf").FullName;
$ldffilename=(get-item ".\$dbName.ldf").FullName;
$sqlCmd = @"
USE [master]
GO
CREATE DATABASE [$DBName] ON (FILENAME = '$mdfFilename'),(FILENAME = '$ldfFilename') for ATTACH
GO
"@

invoke-sqlcmd $sqlCmd -ServerInstance "(localdb)\mssqllocaldb"