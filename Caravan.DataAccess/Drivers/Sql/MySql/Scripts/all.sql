
-- Apps
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_apps` (
  `capp_id`       INT            NOT NULL AUTO_INCREMENT,
  `capp_name`     VARCHAR(32)    NOT NULL,
  `capp_descr`    VARCHAR(256)   NOT NULL,
  PRIMARY KEY (`capp_id`),
  UNIQUE INDEX `uk_crvn_sec_apps` (`capp_name`(32) ASC));

-- Log Settings
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

-- Log Entries
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
  `clog_long_msg`      TEXT            NULL,
  PRIMARY KEY (`clog_id`),
  INDEX `ix_crvn_log_date` (`clog_date` DESC, `capp_id` ASC),
  INDEX `ix_crvn_log_type` (`clos_type` ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnlog_crvnlogsettings`
    FOREIGN KEY (`capp_id`, `clos_type`)
    REFERENCES `mydb`.`crvn_log_settings` (`capp_id`, `clos_type`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- Users
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_users` (
  `cusr_id`         BIGINT         NOT NULL AUTO_INCREMENT,
  `capp_id`         INT            NOT NULL,
  `cusr_login`      VARCHAR(32)    NOT NULL,
  `cusr_hashed_pwd` VARCHAR(256)   NULL,
  `cusr_active`     BIT            NOT NULL,
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

-- Groups
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

-- Roles
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

-- User Groups
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_user_groups` (
  `cusr_id` BIGINT    NOT NULL,
  `cgrp_id` INT       NOT NULL,
  PRIMARY KEY (`cusr_id`, `cgrp_id`),
  CONSTRAINT `fk_crvnsecusrgrp_crvnsecusr`
    FOREIGN KEY (`cusr_id`)
    REFERENCES `mydb`.`crvn_sec_users` (`cusr_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_crvnsecusrgrp_crvnsecgrp`
    FOREIGN KEY (`cgrp_id`)
    REFERENCES `mydb`.`crvn_sec_groups` (`cgrp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- User Roles
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_user_roles` (
  `cusr_id` BIGINT   NOT NULL,
  `crol_id` INT      NOT NULL,
  PRIMARY KEY (`cusr_id`, `crol_id`),
  CONSTRAINT `fk_crvnsecusrrol_crvnsecusr`
    FOREIGN KEY (`cusr_id`)
    REFERENCES `mydb`.`crvn_sec_users` (`cusr_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_crvnsecusrrol_crvnsecrol`
    FOREIGN KEY (`crol_id`)
    REFERENCES `mydb`.`crvn_sec_roles` (`crol_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- Contexts
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_contexts` (
  `cctx_id`       INT             NOT NULL AUTO_INCREMENT,
  `capp_id`       INT             NOT NULL,
  `cctx_name`     VARCHAR(256)    NOT NULL,
  `cctx_descr`    VARCHAR(256)    NOT NULL,
  PRIMARY KEY (`cctx_id`),
  UNIQUE INDEX `uk_crvn_sec_contexts` (`cctx_name`(32) ASC, `capp_id` ASC),
  CONSTRAINT `fk_crvnsecctxs_crvnsecapps`
    FOREIGN KEY (`capp_id`)
    REFERENCES `mydb`.`crvn_sec_apps` (`capp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- Objects
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_objects` (
  `cobj_id`           INT             NOT NULL AUTO_INCREMENT,
  `cctx_id`           INT             NOT NULL,
  `cobj_name`         VARCHAR(32)     NOT NULL,
  `cobj_descr`        VARCHAR(256)    NOT NULL,
  `cobj_type`         VARCHAR(8)      NOT NULL,
  PRIMARY KEY (`cobj_id`),
  UNIQUE INDEX `uk_crvn_sec_objects` (`cobj_name`(32) ASC, `cctx_id` ASC),
  CONSTRAINT `fk_crvnsecobjs_crvnsecctxs`
    FOREIGN KEY (`cctx_id`)
    REFERENCES `mydb`.`crvn_sec_contexts` (`cctx_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- Sec Entries
-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_entries` (
  `csec_id`   BIGINT   NOT NULL AUTO_INCREMENT,
  `cobj_id`   INT      NOT NULL,
  `cusr_id`   BIGINT   NULL, -- Might be null, either user, group or role
  `cgrp_id`   INT      NULL, -- Might be null, either user, group or role
  `crol_id`   INT      NULL, -- Might be null, either user, group or role
  PRIMARY KEY (`csec_id`),
  CONSTRAINT `fk_crvnsec_crvnsecobj`
    FOREIGN KEY (`cobj_id`)
    REFERENCES `mydb`.`crvn_sec_objects` (`cobj_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_crvnsec_crvnsecusr`
    FOREIGN KEY (`cusr_id`)
    REFERENCES `mydb`.`crvn_sec_users` (`cusr_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_crvnsec_crvnsecgrp`
    FOREIGN KEY (`cgrp_id`)
    REFERENCES `mydb`.`crvn_sec_groups` (`cgrp_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_crvnsec_crvnsecrol`
    FOREIGN KEY (`crol_id`)
    REFERENCES `mydb`.`crvn_sec_roles` (`crol_id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);