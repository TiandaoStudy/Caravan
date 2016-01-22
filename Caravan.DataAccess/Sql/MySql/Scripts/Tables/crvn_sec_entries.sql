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