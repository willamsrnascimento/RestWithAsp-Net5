DROP TABLE IF EXISTS `book`;
CREATE TABLE `book` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `author` varchar(80) NOT NULL,
  `launch_date` DATETIME NOT NULL,
  `price` DOUBLE NOT NULL,
  `title` varchar(80) NOT NULL,
  PRIMARY KEY (`id`)
) 
