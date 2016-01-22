-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_log_entries` (
  `clog_id`            BIGINT          NOT NULL AUTO_INCREMENT,
  `capp_id`            INT             NOT NULL,
  `clos_type`          VARCHAR(8)      NOT NULL,
  `clog_date`          TIMESTAMP       NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cusr_login`         VARCHAR(32)     NULL,
  `clog_code_unit`     VARCHAR(256)    NOT NULL,
  `clog_function`      VARCHAR(256)    NOT NULL,
  `clog_short_msg`     VARCHAR(256)    NOT NULL,
  `clog_long_msg`      TEXT            NULL,
  `clog_context`       VARCHAR(256)    NULL,
  `clog_key_0`         VARCHAR(32)     NULL,
  `clog_value_0`       VARCHAR(1024)   NULL,
  `clog_key_1`         VARCHAR(32)     NULL,
  `clog_value_1`       VARCHAR(1024)   NULL,
  `clog_key_2`         VARCHAR(32)     NULL,
  `clog_value_2`       VARCHAR(1024)   NULL,
  `clog_key_3`         VARCHAR(32)     NULL,
  `clog_value_3`       VARCHAR(1024)   NULL,
  `clog_key_4`         VARCHAR(32)     NULL,
  `clog_value_4`       VARCHAR(1024)   NULL,
  `clog_key_5`         VARCHAR(32)     NULL,
  `clog_value_5`       VARCHAR(1024)   NULL,
  `clog_key_6`         VARCHAR(32)     NULL,
  `clog_value_6`       VARCHAR(1024)   NULL,
  `clog_key_7`         VARCHAR(32)     NULL,
  `clog_value_7`       VARCHAR(1024)   NULL,
  `clog_key_8`         VARCHAR(32)     NULL,
  `clog_value_8`       VARCHAR(1024)   NULL,
  `clog_key_9`         VARCHAR(32)     NULL,
  `clog_value_9`       VARCHAR(1024)   NULL,
  PRIMARY KEY (`clog_id`),
  INDEX `ix_crvn_log_date` (`clog_date` DESC, `capp_id` ASC),
  INDEX `ix_crvn_log_type` (`clos_type` ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnlog_crvnlogsettings`
    FOREIGN KEY (`capp_id`, `clos_type`)
    REFERENCES `mydb`.`crvn_log_settings` (`capp_id`, `clos_type`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);