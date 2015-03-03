-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE IF NOT EXISTS `mydb`.`crvn_sec_user_roles` (
  `cusr_id` INT NOT NULL,
  `crol_id` INT NOT NULL,
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