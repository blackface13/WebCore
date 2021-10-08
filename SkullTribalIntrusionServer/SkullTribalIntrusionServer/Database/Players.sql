Drop TABLE [dbo].[Players]
CREATE TABLE [dbo].[Players]
(
	[PlayerId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PlayerName] NVARCHAR(50) NULL, 
    [Level] INT NULL DEFAULT 0, 
    [Exp] real NULL DEFAULT 0, 
    [Golds] BIGINT NULL DEFAULT 0, 
    [Silver] BIGINT NULL DEFAULT 0, 
    [RoundNumber] INT NULL DEFAULT 0, 
    [ShotNumber] BIGINT NULL DEFAULT 0, 
    [ShotHitted] BIGINT NULL DEFAULT 0, 
    [ShotToHead] BIGINT NULL DEFAULT 0, 
    [ShotHighAngle] BIGINT NULL DEFAULT 0, 
    [ShotSuperHighAngle] BIGINT NULL DEFAULT 0, 
    [HittedNumber] BIGINT NULL DEFAULT 0, 
    [HittedHeadNumber] BIGINT NULL DEFAULT 0, 
    [DamageCreated] real NULL DEFAULT 0, 
    [DamageReceived] real NULL DEFAULT 0,
    StartedDate DateTime NULL,
    ArrowHeight BIGINT NULL DEFAULT 0,
    ArrowSuperHeight BIGINT NULL DEFAULT 0,
    TotalBattleTime BIGINT NULL DEFAULT 0,
    LastTimeSync INT NULL DEFAULT 0,
    ArrowsBagValues Nvarchar(20) NULL,
    ArrowsBuyedValues Nvarchar(20) NULL
)
