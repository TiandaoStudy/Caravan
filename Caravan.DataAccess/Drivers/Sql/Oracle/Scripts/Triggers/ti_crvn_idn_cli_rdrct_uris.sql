﻿-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_rdrct_uris
BEFORE INSERT ON mydb.crvn_idn_cli_rdrct_uris 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_cli_rdrct_uris_id.nextval
    INTO :new.CCRU_ID
    FROM DUAL;
END;
/