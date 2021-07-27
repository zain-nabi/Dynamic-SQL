USE CRM
GO

CREATE PROCEDURE proc_Custormer_GetCustomersByCustomerAnalysisID 
@CustomerAnalysisID INT,
@DatabaseName VARCHAR(250)

AS
BEGIN

DECLARE @SQL NVARCHAR(MAX)

SET @SQL ='SELECT * FROM ' + @DatabaseName + '.[dbo].[vwCustomers] vwC WITH(NOLOCK)
INNER JOIN ' + @DatabaseName + '.[dbo].[CustomerAnalysis] CA WITH(NOLOCK) ON vwC.CustomerID = CA.CustomerID
WHERE CA.CustomerAnalysisID = ' + CONVERT(VARCHAR(MAX),@CustomerAnalysisID) + ''

EXEC (@SQL)

END


