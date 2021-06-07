﻿CREATE TABLE [dbo].[Players]
(
	[PlayerId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PlayerName] NVARCHAR(50) NULL, 
    [Golds] BIGINT NULL DEFAULT 0, 
    [Gems] BIGINT NULL DEFAULT 0, 
    [RoundNumber] BIGINT NULL DEFAULT 0, 
    [ShotNumber] BIGINT NULL DEFAULT 0, 
    [ShotHitted] BIGINT NULL DEFAULT 0, 
    [ShotToHead] BIGINT NULL DEFAULT 0, 
    [ShotHighAngle] BIGINT NULL DEFAULT 0, 
    [ShotSuperHighAngle] BIGINT NULL DEFAULT 0, 
    [HittedNumber] BIGINT NULL DEFAULT 0, 
    [HittedHeadNumber] BIGINT NULL, 
    [DamageCreated] BIGINT NULL DEFAULT 0, 
    [DamageReceived] BIGINT NULL DEFAULT 0
)
