/*
SQLyog Enterprise - MySQL GUI v7.02 
MySQL - 5.1.41-community : Database - test
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`test` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `test`;

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
  PRIMARY KEY (`Id`),
  KEY `FK_answers` (`QuestionId`),
  KEY `FK_answersa` (`Author`),
  KEY `FK_lastrelateduser` (`LastRelatedUser`),
  CONSTRAINT `FK_lastrelateduser` FOREIGN KEY (`LastRelatedUser`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_answers` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`),
  CONSTRAINT `FK_answersa` FOREIGN KEY (`Author`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

/*Data for the table `answers` */

insert  into `answers`(`Id`,`QuestionId`,`Body`,`UpdateDate`,`LastRelatedUser`,`CreatedDate`,`Author`) values (1,1,'Answer # 0','2010-01-22 17:30:33',1,'2010-01-22 17:30:33',1),(2,1,'Answer # 1','2010-01-22 17:30:33',1,'2010-01-22 17:30:33',1),(3,1,'Answer # 2','2010-01-22 17:30:34',1,'2010-01-22 17:30:34',1),(4,1,'Answer # 3','2010-01-22 17:30:34',1,'2010-01-22 17:30:34',1),(5,1,'Answer # 4','2010-01-22 17:30:34',1,'2010-01-22 17:30:34',1);

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
  PRIMARY KEY (`Id`),
  KEY `UpdateDate` (`UpdateDate`),
  KEY `FK_author` (`Author`),
  KEY `FK_last_related_user` (`LastRelatedUser`),
  CONSTRAINT `FK_author` FOREIGN KEY (`Author`) REFERENCES `users` (`Id`),
  CONSTRAINT `FK_last_related_user` FOREIGN KEY (`LastRelatedUser`) REFERENCES `users` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `questions` */

insert  into `questions`(`Body`,`Author`,`Id`,`Title`,`UpdateDate`,`LastRelatedUser`,`CreatedDate`) values ('Well, is there?',1,1,'Is there a god?','2010-01-22 17:30:33',1,'2010-01-22 17:30:33');

/*Table structure for table `questiontags` */

DROP TABLE IF EXISTS `questiontags`;

CREATE TABLE `questiontags` (
  `TagId` int(4) NOT NULL,
  `QuestionId` int(4) NOT NULL,
  PRIMARY KEY (`TagId`,`QuestionId`),
  KEY `IX_Tag` (`TagId`),
  KEY `IX_Questions` (`QuestionId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `questiontags` */

/*Table structure for table `tags` */

DROP TABLE IF EXISTS `tags`;

CREATE TABLE `tags` (
  `TagId` int(4) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`TagId`),
  KEY `Name` (`Name`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `tags` */

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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

/*Data for the table `users` */

insert  into `users`(`Id`,`Name`,`Website`,`Reputation`,`SignupDate`,`OpenId`) values (1,'User0',NULL,1,'2010-01-22 17:30:33','OpenId0'),(2,'User1',NULL,1,'2010-01-22 17:30:33','OpenId1'),(3,'User2',NULL,1,'2010-01-22 17:30:34','OpenId2'),(4,'User3',NULL,1,'2010-01-22 17:30:34','OpenId3'),(5,'User4',NULL,1,'2010-01-22 17:30:34','OpenId4'),(6,'User5',NULL,1,'2010-01-22 17:30:34','OpenId5'),(7,'User6',NULL,1,'2010-01-22 17:30:34','OpenId6'),(8,'User7',NULL,1,'2010-01-22 17:30:34','OpenId7'),(9,'User8',NULL,1,'2010-01-22 17:30:34','OpenId8'),(10,'User9',NULL,1,'2010-01-22 17:30:34','OpenId9'),(11,'User10',NULL,1,'2010-01-22 17:30:34','OpenId10');

/*Table structure for table `votesonanswers` */

DROP TABLE IF EXISTS `votesonanswers`;

CREATE TABLE `votesonanswers` (
  `UserId` int(4) unsigned NOT NULL,
  `PostId` int(4) unsigned NOT NULL,
  `Vote` tinyint(1) unsigned NOT NULL,
  PRIMARY KEY (`UserId`,`PostId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*Data for the table `votesonanswers` */

insert  into `votesonanswers`(`UserId`,`PostId`,`Vote`) values (2,2,0),(3,3,0),(4,3,0),(5,4,0),(6,4,0),(7,4,0),(8,5,0),(9,5,0),(10,5,0),(11,5,0);

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

/*Data for the table `votesonquestions` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
