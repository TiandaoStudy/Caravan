-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_users` (
  `cusr_id`         INT            NOT NULL AUTO_INCREMENT,
  `capp_id`         INT            NOT NULL,
  `cusr_login`      VARCHAR(32)    NOT NULL,
  `cusr_hashed_pwd` VARCHAR(256)   NULL,
  `cusr_active`     TINYINT        NOT NULL,
  `cusr_first_name` VARCHAR(256)   NULL,
  `cusr_last_name`  VARCHAR(256)   NULL,
  `cusr_email`      VARCHAR(256)   NULL,
  PRIMARY KEY (`cusr_id`),
  UNIQUE INDEX `uk_crvn_sec_users` (`cusr_login`(32) ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnsecusers_crvnsecapps`
    FOREIGN KEY (`capp_id`)
    REFERENCES `mydb`.`crvn_sec_apps` (`capp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);