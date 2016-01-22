-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_log_settings` (
  `capp_id`              INT         NOT NULL,
  `clos_type`            VARCHAR(8)  NOT NULL,
  `clos_enabled`         BIT         NOT NULL,
  `clos_days`            SMALLINT    NOT NULL,
  `clos_max_entries`     INT         NOT NULL,
  PRIMARY KEY (`clos_type`, `capp_id`),
  CONSTRAINT `fk_crvnlogsettings_crvnsecapps`
    FOREIGN KEY (`capp_id`)
    REFERENCES `mydb`.`crvn_sec_apps` (`capp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);