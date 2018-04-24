-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: repulo
-- ------------------------------------------------------
-- Server version	5.7.21-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `jaratok`
--

DROP TABLE IF EXISTS `jaratok`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `jaratok` (
  `idjaratok` int(11) NOT NULL AUTO_INCREMENT,
  `honnan` varchar(60) NOT NULL,
  `hova` varchar(60) NOT NULL,
  `tavolsag` int(11) NOT NULL,
  `felszall` varchar(5) NOT NULL,
  `leszall` varchar(5) NOT NULL,
  `tarsasag` varchar(45) NOT NULL,
  PRIMARY KEY (`idjaratok`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jaratok`
--

LOCK TABLES `jaratok` WRITE;
/*!40000 ALTER TABLE `jaratok` DISABLE KEYS */;
INSERT INTO `jaratok` VALUES (1,'Budapest','London',1500,'15:00','18:30','WizzAir'),(2,'Budapest','Lisszabon',2000,'16:00','19:20','TP'),(3,'Lisszabon','Boston',4000,'05:00','13:00','TP'),(4,'Boston','San Francisco',3700,'16:00','22:30','Lufthansa'),(5,'Boston','London',5000,'14:00','22:00','WOW Air'),(6,'Prága','Budapest',525,'08:00','9:30','WizzAir'),(7,'New York','Los Angeles',4000,'09:00','15:00','USAir'),(8,'Los Angeles','Budapest',5000,'16:00','22:00','USAir'),(9,'Lisszabon','Budapest',1500,'06:00','08:00','Lufthansa'),(10,'Budapest','London',1500,'09:00','11:00','Lufthansa'),(11,'Lisszabon','London',3000,'07:00','11:00','WizzAir'),(12,'London','Budapest',3000,'22:00','03:30','WizzAir'),(13,'Bukarest','Budapest',700,'12:00','13:00','TP'),(14,'Budapest','Minszk',930,'14:00','15:10','USAir');
/*!40000 ALTER TABLE `jaratok` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-04-24 21:11:47
