-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_apps` (
  `capp_id`       INT            NOT NULL AUTO_INCREMENT,
  `capp_name`     VARCHAR(32)    NOT NULL,
  `capp_descr`    VARCHAR(256)   NOT NULL,
  PRIMARY KEY (`capp_id`),
  UNIQUE INDEX `uk_crvn_sec_apps` (`capp_name`(32) ASC));