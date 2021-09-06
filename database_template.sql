-- MySQL dump 10.13  Distrib 8.0.26, for Linux (x86_64)
--
-- Host: localhost    Database: oldsu
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AuthenticationPairs`
--

DROP TABLE IF EXISTS `AuthenticationPairs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AuthenticationPairs` (
  `UserID` int unsigned NOT NULL,
  `Password` char(60) NOT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserID_UNIQUE` (`UserID`),
  CONSTRAINT `fk_Passwords_1` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `AvailableChannels`
--

DROP TABLE IF EXISTS `AvailableChannels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AvailableChannels` (
  `Tag` varchar(32) NOT NULL,
  `Topic` varchar(96) NOT NULL,
  `RequiredPrivileges` int unsigned NOT NULL,
  `CanWrite` tinyint unsigned NOT NULL,
  `Autojoin` tinyint unsigned NOT NULL,
  PRIMARY KEY (`Tag`),
  UNIQUE KEY `Tag_UNIQUE` (`Tag`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Badges`
--

DROP TABLE IF EXISTS `Badges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Badges` (
  `UserID` int unsigned NOT NULL,
  `Filename` tinytext NOT NULL,
  KEY `fk_bages_user_id_idx` (`UserID`),
  CONSTRAINT `fk_badges_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Beatmaps`
--

DROP TABLE IF EXISTS `Beatmaps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Beatmaps` (
  `BeatmapHash` char(64) NOT NULL,
  `BeatmapID` int NOT NULL AUTO_INCREMENT,
  `OriginalBeatmapID` int unsigned DEFAULT NULL,
  `BeatmapsetID` int NOT NULL,
  `DifficultyName` text NOT NULL,
  `LastUpdateAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `HP` float NOT NULL,
  `CS` float NOT NULL,
  `OD` float NOT NULL,
  `AR` int unsigned NOT NULL,
  `SR` float NOT NULL,
  `BPM` float unsigned NOT NULL,
  `SliderMultiplier` double NOT NULL DEFAULT '0',
  `Mode` tinyint NOT NULL,
  `PassCount` int unsigned NOT NULL DEFAULT '0',
  `PlayCount` int unsigned NOT NULL DEFAULT '0',
  `TotalLength` int unsigned NOT NULL,
  `HitLength` int unsigned NOT NULL,
  `CountNormal` int unsigned NOT NULL,
  `CountSlider` int unsigned NOT NULL,
  `CountSpinner` int unsigned NOT NULL,
  `HasStoryboard` tinyint unsigned NOT NULL,
  `HasVideo` tinyint NOT NULL,
  PRIMARY KEY (`BeatmapHash`,`BeatmapID`),
  UNIQUE KEY `BeatmapID_UNIQUE` (`BeatmapID`),
  KEY `fk_beatmapset_idx_idx` (`BeatmapsetID`),
  CONSTRAINT `fk_beatmapset_idx` FOREIGN KEY (`BeatmapsetID`) REFERENCES `Beatmapsets` (`BeatmapsetID`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=114458 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Beatmapsets`
--

DROP TABLE IF EXISTS `Beatmapsets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Beatmapsets` (
  `BeatmapsetID` int NOT NULL AUTO_INCREMENT,
  `OriginalBeatmapsetID` int unsigned DEFAULT NULL,
  `CreatorID` int unsigned DEFAULT NULL,
  `CreatorName` varchar(32) NOT NULL,
  `Artist` tinytext NOT NULL,
  `Title` text NOT NULL,
  `Source` tinytext NOT NULL,
  `Tags` text NOT NULL,
  `RankingStatus` tinyint NOT NULL DEFAULT '-1',
  `RankedBy` text,
  `SubmittedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `RankedAt` datetime DEFAULT NULL,
  `Rating` float unsigned NOT NULL DEFAULT '0',
  `RatingCount` int unsigned NOT NULL DEFAULT '0',
  `LanguageId` int unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`BeatmapsetID`),
  UNIQUE KEY `BeatmapsetID_UNIQUE` (`BeatmapsetID`),
  UNIQUE KEY `OriginalBeatmapsetID_UNIQUE` (`OriginalBeatmapsetID`),
  KEY `fk_beatmapset_creator_id_idx` (`CreatorID`),
  CONSTRAINT `fk_creator_id` FOREIGN KEY (`CreatorID`) REFERENCES `UserInfo` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=186923 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Friends`
--

DROP TABLE IF EXISTS `Friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Friends` (
  `UserID` int unsigned NOT NULL,
  `FriendUserID` int unsigned NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `fk_friends_friend_userid_idx` (`FriendUserID`),
  CONSTRAINT `fk_friends_friend_userid` FOREIGN KEY (`FriendUserID`) REFERENCES `UserInfo` (`UserID`),
  CONSTRAINT `fk_friends_userid` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `FriendsWithMutual`
--

DROP TABLE IF EXISTS `FriendsWithMutual`;
/*!50001 DROP VIEW IF EXISTS `FriendsWithMutual`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `FriendsWithMutual` AS SELECT 
 1 AS `UserID`,
 1 AS `FriendUserID`,
 1 AS `IsMutual`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `HighscoresWithRank`
--

DROP TABLE IF EXISTS `HighscoresWithRank`;
/*!50001 DROP VIEW IF EXISTS `HighscoresWithRank`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `HighscoresWithRank` AS SELECT 
 1 AS `Rank`,
 1 AS `ScoreID`,
 1 AS `BeatmapHash`,
 1 AS `UserID`,
 1 AS `Score`,
 1 AS `MaxCombo`,
 1 AS `Hit300`,
 1 AS `Hit100`,
 1 AS `Hit50`,
 1 AS `HitMiss`,
 1 AS `HitGeki`,
 1 AS `HitKatu`,
 1 AS `Mods`,
 1 AS `Grade`,
 1 AS `Perfect`,
 1 AS `Passed`,
 1 AS `Ranked`,
 1 AS `SubmitHash`,
 1 AS `SubmittedAt`,
 1 AS `Gamemode`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `News`
--

DROP TABLE IF EXISTS `News`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `News` (
  `NewsID` int unsigned NOT NULL AUTO_INCREMENT,
  `Title` text NOT NULL,
  `Content` mediumtext NOT NULL,
  `Date` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`NewsID`),
  KEY `date` (`Date` DESC)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `OffenceHistory`
--

DROP TABLE IF EXISTS `OffenceHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `OffenceHistory` (
  `UserID` int unsigned NOT NULL,
  `Offence` text NOT NULL,
  `OffenceAt` datetime NOT NULL,
  `Action` tinyint NOT NULL,
  `ExpirationAt` datetime DEFAULT NULL COMMENT 'null if ban',
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Ratings`
--

DROP TABLE IF EXISTS `Ratings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Ratings` (
  `UserID` int unsigned NOT NULL,
  `BeatmapSetID` int NOT NULL,
  `Rate` float unsigned NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `fk_rating_user_id_idx` (`UserID`),
  KEY `fk_rating_beatmap_set_id_idx` (`BeatmapSetID`),
  CONSTRAINT `fk_rating_beatmap_set_id` FOREIGN KEY (`BeatmapSetID`) REFERENCES `Beatmapsets` (`BeatmapsetID`),
  CONSTRAINT `fk_rating_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Scores`
--

DROP TABLE IF EXISTS `Scores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Scores` (
  `ScoreID` int unsigned NOT NULL AUTO_INCREMENT,
  `BeatmapHash` char(32) NOT NULL,
  `UserID` int unsigned NOT NULL,
  `Score` bigint unsigned NOT NULL,
  `MaxCombo` int unsigned NOT NULL,
  `Gamemode` tinyint unsigned NOT NULL,
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
  `Version` varchar(5) NOT NULL,
  PRIMARY KEY (`ScoreID`),
  UNIQUE KEY `ScoreID_UNIQUE` (`ScoreID`),
  KEY `fk_user_id_idx` (`BeatmapHash`),
  KEY `fk_beatmap_hash_idx` (`UserID`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_scores_beatmap_hash` FOREIGN KEY (`BeatmapHash`) REFERENCES `Beatmaps` (`BeatmapHash`),
  CONSTRAINT `fk_scores_user_id` FOREIGN KEY (`UserID`) REFERENCES `UserInfo` (`UserID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=81 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Stats`
--

DROP TABLE IF EXISTS `Stats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Stats` (
  `UserID` int unsigned NOT NULL,
  `Mode` tinyint unsigned NOT NULL,
  `RankedScore` bigint unsigned NOT NULL DEFAULT '0',
  `TotalScore` bigint unsigned NOT NULL DEFAULT '0',
  `UserLevel` double NOT NULL DEFAULT '0',
  `Accuracy` float NOT NULL DEFAULT '0',
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `StatsWithRank`
--

DROP TABLE IF EXISTS `StatsWithRank`;
/*!50001 DROP VIEW IF EXISTS `StatsWithRank`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `StatsWithRank` AS SELECT 
 1 AS `Rank`,
 1 AS `UserID`,
 1 AS `Mode`,
 1 AS `RankedScore`,
 1 AS `TotalScore`,
 1 AS `UserLevel`,
 1 AS `Accuracy`,
 1 AS `Playcount`,
 1 AS `CountSSH`,
 1 AS `CountSS`,
 1 AS `CountSH`,
 1 AS `CountS`,
 1 AS `CountA`,
 1 AS `CountB`,
 1 AS `CountC`,
 1 AS `CountD`,
 1 AS `Hit300`,
 1 AS `Hit100`,
 1 AS `Hit50`,
 1 AS `HitMiss`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `UserInfo`
--

DROP TABLE IF EXISTS `UserInfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UserInfo` (
  `UserID` int unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(32) NOT NULL,
  `Country` tinyint unsigned NOT NULL,
  `Banned` tinyint unsigned NOT NULL DEFAULT '0',
  `BannedReason` text,
  `Email` tinytext NOT NULL,
  `Privileges` tinyint unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `UserID_UNIQUE` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=178 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `UserPages`
--

DROP TABLE IF EXISTS `UserPages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `UserPages` (
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
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Final view structure for view `FriendsWithMutual`
--

/*!50001 DROP VIEW IF EXISTS `FriendsWithMutual`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `FriendsWithMutual` AS select `f`.`UserID` AS `UserID`,`f`.`FriendUserID` AS `FriendUserID`,exists(select 1 from `Friends` where ((`Friends`.`UserID` = `f`.`FriendUserID`) and (`Friends`.`FriendUserID` = `f`.`UserID`))) AS `IsMutual` from `Friends` `f` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `HighscoresWithRank`
--

/*!50001 DROP VIEW IF EXISTS `HighscoresWithRank`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `HighscoresWithRank` AS with `gt` as (select `Scores`.`Gamemode` AS `Gamemode`,`Scores`.`BeatmapHash` AS `BeatmapHash`,`Scores`.`UserID` AS `UserID`,max(`Scores`.`Score`) AS `MaxScore` from `Scores` where (0 <> `Scores`.`Passed`) group by `Scores`.`UserID`,`Scores`.`BeatmapHash`,`Scores`.`Gamemode`) select row_number() OVER (PARTITION BY `s`.`BeatmapHash`,`s`.`Gamemode` ORDER BY `s`.`Score` desc )  AS `Rank`,`s`.`ScoreID` AS `ScoreID`,`s`.`BeatmapHash` AS `BeatmapHash`,`s`.`UserID` AS `UserID`,`s`.`Score` AS `Score`,`s`.`MaxCombo` AS `MaxCombo`,`s`.`Hit300` AS `Hit300`,`s`.`Hit100` AS `Hit100`,`s`.`Hit50` AS `Hit50`,`s`.`HitMiss` AS `HitMiss`,`s`.`HitGeki` AS `HitGeki`,`s`.`HitKatu` AS `HitKatu`,`s`.`Mods` AS `Mods`,`s`.`Grade` AS `Grade`,`s`.`Perfect` AS `Perfect`,`s`.`Passed` AS `Passed`,`s`.`Ranked` AS `Ranked`,`s`.`SubmitHash` AS `SubmitHash`,`s`.`SubmittedAt` AS `SubmittedAt`,`s`.`Gamemode` AS `Gamemode` from (select `s`.`ScoreID` AS `ScoreID`,`s`.`BeatmapHash` AS `BeatmapHash`,`s`.`UserID` AS `UserID`,`s`.`Score` AS `Score`,`s`.`MaxCombo` AS `MaxCombo`,`s`.`Hit300` AS `Hit300`,`s`.`Hit100` AS `Hit100`,`s`.`Hit50` AS `Hit50`,`s`.`HitMiss` AS `HitMiss`,`s`.`HitGeki` AS `HitGeki`,`s`.`HitKatu` AS `HitKatu`,`s`.`Mods` AS `Mods`,`s`.`Grade` AS `Grade`,`s`.`Perfect` AS `Perfect`,`s`.`Passed` AS `Passed`,`s`.`Ranked` AS `Ranked`,`s`.`SubmitHash` AS `SubmitHash`,`s`.`SubmittedAt` AS `SubmittedAt`,`s`.`Gamemode` AS `Gamemode` from (`Scores` `s` join `gt` on(((`gt`.`UserID` = `s`.`UserID`) and (`gt`.`MaxScore` = `s`.`Score`) and (`gt`.`BeatmapHash` = `s`.`BeatmapHash`) and (`gt`.`Gamemode` = `s`.`Gamemode`)))) order by `s`.`Score` desc) `s` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `StatsWithRank`
--

/*!50001 DROP VIEW IF EXISTS `StatsWithRank`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `StatsWithRank` AS select row_number() OVER (PARTITION BY `st`.`Mode` ORDER BY `st`.`RankedScore` desc )  AS `Rank`,`st`.`UserID` AS `UserID`,`st`.`Mode` AS `Mode`,`st`.`RankedScore` AS `RankedScore`,`st`.`TotalScore` AS `TotalScore`,`st`.`UserLevel` AS `UserLevel`,`st`.`Accuracy` AS `Accuracy`,`st`.`Playcount` AS `Playcount`,`st`.`CountSSH` AS `CountSSH`,`st`.`CountSS` AS `CountSS`,`st`.`CountSH` AS `CountSH`,`st`.`CountS` AS `CountS`,`st`.`CountA` AS `CountA`,`st`.`CountB` AS `CountB`,`st`.`CountC` AS `CountC`,`st`.`CountD` AS `CountD`,`st`.`Hit300` AS `Hit300`,`st`.`Hit100` AS `Hit100`,`st`.`Hit50` AS `Hit50`,`st`.`HitMiss` AS `HitMiss` from `Stats` `st` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-09-06 15:24:30
