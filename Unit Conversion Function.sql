ALTER FUNCTION dbo.ufn_ConvertUnit
(
	@UnitFrom int,
	@UnitTo int,
	@UnitValue numeric(12,5)
)
RETURNS numeric(12,5)
WITH SCHEMABINDING
BEGIN
	DECLARE @result numeric(12,5)=@UnitValue

	-- Operator, Factor
	IF EXISTS(SELECT TOP 1 'A' FROM dbo.UnitConversion WITH(NOLOCK) WHERE FromUnitId=@UnitFrom AND ToUnitId=@UnitTo)
		SELECT @result = 
		CASE Operator
		WHEN '*' THEN (@result * Factor)
		WHEN '/' THEN (@result / Factor)
		WHEN '+' THEN (@result + Factor)
		WHEN '-' THEN (@result - Factor)
		END
		FROM dbo.UnitConversion WITH(NOLOCK)
		WHERE FromUnitId=@UnitFrom AND ToUnitId=@UnitTo
		ORDER BY CalcStep
	ELSE IF EXISTS(SELECT TOP 1 'A' FROM dbo.UnitConversion WITH(NOLOCK) WHERE FromUnitId=@UnitTo AND ToUnitId=@UnitFrom)
		SELECT @result = 
		CASE Operator 
		WHEN '*' THEN (@result / Factor)
		WHEN '/' THEN (@result * Factor)
		WHEN '+' THEN (@result - Factor)
		WHEN '-' THEN (@result + Factor)
		END
		FROM dbo.UnitConversion WITH(NOLOCK)
		WHERE FromUnitId=@UnitTo AND ToUnitId=@UnitFrom
		ORDER BY CalcStep desc
	ELSE
		SET @result=null

	RETURN @result;
END
go

--drop function dbo.ufn_GetChildUnit
--ALTER FUNCTION dbo.ufn_GetChildUnit
--(
--	@UnitId int
--)
--RETURNS int
--AS
--BEGIN
--	DECLARE @ChildUnitId int
--	SELECT @ChildUnitId=ToUnitId FROM UnitConversion WHERE FromUnitId=@UnitId
	
--	IF @@ROWCOUNT > 0
--		SELECT @ChildUnitId=dbo.ufn_GetChildUnit(@ChildUnitId)
--	ELSE
--		SELECT @ChildUnitId=@UnitId

--	RETURN @ChildUnitId
--END
--go

--drop procedure usp_GetChildUnit

ALTER FUNCTION dbo.ufn_GetChildUnit
(
	@UnitId int
)
RETURNS int
AS
BEGIN
	DECLARE @ChildUnitId int

	;WITH Unit_cte AS
	(
		SELECT FromUnitId, ToUnitId, [Level]=1
		FROM UnitConversion
		WHERE FromUnitId=@UnitId
		UNION ALL
		SELECT u.FromUnitId, u.ToUnitId, [Level]+1
		FROM UnitConversion u
		INNER JOIN Unit_cte ucte ON ucte.ToUnitId=u.FromUnitId
	)
	SELECT TOP 1 @ChildUnitId=ToUnitId  
	FROM Unit_cte
	ORDER BY [Level] DESC

	RETURN @ChildUnitId
END
go

--EXEC usp_GetChildUnit 9
SELECT dbo.ufn_GetChildUnit(9)

SELECT * FROM Inventory
SELECT * FROM InvoiceItem
go

select * from Inventory
go

-- EXEC usp_SaveInventory 'MOUSE', 5, 2, 3
ALTER PROCEDURE usp_SaveInventory
(
	@Code nvarchar(15)
	,@ProductId int
	,@Quantity money
	,@UnitId int
	,@Description nvarchar(255) = NULL
	,@ExpiryDate date = NULL
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @NewQty money
	DECLARE @NewUnitId int

	SELECT @NewUnitId = dbo.ufn_GetChildUnit(@UnitId)
	SELECT @NewQty = dbo.ufn_ConvertUnit(@UnitId, @NewUnitId, ABS(@Quantity))

	IF NOT EXISTS(SELECT TOP 1 'A' FROM Inventory WITH(NOLOCK) WHERE Code=@Code AND ProductId=@ProductId AND UnitId=@NewUnitId)
		INSERT INTO Inventory(Code, ProductId, Quantity, UnitId, [Description], ExpiryDate)
		VALUES(@Code, @ProductId, @NewQty, @NewUnitId, @Description, @ExpiryDate)
	ELSE
	BEGIN
		-- Already exists with different unit id
		DECLARE @dbQty money
		DECLARE @dbUnitId int
		SELECT TOP 1 @dbQty=Quantity, @dbUnitId=UnitId FROM Inventory WITH(NOLOCK) WHERE Code=@Code AND ProductId=@ProductId
		
		IF @dbUnitId <> @NewUnitId
		BEGIN
			DECLARE @NewDbUnitId int
			DECLARE @NewDbQty money
			SELECT @NewDbUnitId = dbo.ufn_GetChildUnit(@dbUnitId)
			SELECT @NewDbQty = dbo.ufn_ConvertUnit(@dbUnitId, @NewUnitId, ABS(@dbQty))
			UPDATE Inventory WITH(ROWLOCK) SET Quantity=@NewDbQty, UnitId=@NewDbUnitId WHERE Code=@Code AND ProductId=@ProductId AND UnitId=@dbUnitId
		END

		-- Common update
		UPDATE Inventory WITH(ROWLOCK) SET Quantity += @NewQty, UnitId=@NewUnitId,
		[Description]=CASE WHEN @Description IS NULL THEN [Description] ELSE @Description END,
		ExpiryDate=CASE WHEN @ExpiryDate IS NULL THEN ExpiryDate ELSE @ExpiryDate END
		WHERE Code=@Code AND ProductId=@ProductId AND UnitId=@NewUnitId
	END

	SET NOCOUNT OFF;
END
GO

SELECT * FROM Product

INSERT INTO Product(Code, [Name], LevelNumber, IndexNumber, IsGroup, UnitId, CostPrice, SellingPrice, TaxPercent, Surcharge, Freight, ProfitPercent,
MinimumQuantity, MaximumQuantity, ReorderLevelQuantity, CompanyId, Active)
VALUES('MOUSE', 'Mouse', 1, 1, 0, 1, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0, 0, 0, 2, 1)



insert into Unit values('DOZ','Dozen','',12,null,0,0,null,2,1)
insert into Unit values('KG','Kilogram','',0,null,0,0,null,2,1)
insert into Unit values('LB','Pounds','',0,null,0,0,null,2,1)
insert into Unit values('CEL','Celsius','',0,null,0,0,null,2,1)
insert into Unit values('FAH','Fahrenheit','',0,null,0,0,null,2,1)

insert into UnitConversion values (4,5,1,'*',2.2, 1)
insert into UnitConversion values (6,7,1,'*',1.8, 1)
insert into UnitConversion values (6,7,2,'+',32, 1)
insert into UnitConversion values (1,3,1,'/',12, 1)
insert into UnitConversion values (3,1,1,'*',12, 1)
insert into UnitConversion values (8, 1, 1, '*' , 10, 1)
insert into UnitConversion values (9, 8, 1, '*' , 10, 1)


select * from Unit
select * from UnitConversion

select u1.[Name] UnitFrom, u2.[Name] UnitTo, CalcStep, Operator, Factor
from UnitConversion uc 
inner join Unit u1 on uc.FromUnitId=u1.Id
inner join Unit u2 on uc.ToUnitId=u2.Id
where uc.FromUnitId=6 and uc.ToUnitId=7
order by CalcStep
go

--drop function dbo.ufn_UnitConv

select uc.FromUnitId, uc.ToUnitId, u1.[Name] UnitFrom, u2.[Name] UnitTo, CalcStep, Operator, Factor
from UnitConversion uc 
inner join Unit u1 on uc.FromUnitId=u1.Id
inner join Unit u2 on uc.ToUnitId=u2.Id
--where uc.UnitFrom=6 and uc.UnitTo=7
--order by CalcStep

select * from Unit
select * from UnitConversion



SELECT dbo.ufn_ConvertUnit(3, 1, 12)
SELECT dbo.ufn_ConvertUnit(1, 3, 12)

select convert(money, 10 * 2.20462)
SELECT dbo.ufn_ConvertUnit(4, 5, 10)

SELECT dbo.ufn_ConvertUnit(6, 7, 100), dbo.ufn_ConvertUnit(7, 6, 212)

select dbo.ufn_ConvertUnit(6, 7, 47) -- cel to fah
select dbo.ufn_ConvertUnit(7, 6, 35) -- fah to cel

select dbo.ufn_ConvertUnit(3, 1, 2) -- doz to no.
select dbo.ufn_ConvertUnit(1, 3, 25) -- no. to doz

select 0.08333*12

-- eval
exec('select 0.08333' + '* 12')
go

SELECT dbo.ufn_GetChildUnit(3)
