-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_roles` (
  `crol_id`           INT            NOT NULL AUTO_INCREMENT,
  `capp_id`           INT            NOT NULL,
  `crol_name`         VARCHAR(32)    NOT NULL,
  `crol_descr`        VARCHAR(256)   NOT NULL,
  `crol_notes`        VARCHAR(1024)  NULL,
  PRIMARY KEY (`crol_id`),
  UNIQUE INDEX `uk_crvn_sec_roles` (`crol_name`(32) ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnsecroles_crvnsecapps`
    FOREIGN KEY (`capp_id`)
    REFERENCES `mydb`.`crvn_sec_apps` (`capp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);