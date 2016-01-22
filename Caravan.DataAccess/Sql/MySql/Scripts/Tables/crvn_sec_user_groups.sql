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