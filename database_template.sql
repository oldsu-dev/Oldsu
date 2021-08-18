CREATE TABLE `AvailableChannels` (
  `Tag` varchar(32) NOT NULL,
  `Topic` varchar(96) NOT NULL,
  `RequiredPrivileges` int unsigned NOT NULL,
  `CanWrite` tinyint unsigned NOT NULL,
  `Autojoin` tinyint unsigned NOT NULL,
  PRIMARY KEY (`Tag`),
  UNIQUE KEY `Tag_UNIQUE` (`Tag`)
);

CREATE TABLE `UserInfo` (
  `UserID` int unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(32) NOT NULL,
  `Password` char(64) NOT NULL,
  `Country` tinyint unsigned NOT NULL,
  `Banned` tinyint unsigned NOT NULL DEFAULT '0',
  `BannedReason` text,
  `Email` tinytext NOT NULL,
  `Privileges` tinyint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Username_UNIQUE` (`Username`),
  UNIQUE KEY `UserID_UNIQUE` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Friends` (
  `UserID` INT UNSIGNED NOT NULL,
  `FriendUserID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`UserID`),
  INDEX `fk_friends_friend_userid_idx` (`FriendUserID` ASC) VISIBLE,
  CONSTRAINT `fk_friends_userid`
    FOREIGN KEY (`UserID`)
    REFERENCES `UserInfo` (`UserID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_friends_friend_userid`
    FOREIGN KEY (`FriendUserID`)
    REFERENCES `UserInfo`  (`UserID`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

CREATE TABLE `OffenceHistory` (
  `UserID` INT UNSIGNED NOT NULL,
  `Offence` TEXT NOT NULL,
  `OffenceAt` DATETIME NOT NULL,
  `Action` TINYINT(10) NOT NULL,
  `ExpirationAt` DATETIME NULL COMMENT 'null if ban',
  PRIMARY KEY (`UserID`));

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
  CONSTRAINT `fk_beatmapset_creator_id` FOREIGN KEY (`CreatorID`) REFERENCES `UserInfo` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE `Badges` (
  `UserID` int unsigned NOT NULL,
  `Filename` tinytext NOT NULL,
  KEY `fk_bages_user_id_idx` (`UserID`),
  CONSTRAINT `fk_badges_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
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
  CONSTRAINT `fk_scores_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
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
  CONSTRAINT `fk_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
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
  CONSTRAINT `fk_userid` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE VIEW `HighScoresWithRank` AS with `gt` as (select `Scores`.`Gamemode` AS `Gamemode`,`Scores`.`BeatmapHash` AS `BeatmapHash`,`Scores`.`UserID` AS `UserID`,max(`Scores`.`Score`) AS `MaxScore` from `Scores` where (0 <> `Scores`.`Passed`) group by `Scores`.`UserID`,`Scores`.`BeatmapHash`,`Scores`.`Gamemode`) select row_number() OVER (PARTITION BY `s`.`BeatmapHash`,`s`.`Gamemode` ORDER BY `s`.`Score` desc )  AS `Rank`,`s`.`ScoreID` AS `ScoreID`,`s`.`BeatmapHash` AS `BeatmapHash`,`s`.`UserID` AS `UserID`,`s`.`Score` AS `Score`,`s`.`MaxCombo` AS `MaxCombo`,`s`.`Hit300` AS `Hit300`,`s`.`Hit100` AS `Hit100`,`s`.`Hit50` AS `Hit50`,`s`.`HitMiss` AS `HitMiss`,`s`.`HitGeki` AS `HitGeki`,`s`.`HitKatu` AS `HitKatu`,`s`.`Mods` AS `Mods`,`s`.`Grade` AS `Grade`,`s`.`Perfect` AS `Perfect`,`s`.`Passed` AS `Passed`,`s`.`Ranked` AS `Ranked`,`s`.`SubmitHash` AS `SubmitHash`,`s`.`SubmittedAt` AS `SubmittedAt`,`s`.`Gamemode` AS `Gamemode` from (select `s`.`ScoreID` AS `ScoreID`,`s`.`BeatmapHash` AS `BeatmapHash`,`s`.`UserID` AS `UserID`,`s`.`Score` AS `Score`,`s`.`MaxCombo` AS `MaxCombo`,`s`.`Hit300` AS `Hit300`,`s`.`Hit100` AS `Hit100`,`s`.`Hit50` AS `Hit50`,`s`.`HitMiss` AS `HitMiss`,`s`.`HitGeki` AS `HitGeki`,`s`.`HitKatu` AS `HitKatu`,`s`.`Mods` AS `Mods`,`s`.`Grade` AS `Grade`,`s`.`Perfect` AS `Perfect`,`s`.`Passed` AS `Passed`,`s`.`Ranked` AS `Ranked`,`s`.`SubmitHash` AS `SubmitHash`,`s`.`SubmittedAt` AS `SubmittedAt`,`s`.`Gamemode` AS `Gamemode` from (`Scores` `s` join `gt` on(((`gt`.`UserID` = `s`.`UserID`) and (`gt`.`MaxScore` = `s`.`Score`) and (`gt`.`BeatmapHash` = `s`.`BeatmapHash`) and (`gt`.`Gamemode` = `s`.`Gamemode`)))) order by `s`.`Score` desc) `s`

CREATE VIEW `StatsWithRank` AS select row_number() OVER (PARTITION BY `st`.`Mode` ORDER BY `st`.`RankedScore` )  AS `Rank`,`st`.`UserID` AS `UserID`,`st`.`Mode` AS `Mode`,`st`.`RankedScore` AS `RankedScore`,`st`.`TotalScore` AS `TotalScore`,`st`.`UserLevel` AS `UserLevel`,`st`.`Accuracy` AS `Accuracy`,`st`.`Playcount` AS `Playcount`,`st`.`CountSSH` AS `CountSSH`,`st`.`CountSS` AS `CountSS`,`st`.`CountSH` AS `CountSH`,`st`.`CountS` AS `CountS`,`st`.`CountA` AS `CountA`,`st`.`CountB` AS `CountB`,`st`.`CountC` AS `CountC`,`st`.`CountD` AS `CountD`,`st`.`Hit300` AS `Hit300`,`st`.`Hit100` AS `Hit100`,`st`.`Hit50` AS `Hit50`,`st`.`HitMiss` AS `HitMiss` from `Stats` `st`;

INSERT INTO `AvailableChannels` (`Tag`, `Topic`, `RequiredPrivileges`, `Autojoin`, `CanWrite`) VALUES ('#finnish', 'Discussion in finnish.', '0', '0', '1');
INSERT INTO `AvailableChannels` (`Tag`, `Topic`, `RequiredPrivileges`, `Autojoin`, `CanWrite`) VALUES ('#staff', 'Staff-chat.', '8', '1', '1');
