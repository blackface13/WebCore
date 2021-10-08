drop TABLE [dbo].[PlayerItems]
CREATE TABLE [dbo].[PlayerItems]
(
	[ItemGuidId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[PlayerEntityPlayerId] UNIQUEIDENTIFIER NULL DEFAULT NULL, 
	[PlayerId] UNIQUEIDENTIFIER not null, 
    [ItemTypeMode] INT NULL DEFAULT -1, 
    [ItemId] INT NULL DEFAULT -1, 
    [Index] INT NULL DEFAULT -1, 
    [Quantity] INT NULL DEFAULT -1, 
)
