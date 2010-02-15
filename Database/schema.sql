/*
SQLyog Enterprise - MySQL GUI v7.02 
MySQL - 5.1.42-community : Database - stackunderflow
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`stackunderflow` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `stackunderflow`;

/*Table structure for table `answers` */

DROP TABLE IF EXISTS `answers`;

CREATE TABLE `answers` (
  `Id` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `QuestionId` int(4) unsigned NOT NULL,
  `Body` varchar(1500) NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `LastRelatedUser` int(4) unsigned NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `Author` int(4) unsigned NOT NULL,
  `Votes` int(4) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_answers` (`QuestionId`),
  KEY `FK_answersa` (`Author`),
  KEY `FK_lastrelateduser` (`LastRelatedUser`),
  CONSTRAINT `FK_answers` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`),
  CONSTRAINT `FK_answersa` FOREIGN KEY (`Author`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_lastrelateduser` FOREIGN KEY (`LastRelatedUser`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;

/*Table structure for table `questions` */

DROP TABLE IF EXISTS `questions`;

CREATE TABLE `questions` (
  `Body` varchar(10000) NOT NULL,
  `Author` int(4) unsigned NOT NULL,
  `Id` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `LastRelatedUser` int(4) unsigned NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `Votes` int(4) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `UpdateDate` (`UpdateDate`),
  KEY `FK_author` (`Author`),
  KEY `FK_last_related_user` (`LastRelatedUser`),
  CONSTRAINT `FK_author` FOREIGN KEY (`Author`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_last_related_user` FOREIGN KEY (`LastRelatedUser`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

/*Table structure for table `questiontags` */

DROP TABLE IF EXISTS `questiontags`;

CREATE TABLE `questiontags` (
  `TagId` int(4) NOT NULL,
  `QuestionId` int(4) NOT NULL,
  PRIMARY KEY (`TagId`,`QuestionId`),
  KEY `IX_Tag` (`TagId`),
  KEY `IX_Questions` (`QuestionId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Table structure for table `tags` */

DROP TABLE IF EXISTS `tags`;

CREATE TABLE `tags` (
  `TagId` int(4) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`TagId`),
  KEY `Name` (`Name`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `Id` int(4) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Website` varchar(300) DEFAULT NULL,
  `Reputation` int(4) NOT NULL,
  `SignupDate` datetime NOT NULL,
  `OpenId` varchar(500) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_OpenId` (`OpenId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Table structure for table `votesonanswers` */

DROP TABLE IF EXISTS `votesonanswers`;

CREATE TABLE `votesonanswers` (
  `UserId` int(4) unsigned NOT NULL,
  `PostId` int(4) unsigned NOT NULL,
  `Vote` tinyint(1) unsigned NOT NULL,
  PRIMARY KEY (`UserId`,`PostId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Table structure for table `votesonquestions` */

DROP TABLE IF EXISTS `votesonquestions`;

CREATE TABLE `votesonquestions` (
  `UserId` int(4) NOT NULL,
  `PostId` int(4) NOT NULL,
  `Vote` tinyint(1) NOT NULL,
  PRIMARY KEY (`UserId`,`PostId`),
  KEY `UserId` (`UserId`),
  KEY `QuestionId` (`PostId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
