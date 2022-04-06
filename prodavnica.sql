/*
Navicat MySQL Data Transfer

Source Server         : Grupa
Source Server Version : 50018
Source Host           : localhost:3306
Source Database       : prodavnica

Target Server Type    : MYSQL
Target Server Version : 50018
File Encoding         : 65001

Date: 2022-04-06 07:39:05
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `artikal`
-- ----------------------------
DROP TABLE IF EXISTS `artikal`;
CREATE TABLE `artikal` (
  `artikal_id` int(11) NOT NULL auto_increment,
  `naziv_artikla` varchar(255) default NULL,
  `vrsta_artikla` varchar(255) default NULL,
  `cijena` double default NULL,
  PRIMARY KEY  (`artikal_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of artikal
-- ----------------------------
INSERT INTO `artikal` VALUES ('1', 'Samsung A22', 'Telefon', '450');
INSERT INTO `artikal` VALUES ('2', 'Samsung A50', 'Telefon', '260');
INSERT INTO `artikal` VALUES ('3', 'Playstation 4', 'Konzola', '500');
INSERT INTO `artikal` VALUES ('4', 'GOD OF WAR 4', 'Igrica', '30');
INSERT INTO `artikal` VALUES ('5', 'Laptop HP EliteBook', 'Laptop', '780');

-- ----------------------------
-- Table structure for `kupac`
-- ----------------------------
DROP TABLE IF EXISTS `kupac`;
CREATE TABLE `kupac` (
  `kupac_id` int(11) NOT NULL auto_increment,
  `ime` varchar(255) default NULL,
  `prezime` varchar(255) default NULL,
  `grad` varchar(255) default NULL,
  `adresa` varchar(255) default NULL,
  `telefon` varchar(255) default NULL,
  `user` varchar(255) default NULL,
  `pass` varchar(255) default NULL,
  PRIMARY KEY  (`kupac_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of kupac
-- ----------------------------
INSERT INTO `kupac` VALUES ('1', 'Arman', 'Basovic', 'Sarajevo', 'Poturšahidijina 10', '0603033933', 'arman.basovic', 'arman1234');
INSERT INTO `kupac` VALUES ('2', 'Bakir', 'Kustura', 'Sarajevo', 'Most Spasa 33', '061630723', 'bakir_.kustura', 'bakir3322');
INSERT INTO `kupac` VALUES ('3', 'Velid', 'Madžak', 'Tarcin', 'Huseina Kapetana Gradašcevica 10', '0603208915', 'velidmadzak', 'velid3281');
INSERT INTO `kupac` VALUES ('4', 'Tarik', 'Kukuljac', 'Sarajevo', 'Nahorevska 248', '060842069', 'tar33k', 'tarik.33');

-- ----------------------------
-- Table structure for `narudzbenica`
-- ----------------------------
DROP TABLE IF EXISTS `narudzbenica`;
CREATE TABLE `narudzbenica` (
  `narudzbenica_id` int(11) NOT NULL auto_increment,
  `kupac_id` int(11) default NULL,
  `datum_narudzbe` date default NULL,
  PRIMARY KEY  (`narudzbenica_id`),
  KEY `kupac_id` (`kupac_id`),
  CONSTRAINT `narudzbenica_ibfk_1` FOREIGN KEY (`kupac_id`) REFERENCES `kupac` (`kupac_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of narudzbenica
-- ----------------------------
INSERT INTO `narudzbenica` VALUES ('1', '1', '2022-03-09');
INSERT INTO `narudzbenica` VALUES ('2', '2', '2022-02-23');
INSERT INTO `narudzbenica` VALUES ('3', '3', '2021-06-03');
INSERT INTO `narudzbenica` VALUES ('5', null, '2022-04-02');
INSERT INTO `narudzbenica` VALUES ('6', null, '2022-04-02');
INSERT INTO `narudzbenica` VALUES ('8', null, '2022-04-04');
INSERT INTO `narudzbenica` VALUES ('9', null, '2022-04-04');
INSERT INTO `narudzbenica` VALUES ('11', null, '2022-04-04');
INSERT INTO `narudzbenica` VALUES ('15', null, '2022-04-04');
INSERT INTO `narudzbenica` VALUES ('16', null, '2022-04-04');
INSERT INTO `narudzbenica` VALUES ('17', null, '2022-04-04');

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
  CONSTRAINT `skladiste_ibfk_1` FOREIGN KEY (`artikal_id`) REFERENCES `artikal` (`artikal_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of skladiste
-- ----------------------------
INSERT INTO `skladiste` VALUES ('1', '1', '34');
INSERT INTO `skladiste` VALUES ('2', '2', '38');
INSERT INTO `skladiste` VALUES ('3', '3', '107');
INSERT INTO `skladiste` VALUES ('4', '4', '147');
INSERT INTO `skladiste` VALUES ('5', '5', '99');

-- ----------------------------
-- Table structure for `stavka_narudzbenice`
-- ----------------------------
DROP TABLE IF EXISTS `stavka_narudzbenice`;
CREATE TABLE `stavka_narudzbenice` (
  `stavka_id` int(11) NOT NULL auto_increment,
  `narudzebnica_id` int(11) default NULL,
  `artikal_id` int(11) default NULL,
  `kolicina` int(11) default NULL,
  PRIMARY KEY  (`stavka_id`),
  KEY `narudzebnica_id` (`narudzebnica_id`),
  KEY `artikal_id` (`artikal_id`),
  CONSTRAINT `stavka_narudzbenice_ibfk_1` FOREIGN KEY (`artikal_id`) REFERENCES `artikal` (`artikal_id`),
  CONSTRAINT `stavka_narudzbenice_ibfk_2` FOREIGN KEY (`narudzebnica_id`) REFERENCES `narudzbenica` (`narudzbenica_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of stavka_narudzbenice
-- ----------------------------
INSERT INTO `stavka_narudzbenice` VALUES ('4', '3', '5', '2');
INSERT INTO `stavka_narudzbenice` VALUES ('55', '5', '2', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('56', '5', '3', '2');
INSERT INTO `stavka_narudzbenice` VALUES ('57', '5', '1', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('134', '8', '3', '2');
INSERT INTO `stavka_narudzbenice` VALUES ('199', '8', '2', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('202', '8', '4', '2');
INSERT INTO `stavka_narudzbenice` VALUES ('206', '8', '5', '5');
INSERT INTO `stavka_narudzbenice` VALUES ('224', '9', '1', '5');
INSERT INTO `stavka_narudzbenice` VALUES ('227', '9', '2', '3');
INSERT INTO `stavka_narudzbenice` VALUES ('229', '11', '1', '3');
INSERT INTO `stavka_narudzbenice` VALUES ('231', '15', '1', '1');
INSERT INTO `stavka_narudzbenice` VALUES ('233', '16', '1', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('235', '17', '1', '1');
INSERT INTO `stavka_narudzbenice` VALUES ('237', '17', '2', '4');
INSERT INTO `stavka_narudzbenice` VALUES ('239', '17', '3', '4');
