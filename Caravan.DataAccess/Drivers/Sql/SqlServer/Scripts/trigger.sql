CREATE TRIGGER dbo.caravan_log_id 
    ON dbo.caravan_log 
	INSTEAD OF INSERT 
AS 
	SELECT COALESCE()
	  FROM dbo.caravan_log
	 WHERE NEW
GO


