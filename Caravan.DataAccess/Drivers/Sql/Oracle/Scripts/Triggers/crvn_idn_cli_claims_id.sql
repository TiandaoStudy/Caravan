﻿-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_idn_cli_claims_id
BEFORE INSERT ON mydb.crvn_idn_cli_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_cli_claims_id.nextval
    INTO :new.CCLM_ID
    FROM DUAL;
END;
/