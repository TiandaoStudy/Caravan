-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_pstlgtrdr_uris
BEFORE INSERT ON mydb.crvn_idn_cli_pstlgtrdr_uris 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_cli_pstlgtrdr_uris_id.nextval
    INTO :new.CCPR_ID
    FROM DUAL;
END;
/