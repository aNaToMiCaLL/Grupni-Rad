/*
Navicat MySQL Data Transfer

Source Server         : Grupa
Source Server Version : 50018
Source Host           : localhost:3306
Source Database       : prodavnica

Target Server Type    : MYSQL
Target Server Version : 50018
File Encoding         : 65001

Date: 2022-05-02 08:46:38
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `artikal`
-- ----------------------------
DROP TABLE IF EXISTS `artikal`;
CREATE TABLE `artikal` (
  `artikal_id` int(11) NOT NULL auto_increment,
  `naziv_artikla` varchar(40) default NULL,
  `vrsta_artikla` varchar(40) default NULL,
  `cijena` double default NULL,
  PRIMARY KEY  (`artikal_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of artikal
-- ----------------------------
INSERT INTO `artikal` VALUES ('1', 'Samsung A22', 'Telefon', '450');
INSERT INTO `artikal` VALUES ('2', 'Samsung A50', 'Telefon', '260');
INSERT INTO `artikal` VALUES ('3', 'Playstation 4', 'Konzola', '500');
INSERT INTO `artikal` VALUES ('4', 'GOD OF WAR 4', 'Igrica', '30');
INSERT INTO `artikal` VALUES ('5', 'Laptop HP EliteBook', 'Laptop', '780');
INSERT INTO `artikal` VALUES ('6', 'Laptop Asus Zenbook', 'Laptop', '800');

-- ----------------------------
-- Table structure for `kupac`
-- ----------------------------
DROP TABLE IF EXISTS `kupac`;
CREATE TABLE `kupac` (
  `kupac_id` int(11) NOT NULL auto_increment,
  `ime` varchar(40) default NULL,
  `prezime` varchar(40) default NULL,
  `grad` varchar(40) default NULL,
  `adresa` varchar(40) default NULL,
  `telefon` varchar(40) default NULL,
  `username` varchar(40) default NULL,
  `password` varchar(40) default NULL,
  PRIMARY KEY  (`kupac_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of kupac
-- ----------------------------
INSERT INTO `kupac` VALUES ('1', 'Arman', 'Bašovic', 'Sarajevo', 'Poturšahidijina 10', '0603033933', 'arman.basovic', 'arman1234');
INSERT INTO `kupac` VALUES ('2', 'Velid', 'Madžak', 'Tarcin', 'Tarcin', '061023923', 'velidmadzak', 'velid3281');
INSERT INTO `kupac` VALUES ('3', 'Amer', 'Dautovic', 'Sarajevo', 'Safeta Hadžica 32', '060545232', 'Amer.d', 'amer4321');
INSERT INTO `kupac` VALUES ('4', 'Faris', 'Mujezin', 'Sarajevo', 'Sabita Užicanina 33', '064433284', 'faris.m', 'faris1234');

-- ----------------------------
-- Table structure for `narudzbenica`
-- ----------------------------
DROP TABLE IF EXISTS `narudzbenica`;
CREATE TABLE `narudzbenica` (
  `narudzbenica_id` int(11) NOT NULL auto_increment,
  `kupac_id` int(30) default NULL,
  `datum_narudzbe` date default NULL,
  PRIMARY KEY  (`narudzbenica_id`),
  KEY `kupac_id` (`kupac_id`),
  CONSTRAINT `narudzbenica_ibfk_1` FOREIGN KEY (`kupac_id`) REFERENCES `kupac` (`kupac_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of narudzbenica
-- ----------------------------
INSERT INTO `narudzbenica` VALUES ('3', '2', '2022-05-02');
INSERT INTO `narudzbenica` VALUES ('4', '3', '2022-05-02');
INSERT INTO `narudzbenica` VALUES ('5', '3', '2022-05-02');
INSERT INTO `narudzbenica` VALUES ('6', '2', '2022-05-02');

-- ----------------------------
-- Table structure for `skladiste`
-- ----------------------------
DROP TABLE IF EXISTS `skladiste`;
CREATE TABLE `skladiste` (
  `id` int(11) NOT NULL auto_increment,
  `artikal_id` int(11) default NULL,
  `kolicina_stanje` int(11) default NULL,
  PRIMARY KEY  (`id`),
  KEY `artikal_id` (`artikal_id`),
  CONSTRAINT `skladiste_ibfk_1` FOREIGN KEY (`artikal_id`) REFERENCES `artikal` (`artikal_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of skladiste
-- ----------------------------
INSERT INTO `skladiste` VALUES ('1', '1', '49');
INSERT INTO `skladiste` VALUES ('2', '2', '45');
INSERT INTO `skladiste` VALUES ('3', '3', '29');
INSERT INTO `skladiste` VALUES ('4', '4', '75');
INSERT INTO `skladiste` VALUES ('5', '5', '18');
INSERT INTO `skladiste` VALUES ('6', '6', '20');

-- ----------------------------
-- Table structure for `stavka_narudzbenice`
-- ----------------------------
DROP TABLE IF EXISTS `stavka_narudzbenice`;
CREATE TABLE `stavka_narudzbenice` (
  `stavka_id` int(11) NOT NULL auto_increment,
  `narudzbenica_id` int(11) default NULL,
  `artikal_id` int(11) default NULL,
  `kolicina` int(11) default NULL,
  PRIMARY KEY  (`stavka_id`),
  KEY `artikal_id` (`artikal_id`),
  KEY `stavka_narudzbenice_ibfk_1` (`narudzbenica_id`),
  CONSTRAINT `stavka_narudzbenice_ibfk_1` FOREIGN KEY (`narudzbenica_id`) REFERENCES `narudzbenica` (`narudzbenica_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `stavka_narudzbenice_ibfk_2` FOREIGN KEY (`artikal_id`) REFERENCES `artikal` (`artikal_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of stavka_narudzbenice
-- ----------------------------
INSERT INTO `stavka_narudzbenice` VALUES ('14', '3', '1', '3');
INSERT INTO `stavka_narudzbenice` VALUES ('53', '3', '3', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('58', '3', '5', '2');
INSERT INTO `stavka_narudzbenice` VALUES ('62', '4', '3', '1');
INSERT INTO `stavka_narudzbenice` VALUES ('64', '5', '3', '1');
INSERT INTO `stavka_narudzbenice` VALUES ('66', '6', '1', '2');
