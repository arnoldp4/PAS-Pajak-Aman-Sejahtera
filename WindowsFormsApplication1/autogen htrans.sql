CREATE OR REPLACE FUNCTION generateHTrans
RETURN VARCHAR2 IS
	kode_akhir VARCHAR2(12);
	KODE NUMBER;
BEGIN
	SELECT NVL(MAX(SUBSTR(HT_ID,9,2)),0) INTO KODE 
	FROM H_TRANS
	WHERE HT_ID LIKE HT_ID || '%';
	IF KODE IS NULL THEN
		KODE := 1;
	ELSE 
		KODE := KODE + 1;
	END IF;
	kode_akhir := '0031000' || LPAD(KODE,3,'0');
	RETURN kode_akhir;
END;
/
show err;

DECLARE
  X varchar2(12);
BEGIN
  X := generateHTrans;
  Dbms_Output.put_line(X);
END;
/