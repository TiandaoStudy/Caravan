-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_sec_group_id
BEFORE INSERT ON mydb.caravan_sec_group 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(cgrp_id), -1) + 1
    INTO :new.cgrp_id
    FROM mydb.caravan_sec_group
   WHERE capp_id = :new.capp_id;
END;