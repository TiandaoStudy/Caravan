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