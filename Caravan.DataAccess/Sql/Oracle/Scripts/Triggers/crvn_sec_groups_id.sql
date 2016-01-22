-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_groups_id
BEFORE INSERT ON mydb.crvn_sec_groups 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_groups_id.nextval
    INTO :new.cgrp_id
    FROM DUAL;
END;
/