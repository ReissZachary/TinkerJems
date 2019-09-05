param(
	[string]$dbName="TinkerJems"
)

install-module sqlserver -Scope CurrentUser

$sqlCmd = @"
USE [master]
GO
sp_detach_db $dbName
GO
"@

invoke-sqlcmd $sqlCmd -ServerInstance "(localdb)\mssqllocaldb"