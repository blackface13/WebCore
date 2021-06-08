CREATE TABLE [dbo].[Players]
(
	[PlayerId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PlayerName] NVARCHAR(50) NULL, 
    [Golds] BIGINT NULL DEFAULT 0, 
    [Gems] BIGINT NULL DEFAULT 0, 
    [RoundNumber] INT NULL DEFAULT 0, 
    [ShotNumber] BIGINT NULL DEFAULT 0, 
    [ShotHitted] BIGINT NULL DEFAULT 0, 
    [ShotToHead] BIGINT NULL DEFAULT 0, 
    [ShotHighAngle] BIGINT NULL DEFAULT 0, 
    [ShotSuperHighAngle] BIGINT NULL DEFAULT 0, 
    [HittedNumber] BIGINT NULL DEFAULT 0, 
    [HittedHeadNumber] BIGINT NULL DEFAULT 0, 
    [DamageCreated] REAL NULL DEFAULT 0, 
    [DamageReceived] REAL NULL DEFAULT 0
)
