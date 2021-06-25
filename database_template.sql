
CREATE TABLE `Users` (
  `UserID` int unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(32) NOT NULL,
  `Password` char(32) NOT NULL,
  `Country` tinyint unsigned NOT NULL,
  `Banned` tinyint unsigned NOT NULL DEFAULT '0',
  `BannedReason` text,
  `Email` tinytext NOT NULL,
  `Privileges` tinyint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`),
  UNIQUE KEY `UserID_UNIQUE` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Beatmapsets` (
  `BeatmapsetID` int NOT NULL AUTO_INCREMENT,
  `CreatorID` int unsigned NOT NULL,
  `Artist` tinytext NOT NULL,
  `Title` text NOT NULL,
  `Source` tinytext NOT NULL,
  `Tags` json NOT NULL,
  `RankingStatus` tinyint NOT NULL DEFAULT '-1',
  `RankedBy` text,
  `SubmittedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`BeatmapsetID`),
  UNIQUE KEY `BeatmapsetID_UNIQUE` (`BeatmapsetID`),
  KEY `fk_beatmapset_creator_id_idx` (`CreatorID`),
  CONSTRAINT `fk_beatmapset_creator_id` FOREIGN KEY (`CreatorID`) REFERENCES `Users` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Badges` (
  `UserID` int unsigned NOT NULL,
  `Filename` tinytext NOT NULL,
  KEY `fk_bages_user_id_idx` (`UserID`),
  CONSTRAINT `fk_badges_user_id` FOREIGN KEY (`UserID`) REFERENCES `Users` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Beatmaps` (
  `BeatmapID` int NOT NULL AUTO_INCREMENT,
  `BeatmapHash` char(64) NOT NULL,
  `BeatmapsetID` int NOT NULL,
  `HP` float NOT NULL,
  `CS` float NOT NULL,
  `OD` float NOT NULL,
  `SR` float NOT NULL,
  `BPM` int unsigned NOT NULL,
  `SliderMultiplier` double NOT NULL,
  `Mode` tinyint NOT NULL,
  PRIMARY KEY (`BeatmapID`,`BeatmapHash`),
  UNIQUE KEY `BeatmapID_UNIQUE` (`BeatmapID`),
  UNIQUE KEY `BeatmapHash_UNIQUE` (`BeatmapHash`),
  KEY `fk_beatmaps_idx` (`BeatmapsetID`),
  CONSTRAINT `fk_beatmaps_idx` FOREIGN KEY (`BeatmapsetID`) REFERENCES `Beatmapsets` (`BeatmapsetID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Scores` (
  `ScoreID` int unsigned NOT NULL AUTO_INCREMENT,
  `BeatmapHash` char(64) NOT NULL,
  `UserID` int unsigned NOT NULL,
  `Score` bigint unsigned NOT NULL,
  `MaxCombo` int unsigned NOT NULL,
  `Hit300` int unsigned NOT NULL,
  `Hit100` int unsigned NOT NULL,
  `Hit50` int unsigned NOT NULL,
  `HitMiss` int unsigned NOT NULL,
  `HitGeki` int unsigned NOT NULL,
  `HitKatu` int unsigned NOT NULL,
  `Mods` int unsigned NOT NULL,
  `Grade` varchar(3) NOT NULL,
  `Perfect` tinyint unsigned NOT NULL,
  `Passed` tinyint unsigned NOT NULL,
  `Ranked` tinyint unsigned NOT NULL,
  `SubmitHash` char(64) NOT NULL,
  `SubmittedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ScoreID`),
  UNIQUE KEY `ScoreID_UNIQUE` (`ScoreID`),
  KEY `fk_user_id_idx` (`BeatmapHash`),
  KEY `fk_beatmap_hash_idx` (`UserID`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_scores_beatmap_hash` FOREIGN KEY (`BeatmapHash`) REFERENCES `Beatmaps` (`BeatmapHash`),
  CONSTRAINT `fk_scores_user_id` FOREIGN KEY (`UserID`) REFERENCES `Users` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Stats` (
  `UserID` int unsigned NOT NULL,
  `Mode` tinyint unsigned NOT NULL,
  `RankedScore` bigint unsigned NOT NULL DEFAULT '0',
  `TotalScore` bigint unsigned NOT NULL DEFAULT '0',
  `UserLevel` double NOT NULL DEFAULT '0',
  `Accuracy` double NOT NULL DEFAULT '0',
  `Playcount` bigint unsigned NOT NULL DEFAULT '0',
  `CountSSH` int unsigned NOT NULL DEFAULT '0',
  `CountSS` int unsigned NOT NULL DEFAULT '0',
  `CountSH` int unsigned NOT NULL DEFAULT '0',
  `CountS` int unsigned NOT NULL DEFAULT '0',
  `CountA` int unsigned NOT NULL DEFAULT '0',
  `CountB` int unsigned NOT NULL DEFAULT '0',
  `CountC` int unsigned NOT NULL DEFAULT '0',
  `CountD` int unsigned NOT NULL DEFAULT '0',
  `Hit300` bigint unsigned NOT NULL DEFAULT '0',
  `Hit100` bigint unsigned NOT NULL DEFAULT '0',
  `Hit50` bigint unsigned NOT NULL DEFAULT '0',
  `HitMiss` bigint unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `stat_UNIQUE` (`UserID`,`Mode`),
  KEY `user_id_idx` (`UserID`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_user_id` FOREIGN KEY (`UserID`) REFERENCES `Users` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Userpages` (
  `UserID` int unsigned NOT NULL,
  `Birthday` date DEFAULT NULL,
  `Occupation` tinytext,
  `Interests` tinytext,
  `Website` tinytext,
  `Twitter` tinytext,
  `Discord` tinytext,
  UNIQUE KEY `UserID_UNIQUE` (`UserID`),
  CONSTRAINT `fk_userid` FOREIGN KEY (`UserID`) REFERENCES `Users` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


