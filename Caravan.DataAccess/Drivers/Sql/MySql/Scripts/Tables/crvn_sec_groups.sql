-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_groups` (
  `cgrp_id`           INT            NOT NULL AUTO_INCREMENT,
  `capp_id`           INT            NOT NULL,
  `cgrp_name`         VARCHAR(32)    NOT NULL,
  `cgrp_descr`        VARCHAR(256)   NOT NULL,
  `cgrp_notes`        VARCHAR(1024)  NULL,
  PRIMARY KEY (`cgrp_id`),
  UNIQUE INDEX `uk_crvn_sec_groups` (`cgrp_name`(32) ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnsecgroups_crvnsecapps`
    FOREIGN KEY (`capp_id`)
    REFERENCES `mydb`.`crvn_sec_apps` (`capp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);