-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_idn_scopes_id
BEFORE INSERT ON mydb.crvn_idn_scopes 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_scopes_id.nextval
    INTO :new.csco_id
    FROM DUAL;
END;
/