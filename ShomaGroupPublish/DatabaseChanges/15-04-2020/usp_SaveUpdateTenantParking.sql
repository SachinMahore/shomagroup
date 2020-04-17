
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_SaveUpdateTenantParking
	@NumberOfParking INT,
	@UnitID INT,
	@TenantID BIGINT
AS
BEGIN
	DECLARE @TenantParking TABLE(ParkingID BIGINT, ParkingType INT, Charges MONEY)
	DECLARE @AdditionalParking INT=0
	DECLARE @AddParking INT=0
	DECLARE @DeleteParking INT=0
	DECLARE @IsAvailable INT=0
	DECLARE @ParkingID BIGINT=0

	INSERT INTO @TenantParking (ParkingID, ParkingType, Charges)
	SELECT P.ParkingID, P.Type, TP.Charges FROM tbl_TenantParking TP INNER JOIN tbl_Parking P ON TP.ParkingID=P.ParkingID WHERE TP.TenantID=@TenantID

	SET @AdditionalParking=ISNULL((SELECT COUNT(*) FROM @TenantParking WHERE ParkingType=2),0)

	IF(@NumberOfParking>0)
	BEGIN
		IF(@AdditionalParking=0)
		BEGIN
			SET @AddParking=@NumberOfParking;
			SET @IsAvailable=(SELECT COUNT(*) FROM tbl_Parking P WHERE ParkingID NOT IN (SELECT ParkingID FROM tbl_TenantParking) AND  P.Type=2 AND ISNULL(PropertyID,0)=0)
			IF(@IsAvailable=0)
			BEGIN
				SELECT '0|No additional parking available.|0|0' AS Result;
			END
			ELSE
			BEGIN
				IF(@IsAvailable>=@NumberOfParking)
				BEGIN
					IF(@NumberOfParking=1)
					BEGIN
						UPDATE TOP (1) tbl_Parking SET PropertyID=@UnitID WHERE PropertyID=0
						INSERT INTO tbl_TenantParking (ParkingID, TenantID, Charges, CreatedDate)
						SELECT ParkingID, @TenantID, Charges, GETDATE() FROM tbl_Parking WHERE Type=2 AND PropertyID=@UnitID
					END
					ELSE
					BEGIN
						UPDATE TOP (2) tbl_Parking SET PropertyID=@UnitID WHERE PropertyID=0
						INSERT INTO tbl_TenantParking (ParkingID, TenantID, Charges, CreatedDate)
						SELECT ParkingID, @TenantID, Charges, GETDATE() FROM tbl_Parking WHERE Type=2 AND PropertyID=@UnitID
					END
					SELECT '1|Progress saved|'+CONVERT(VARCHAR(50),@NumberOfParking)+'|'+CONVERT(VARCHAR(50),@NumberOfParking*100) AS Result
				END
				ELSE
				BEGIN
					SELECT '0|Only '+CONVERT(VARCHAR(50),(SELECT COUNT(*) FROM tbl_Parking P WHERE ParkingID NOT IN (SELECT ParkingID FROM tbl_TenantParking) AND  P.Type=2))+' parking remaining and you select '+CONVERT(VARCHAR(50),@NumberOfParking)+'. Please change the parking size.|0|0' AS Result;
				END
			END
		END
		ELSE
		BEGIN
			IF((@AdditionalParking-@NumberOfParking)>0)
			BEGIN
				SET @DeleteParking=@AdditionalParking-@NumberOfParking;
				SET @ParkingID=(SELECT TOP 1 TP.ParkingID FROM tbl_TenantParking TP INNER JOIN tbl_Parking P ON TP.ParkingID=P.ParkingID AND TenantID=@TenantID AND P.Type=2)
				DELETE FROM tbl_TenantParking WHERE ParkingID=@ParkingID
				UPDATE tbl_Parking SET PropertyID=0 WHERE ParkingID=@ParkingID
				SELECT '1|Progress saved|'+CONVERT(VARCHAR(50),@NumberOfParking)+'|'+CONVERT(VARCHAR(50),@NumberOfParking*100) AS Result
			END
			ELSE
			BEGIN
				SET @AddParking=@NumberOfParking-@AdditionalParking;
				SET @IsAvailable=(SELECT COUNT(*) FROM tbl_Parking P WHERE ParkingID NOT IN (SELECT ParkingID FROM tbl_TenantParking) AND  P.Type=2 AND ISNULL(PropertyID,0)=0)
				IF(@IsAvailable=0)
				BEGIN
					SELECT '0|No additional parking available.|0|0' AS Result;
				END
				ELSE
				BEGIN
					IF(@IsAvailable>=@AddParking)
					BEGIN
						IF(@AddParking>0)
						BEGIN
							IF(@AddParking=1)
							BEGIN
								UPDATE TOP (1) tbl_Parking SET PropertyID=@UnitID WHERE PropertyID=0
								INSERT INTO tbl_TenantParking (ParkingID, TenantID, Charges, CreatedDate)
								SELECT ParkingID, @TenantID, Charges, GETDATE() FROM tbl_Parking WHERE Type=2 AND PropertyID=@UnitID
							END
							ELSE
							BEGIN
								UPDATE TOP (2) tbl_Parking SET PropertyID=@UnitID WHERE PropertyID=0
								INSERT INTO tbl_TenantParking (ParkingID, TenantID, Charges, CreatedDate)
								SELECT ParkingID, @TenantID, Charges, GETDATE() FROM tbl_Parking WHERE Type=2 AND PropertyID=@UnitID
							END
							SELECT '1|Progress saved|'+CONVERT(VARCHAR(50),@NumberOfParking)+'|'+CONVERT(VARCHAR(50),@NumberOfParking*100) AS Result
						END
						ELSE
						BEGIN
							SELECT '1|Progress saved|'+CONVERT(VARCHAR(50),@NumberOfParking)+'|'+CONVERT(VARCHAR(50),@NumberOfParking*100) AS Result
						END
					END
					ELSE
					BEGIN
						SELECT '0|Only '+CONVERT(VARCHAR(50),(SELECT COUNT(*) FROM tbl_Parking P WHERE ParkingID NOT IN (SELECT ParkingID FROM tbl_TenantParking) AND  P.Type=2))+' parking remaining and you select '+CONVERT(VARCHAR(50),@NumberOfParking)+'. Please change the parking size.|0|0' AS Result;
					END
				END
			END
		END
	END
	ELSE
	BEGIN
		UPDATE tbl_Parking SET PropertyID=0 WHERE ParkingID IN (SELECT TP.ParkingID FROM tbl_TenantParking TP INNER JOIN tbl_Parking P ON TP.ParkingID=P.ParkingID AND TenantID=@TenantID AND P.Type=2)
		DELETE FROM tbl_TenantParking WHERE ParkingID IN (SELECT TP.ParkingID FROM tbl_TenantParking TP INNER JOIN tbl_Parking P ON TP.ParkingID=P.ParkingID AND TenantID=@TenantID AND P.Type=2)
		SELECT '1|Progress saved|0|0' AS Result
	END
END
GO
