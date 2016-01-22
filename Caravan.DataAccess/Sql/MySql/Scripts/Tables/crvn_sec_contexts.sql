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