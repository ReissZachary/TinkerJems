param(
	[string]$dbName="TinkerJems"
)

$sqlCmd = @"
USE [master]
GO
sp_detach_db $dbName
GO
"@

invoke-sqlcmd $sqlCmd -ServerInstance "(localdb)\mssqllocaldb"