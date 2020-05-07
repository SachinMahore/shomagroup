GO

ALTER TABLE tbl_Properties DROP COLUMN BGCheckFees
GO
ALTER TABLE tbl_Properties ADD AppCCCheckFees MONEY, AppBGCheckFees MONEY, GuaCCCheckFees MONEY, GuaBGCheckFees MONEY

GO

UPDATE tbl_Properties SET ApplicationFees=100, AppCCCheckFees=35.00, AppBGCheckFees=65.00, GuarantorFees=150.00, GuaCCCheckFees=35.00, GuaBGCheckFees=115