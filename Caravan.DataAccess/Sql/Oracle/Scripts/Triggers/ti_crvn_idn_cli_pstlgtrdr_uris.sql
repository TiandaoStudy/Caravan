-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_cli_pstlgtrdr_uris
BEFORE INSERT ON mydb.crvn_idn_cli_pstlgtrdr_uris 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_cli_pstlgtrdr_uris.nextval
    INTO :new.CPLR_ID
    FROM DUAL;
END;
/