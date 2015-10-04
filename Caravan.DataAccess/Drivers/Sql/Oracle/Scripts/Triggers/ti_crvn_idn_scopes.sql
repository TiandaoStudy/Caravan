-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_scopes
BEFORE INSERT ON mydb.crvn_idn_scopes 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_scopes_id.nextval
    INTO :new.CSCO_ID
    FROM DUAL;
END;
/